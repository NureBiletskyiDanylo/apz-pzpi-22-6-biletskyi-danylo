using MediStoS.Database.Models;
using MediStoS.DataTransferObjects;

namespace MediStoS.Database.Repository.MedicineRepository
{
    public interface IMedicineRepository
    {
        Task<bool> AddMedicineAsync(Medicine medicine);
        Task<bool> DeleteMedicine(Medicine medicine);
        Task<bool> DeleteMedicineByIdAsync(int medicineId);
        Task<MedicineDto?> GetMedicine(int id);
        Task<List<MedicineDto>> GetMedicines();
        Task<bool> UpdateMedicine(Medicine newMedicine);
        Task<bool> UpdateMedicineAsync(MedicineDto medicine);
    }
}