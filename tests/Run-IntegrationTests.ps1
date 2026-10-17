param([string]$Server = '.\SQLEXPRESS', [switch]$LegacySchema)
$ErrorActionPreference = 'Stop'
$repo = Split-Path -Parent $PSScriptRoot
$database = 'TVStore_Requirements_Test_' + [Guid]::NewGuid().ToString('N')
$output = Join-Path $repo 'tv-store\bin\Debug'
$tempFolder = Join-Path ([IO.Path]::GetTempPath()) $database
New-Item -ItemType Directory -Path $tempFolder | Out-Null
function Invoke-TestSql([string]$File) {
    & sqlcmd -S $Server -E -b -l 10 -f 65001 -i $File
    if ($LASTEXITCODE -ne 0) { throw "SQL script failed: $File" }
}
try {
    # Script 01 và 03 xóa dữ liệu: đổi toàn bộ tên CSDL sang tên thử riêng trước khi chạy.
    foreach ($name in @('01_Create_Tables.sql','02_Triggers_StoreProcedures.sql','03_Sample_Data.sql','04_Upgrade_Requirements.sql')) {
        $text = [IO.File]::ReadAllText((Join-Path $repo "Database_Scripts\$name")).Replace('QLBanTivi', $database)
        if ($LegacySchema -and $name -eq '01_Create_Tables.sql') {
            $text = $text -replace '(?m)^.*Passw NVARCHAR.*\r?\n', ''
            $text = $text -replace '(?m)^.*GiaVonChot DECIMAL.*\r?\n', ''
        }
        if ($LegacySchema -and $name -eq '03_Sample_Data.sql') {
            $text = $text -replace '(?m)^UPDATE nv SET Passw=.*\r?\n', ''
        }
        $file = Join-Path $tempFolder $name
        [IO.File]::WriteAllText($file, $text, [Text.UTF8Encoding]::new($true))
        Invoke-TestSql $file
    }
    # Kiểm tra nâng cấp lặp lại không làm mất dữ liệu hoặc gây lỗi cấu trúc.
    Invoke-TestSql (Join-Path $tempFolder '04_Upgrade_Requirements.sql')
    $compiler = Join-Path $env:WINDIR 'Microsoft.NET\Framework64\v4.0.30319\csc.exe'
    & $compiler /nologo /target:exe "/out:$output\InvoiceIntegrationTests.exe" "/reference:$output\tv-store.exe" /reference:System.Data.dll /reference:System.Core.dll /reference:System.Windows.Forms.dll /reference:System.Drawing.dll (Join-Path $PSScriptRoot 'InvoiceIntegrationTests.cs')
    if ($LASTEXITCODE -ne 0) { throw 'Test compilation failed' }
    $connection = "Data Source=$Server;Initial Catalog=$database;Integrated Security=True;Connect Timeout=10"
    $escapedConnection = [System.Security.SecurityElement]::Escape($connection)
    $config = '<configuration><connectionStrings><add name="QLBanTivi" connectionString="' + $escapedConnection + '" providerName="System.Data.SqlClient" /></connectionStrings></configuration>'
    [IO.File]::WriteAllText((Join-Path $output 'InvoiceIntegrationTests.exe.config'), $config, [Text.UTF8Encoding]::new($true))
    & (Join-Path $output 'InvoiceIntegrationTests.exe') "Data Source=$Server;Initial Catalog=$database;Integrated Security=True;Connect Timeout=10"
    if ($LASTEXITCODE -ne 0) { throw 'Integration tests failed' }
}
finally {
    if ($database -notmatch '^TVStore_Requirements_Test_[0-9a-f]{32}$') { throw 'Refusing unexpected test database name' }
    & sqlcmd -S $Server -E -b -l 10 -Q "IF DB_ID(N'$database') IS NOT NULL BEGIN ALTER DATABASE [$database] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE [$database]; END"
    # Giữ script tạm để tìm lỗi; CSDL thử đã được xóa.
}
