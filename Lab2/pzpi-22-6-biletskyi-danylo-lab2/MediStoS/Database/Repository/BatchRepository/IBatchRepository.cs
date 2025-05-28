using MediStoS.Database.Models;
using MediStoS.DataTransferObjects;

namespace MediStoS.Database.Repository.BatchRepository
{
    public interface IBatchRepository
    {
        Task<bool> AddBatchAsync(Batch batch);
        Task<bool> DeleteBatchAsync(Batch batch);
        Task<bool> DeleteBatchByIdAsync(int batchId);
        Task<BatchDto?> GetBatchByIdAsync(int batchId);
        Task<List<BatchDto>> GetBatchesByMedicineId(int medicineId);
        Task<bool> UpdateBatch(BatchDto newBatch);
        Task<string> ValidateBatchCreateDtoAsync(BatchCreateDto createDto);
        Task<int> GetWarehouseLocation(int id);
        Task<List<BatchDto>> GetBatchesByWarehouseId(int warehouseId);
        int GetBatchesCountByWarehouseId(int warehouseId);
        Task<int> GetMedicineIdByBatchId(int batchId);
    }
}