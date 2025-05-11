using MediStoS.Database.Models;

namespace MediStoS.Database.Seeders;

public static class BatchSeeder
{
    public static List<Batch> GetSeedBatches(int[] medicineIds, int[] warehouseIds, int count = 15)
    {
        Random rnd = new();
        var list = new List<Batch>();

        for (int i = 0; i < count; i++)
        {
            var manufactureDate = DateTime.UtcNow.AddMonths(-rnd.Next(1, 24));
            var expirationDate = manufactureDate.AddMonths(rnd.Next(6, 36));

            list.Add(new Batch
            {
                BatchNumber = $"BATCH-{rnd.Next(10000, 99999)}",
                Quantity = rnd.Next(50, 1000),
                ManufactureDate = manufactureDate,
                ExpirationDate = expirationDate,
                WarehouseId = warehouseIds[rnd.Next(warehouseIds.Length)],
                MedicineId = medicineIds[rnd.Next(medicineIds.Length)],
                UserId = 1
            });
        }

        return list;
    }
}

