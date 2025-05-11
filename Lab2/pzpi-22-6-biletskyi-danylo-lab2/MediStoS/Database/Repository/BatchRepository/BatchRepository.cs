using AutoMapper;
using MediStoS.Database.DatabaseContext;
using MediStoS.Database.Models;
using MediStoS.DataTransferObjects;
using Microsoft.EntityFrameworkCore;

namespace MediStoS.Database.Repository.BatchRepository;

public class BatchRepository(ApplicationDbContext context, IMapper mapper) : IBatchRepository
{
    public async Task<BatchDto?> GetBatchByIdAsync(int batchId)
    {
        var batch = await context.Batches.Include(a => a.User).FirstOrDefaultAsync(a => a.Id == batchId);
        if (batch == null) throw new ArgumentNullException("Batch was not found");

        BatchDto batchDto = mapper.Map<BatchDto>(batch);
        return batchDto;
    }

    public async Task<bool> AddBatchAsync(Batch batch)
    {
        await context.Batches.AddAsync(batch);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteBatchAsync(Batch batch)
    {
        context.Batches.Remove(batch);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteBatchByIdAsync(int batchId)
    {
        var batch = await context.Batches.FindAsync(batchId);
        if (batch == null) return false;
        context.Batches.Remove(batch);
        return await context.SaveChangesAsync() > 0;
    }
    public async Task<bool> UpdateBatch(BatchDto newBatch)
    {
        var batch = await context.Batches.FindAsync(newBatch.Id);
        if (batch == null) return false;

        batch.ExpirationDate = newBatch.ExpirationDate;
        batch.BatchNumber = newBatch.BatchNumber;
        batch.Quantity = newBatch.Quantity;
        batch.ManufactureDate = newBatch.ManufactureDate;

        context.Batches.Update(batch);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<List<BatchDto>> GetBatchesByMedicineId(int medicineId)
    {
        var batches = await context.Batches.Where(a => a.MedicineId == medicineId)
            .Select(a => new BatchDto
            {
                Id = a.Id,
                BatchNumber = a.BatchNumber,
                Email = a.User.Email,
                ExpirationDate = a.ExpirationDate,
                ManufactureDate = a.ManufactureDate,
                Quantity = a.Quantity
            }).ToListAsync();
        if (batches == null) return new List<BatchDto>();
        return batches;
    }

    public async Task<string> ValidateBatchCreateDtoAsync(BatchCreateDto createDto)
    {
        if (!await IsUserExists(createDto.UserId)) return "User does not exist";
        if (!await IsMedicineExists(createDto.MedicineId)) return "Medicine does not exist";
        if (!await IsWarehouseExists(createDto.WarehouseId)) return "Warehouse does not exist";
        return string.Empty;
    }

    private async Task<bool> IsWarehouseExists(int warehouseId)
    {
        return await context.Warehouses.AnyAsync(a => a.Id == warehouseId);
    }

    private async Task<bool> IsUserExists(int userId)
    {
        return await context.Users.AnyAsync(a => a.Id == userId);
    }

    private async Task<bool> IsMedicineExists(int medicineId)
    {
        return await context.Medicines.AnyAsync(a => a.Id == medicineId);
    }
}
