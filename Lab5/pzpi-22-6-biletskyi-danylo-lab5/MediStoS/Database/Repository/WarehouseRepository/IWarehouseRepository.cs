using MediStoS.Database.Models;
using MediStoS.DataTransferObjects;

namespace MediStoS.Database.Repository.WarehouseRepository
{
    public interface IWarehouseRepository
    {
        Task<bool> AddWarehouseAsync(Warehouse warehouse);
        Task<bool> DeleteWarehouse(Warehouse warehouse);
        Task<bool> DeleteWarehouseByIdAsync(int id);
        Task<WarehouseDto?> GetWarehouse(int id);
        Task<List<WarehouseDto>> GetWarehouses();
        Task<bool> UpdateWarehouse(Warehouse warehouse);
        Task<bool> UpdateWarehouseAsync(WarehouseDto warehouse, string emailOfChanger);
    }
}