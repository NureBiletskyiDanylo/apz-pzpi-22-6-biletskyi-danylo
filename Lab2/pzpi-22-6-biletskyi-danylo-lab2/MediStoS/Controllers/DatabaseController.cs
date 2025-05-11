using MediStoS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediStoS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseController() : ControllerBase
    {
        private BackupService _backupService = new BackupService();
        [HttpPost]
        [Route("Backup")]
        [Authorize(Roles = "DBAdmin")]
        public async Task<IActionResult> CreateBackup()
        {
            bool result = await _backupService.BackupAsync();
            return result ? Ok() : BadRequest("Backup was not successful");
        }

        [HttpPost]
        [Route("Restore")]
        [Authorize(Roles = "DBAdmin")]
        public async Task<ActionResult> Restore()
        {
            string latestFile = _backupService.GetLatestBackupFile();
            if (string.IsNullOrEmpty(latestFile)) return BadRequest("There are no backups to restore from");

            bool result = await _backupService.RestoreAsync(latestFile);
            return result ? Ok() : BadRequest("Restore was not successful");
        }
    }
}
