using MediStoS.Database.Models;
using MediStoS.Database.Repository.SensorRepository;
using MediStoS.Database.Repository.StorageViolationRepository;
using MediStoS.Database.Repository.UserRepository;
using MediStoS.Database.Repository.WarehouseRepository;
using MediStoS.Enums;
using MediStoS.Services.SMTPService;

namespace MediStoS.Services;

public class StorageRequirementsChecker(ISensorRepository sensorRepository, 
    IWarehouseRepository warehouseRepository, 
    IStorageViolationRepository storageViolationRepository, 
    IUserRepository userRepository,
    IEmailSender emailSender)
{
    public async Task StorageRequirementsValidation(int sensorId, int warehouseId)
    {
        var sensor = await sensorRepository.GetSensor(sensorId);
        if (sensor == null) return;
        var violation = await storageViolationRepository.VerifyViolation(sensorId, warehouseId);
        if (violation != null)
        {
            //WriteMessage(violation, sensor.Type);
        }
    }

    private float CalculateAverage(List<float> values)
    {
        return values.Average();
    }

    private async void WriteMessage(StorageViolation violation, string violationType)
    {
        List<string> emails = userRepository.GetUsersAsync().Result.Select(a => a.Email).ToList();
        foreach(var email in emails)
        {
            await emailSender.SendEmaiAsync(email: email, subject: $"Storage {violationType} violation!", message: $"There has been a violation in warehouse {violation.Warehouse.Name}\n" +
                $"Violation type is: {violationType};\n" +
                $"Temperature is: {violation.Temperature};\n" +
                $"Humidity is: {violation.Humidity};\n" +
                $"Recorded at: {violation.RecordedAt.ToString()}");
        }
    }
}
