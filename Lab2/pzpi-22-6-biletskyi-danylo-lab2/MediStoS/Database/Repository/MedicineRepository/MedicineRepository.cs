using AutoMapper;
using MediStoS.Database.DatabaseContext;
using MediStoS.Database.Models;
using MediStoS.DataTransferObjects;
using Microsoft.EntityFrameworkCore;

namespace MediStoS.Database.Repository.MedicineRepository;

public class MedicineRepository(ApplicationDbContext context, IMapper mapper) : IMedicineRepository
{
    public async Task<MedicineDto?> GetMedicine(int id, bool withBatch = false)
    {
        Medicine? medicine;
        if (withBatch)
        {
        medicine = await context.Medicines.Include(a => a.Batches).ThenInclude(a => a.User).FirstOrDefaultAsync(a => a.Id == id);
        }
        else
        {
        medicine = await context.Medicines.FirstOrDefaultAsync(a => a.Id == id);
        }
        if (medicine == null) throw new ArgumentNullException(nameof(medicine), "Medicine was not found");
        MedicineDto medicineDto = mapper.Map<MedicineDto>(medicine);
        return medicineDto;
    }

    public async Task<List<MedicineDto>> GetMedicines()
    {
        List<Medicine> medicines = await context.Medicines.ToListAsync();
        if (medicines == null) return new List<MedicineDto>();

        List<MedicineDto> medicineDtos = mapper.Map<List<MedicineDto>>(medicines);
        return medicineDtos;
    }

    public async Task<bool> AddMedicineAsync(Medicine medicine)
    {
        await context.Medicines.AddAsync(medicine);
        var result = await context.SaveChangesAsync();
        return result != 0;
    }

    public async Task<bool> DeleteMedicine(Medicine medicine)
    {
        context.Medicines.Remove(medicine);
        var result = await context.SaveChangesAsync();
        return result != 0;
    }

    public async Task<bool> DeleteMedicineByIdAsync(int medicineId)
    {
        var medicine = await context.Medicines.FindAsync(medicineId);
        if (medicine == null) return false;

        context.Remove(medicine);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateMedicine(Medicine newMedicine)
    {
        context.Entry(newMedicine).State = EntityState.Detached;
        context.Medicines.Update(newMedicine);
        var result = await context.SaveChangesAsync();
        return result != 0;
    }

    public async Task<bool> UpdateMedicineAsync(MedicineDto newMedicine)
    {
        var medicine = await context.Medicines.AsNoTracking().FirstOrDefaultAsync(a => a.Id == newMedicine.Id);
        if (medicine == null) return false;

        var updatedMedicine = mapper.Map<Medicine>(newMedicine);
        updatedMedicine.Batches = medicine.Batches;
        context.Medicines.Update(updatedMedicine);

        return await context.SaveChangesAsync() > 0;
    }
}
