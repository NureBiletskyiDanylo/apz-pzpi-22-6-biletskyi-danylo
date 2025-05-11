using System.Diagnostics;

namespace MediStoS.Services;

public class BackupService
{
    private readonly string _powershellPath = "powershell.exe";
    private readonly string _backupScriptPath = @"C:\Study\University\APZ\apz-pzpi-22-6-biletskyi-danylo\Lab2\pzpi-22-6-biletskyi-danylo-lab2\MediStoS\Database\Backup\backup.ps1";
    private readonly string _restoreScriptPath = @"C:\Study\University\APZ\apz-pzpi-22-6-biletskyi-danylo\Lab2\pzpi-22-6-biletskyi-danylo-lab2\MediStoS\Database\Backup\restore.ps1";
    private readonly string _backupFolder = @"C:\Study\University\APZ\apz-pzpi-22-6-biletskyi-danylo\Lab2\pzpi-22-6-biletskyi-danylo-lab2\MediStoS\Database\Backup\";

    public async Task<bool> BackupAsync()
    {
        var processInfo = new ProcessStartInfo
        {
            FileName = _powershellPath,
            Arguments = $"-ExecutionPolicy Bypass -File \"{_backupScriptPath}\"",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var process = new Process { StartInfo = processInfo };

        process.Start();
        string output = await process.StandardOutput.ReadToEndAsync();
        string error = await process.StandardError.ReadToEndAsync();
        process.WaitForExit();

        if (process.ExitCode == 0)
        {
            Console.WriteLine("Backup succeeded");
            Console.WriteLine(output);
            return true;
        }
        else
        {
            Console.WriteLine("Backup failed.");
            Console.WriteLine(error);
            return false;
        }
    }

    public async Task<bool> RestoreAsync(string sqlFilePath)
    {
        var processInfo = new ProcessStartInfo
        {
            FileName = _powershellPath,
            Arguments = $"-ExecutionPolicy Bypass -File \"{_restoreScriptPath}\" \"{sqlFilePath}\"",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };
        using var process = new Process { StartInfo = processInfo };

        process.Start();
        string output = await process.StandardOutput.ReadToEndAsync();
        string error = await process.StandardError.ReadToEndAsync();
        process.WaitForExit();

        if (process.ExitCode == 0)
        {
            Console.WriteLine("Restore succeeded.");
            Console.WriteLine(output);
            return true;
        }
        else
        {
            Console.WriteLine("Restore failed.");
            Console.WriteLine(error);
            return false;
        }
    }

    public string GetLatestBackupFile()
    {
        if (!Directory.Exists(_backupFolder))
            throw new DirectoryNotFoundException("Directory not found for backup");

        var latestFile = new DirectoryInfo(_backupFolder)
            .GetFiles("*.sql")
            .OrderByDescending(f => f.LastWriteTime)
            .FirstOrDefault();

        return latestFile?.FullName!;
    }
}
