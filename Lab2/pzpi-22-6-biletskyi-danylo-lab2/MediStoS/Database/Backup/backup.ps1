$pgDumpPath = "C:\Program Files\PostgreSQL\17\bin\pg_dump.exe"
$pgHost = "127.0.0.1" 
$port = "5432"
$user = "postgres"
$password = "IronSword12"
$database = "medistosdb"
$timestamp = Get-Date -Format "yyyyMMdd_HHmmss"
$outputPath = "C:\Study\University\APZ\apz-pzpi-22-6-biletskyi-danylo\Lab2\pzpi-22-6-biletskyi-danylo-lab2\MediStoS\Database\Backup\backup_$timestamp.sql"

$outputDir = Split-Path $outputPath
if (!(Test-Path -Path $outputDir)) {
    New-Item -Path $outputDir -ItemType Directory | Out-Null
}

$env:PGPASSWORD = $password

$arguments = @(
    "--host=$pgHost"
    "--username=$user"
    "--format=plain"
    "--clean"
    "--file=`"$outputPath`""
    "$database"
)

Write-Host "Starting backup..."
$process = Start-Process -FilePath $pgDumpPath -ArgumentList $arguments -NoNewWindow -Wait -PassThru -RedirectStandardOutput stdout.txt -RedirectStandardError stderr.txt

Remove-Item Env:PGPASSWORD

if ($process.ExitCode -eq 0) {
    Write-Host "Backup successful. Saved to: $outputPath"
    exit 0
} else {
    Write-Host "Backup failed. See stderr.txt"
    exit 1
}
