using AutoMapper;
using MediStoS.Database.DatabaseContext;
using MediStoS.Database.Models;
using MediStoS.DataTransferObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;

namespace MediStoS.Database.Repository.SensorRepository;

public class SensorRepository(ApplicationDbContext context, IMapper mapper) : ISensorRepository
{
    public async Task<SensorDto?> GetSensor(int id)
    {
        var sensor = await context.Sensors.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
        if (sensor == null) throw new ArgumentNullException("Sensor was not found");

        return mapper.Map<SensorDto>(sensor);
    }
    public async Task<bool> AddSensor(Sensor sensor)
    {
        await context.Sensors.AddAsync(sensor);
        var result = await context.SaveChangesAsync();
        return result != 0;
    }

    public async Task<bool> DeleteSensor(int id)
    {
        var sensor = await context.Sensors.FindAsync(id);
        if (sensor == null) return false;
        context.Sensors.Remove(sensor);
        var result = await context.SaveChangesAsync();
        return result != 0;
    }

    public async Task<bool> UpdateSensor(Sensor newSensor)
    {
        var existing = await context.Sensors.FindAsync(newSensor.Id);
        if (existing != null)
        {
            context.Entry(existing).State = EntityState.Detached;
        }

        context.Sensors.Update(newSensor);
        var result = await context.SaveChangesAsync();
        return result != 0;
    }
    public async Task<List<SensorDto>> GetSensorsByWarehouseId(int warehouseId)
    {
        List<Sensor> sensors = await context.Sensors.Where(a => a.WarehouseId == warehouseId).ToListAsync();
        if (sensors == null)
        {
            return new List<SensorDto>();
        }
        return mapper.Map<List<SensorDto>>(sensors);
    }

    public async Task<bool> IsWarehouseExist(int warehouseId)
    {
        return await context.Warehouses.AnyAsync(a => a.Id == warehouseId);
    }

    public int GetSensorCount(int warehouseId)
    {
        int sensorCount = context.Sensors.Where(s => s.WarehouseId == warehouseId).Count();
        return sensorCount;
    }
}
