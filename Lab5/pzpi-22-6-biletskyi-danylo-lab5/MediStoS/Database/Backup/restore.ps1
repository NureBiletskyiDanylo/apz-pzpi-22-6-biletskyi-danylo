param (
    [string]$sqlFilePath
)

# CONFIGURATION
$psqlPath = "C:\Program Files\PostgreSQL\17\bin\psql.exe"
$pgHost = "127.0.0.1"
$port = "5432"
$user = "postgres"
$password = "IronSword12"
$database = "medistosdb"

# Ensure file exists
if (!(Test-Path -Path $sqlFilePath)) {
    Write-Host "SQL file not found: $sqlFilePath"
    exit 1
}

$env:PGPASSWORD = $password

$arguments = @(
    "--host=$pgHost"
    "--port=$port"
    "--username=$user"
    "--dbname=$database"
    "--file=`"$sqlFilePath`""
)

Write-Host "Starting database restore from $sqlFilePath..."
$process = Start-Process -FilePath $psqlPath -ArgumentList $arguments -NoNewWindow -Wait -PassThru -RedirectStandardOutput stdout.txt -RedirectStandardError stderr.txt

Remove-Item Env:PGPASSWORD

if ($process.ExitCode -eq 0) {
    Write-Host "Restore successful from: $sqlFilePath"
    exit 0
} else {
    Write-Host "Restore failed. See stderr.txt"
    exit 1
}
