using MediStoS.Database.DatabaseContext;
using MediStoS.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace MediStoS.Database.Repository.StorageViolationRepository;

public class StorageViolationRepository(ApplicationDbContext context) : IStorageViolationRepository
{
    public async Task<StorageViolation?> GetStorageViolation(int id, bool tracking = true)
    {
        StorageViolation? violation;
        if (tracking)
        {
            violation = await context.StorageViolations.FirstOrDefaultAsync(a => a.Id == id);
        }
        else
        {
            violation = await context.StorageViolations.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
        }
        return violation;
    }

    public async Task<bool> AddStorageViolation(StorageViolation violation)
    {
        await context.StorageViolations.AddAsync(violation);
        var result = await context.SaveChangesAsync();
        return result != 0;
    }

    public async Task<bool> DeleteStorageViolation(StorageViolation violation)
    {
        context.StorageViolations.Remove(violation);
        var result = await context.SaveChangesAsync();
        return result != 0;
    }

    public async Task<List<StorageViolation>> GetStorageViolationsByWarehouseId(int warehouseId)
    {
        List<StorageViolation> violations = await context.StorageViolations.Where(a => a.WarehouseId == warehouseId).ToListAsync();
        if (violations == null)
        {
            return new List<StorageViolation>();
        }
        return violations;
    }

    public async Task<bool> IsWarehouseExist(int warehouseId)
    {
        return await context.Warehouses.AnyAsync(a => a.Id == warehouseId);
    }

    public async Task<StorageViolation?> VerifyViolation(int sensorId, int warehouseId)
    {
        var sensor = await context.Sensors.FindAsync(sensorId);
        if (sensor == null) throw new ArgumentNullException("Sensor was not found");

        var warehouse = await context.Warehouses.Include(w => w.Sensors).FirstOrDefaultAsync(a => a.Id == warehouseId);
        if (warehouse == null) throw new ArgumentNullException("Warehouse was not found");

        bool exceeds = false;
        switch (sensor.Type)
        {
            case Enums.SensorType.Temperature:
                exceeds = sensor.Value > warehouse.MaxTemperature || sensor.Value < warehouse.MinTemperature;
                break;
            case Enums.SensorType.Humidity:
                exceeds = sensor.Value > warehouse.MaxHumidity || sensor.Value < warehouse.MinHumidity;
                break;
        }

        if (exceeds)
        {
            var average = warehouse.Sensors.Where(a => a.Type != sensor.Type).Average(a => a.Value);
            StorageViolation violation = new StorageViolation()
            {
                RecordedAt = DateTime.UtcNow,
                WarehouseId = warehouseId,
                Temperature = sensor.Type == Enums.SensorType.Temperature ? sensor.Value : average,
                Humidity = sensor.Type == Enums.SensorType.Humidity ? sensor.Value : average
            };

            await context.StorageViolations.AddAsync(violation);
            bool success = await context.SaveChangesAsync() > 0;
            if (success) return violation;
            return null;
        }
        return null;
    }
}
