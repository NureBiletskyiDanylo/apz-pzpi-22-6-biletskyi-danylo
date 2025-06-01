using MediStoS.Database.Models;
using MediStoS.DataTransferObjects;

namespace MediStoS.Database.Repository.SensorRepository
{
    public interface ISensorRepository
    {
        Task<bool> AddSensor(Sensor sensor);
        Task<bool> DeleteSensor(int id);
        Task<SensorDto?> GetSensor(int id);
        Task<List<SensorDto>> GetSensorsByWarehouseId(int warehouseId);
        Task<bool> IsWarehouseExist(int warehouseId);
        Task<bool> UpdateSensor(Sensor newSensor);
        int GetSensorCount(int warehouseId);
    }
}