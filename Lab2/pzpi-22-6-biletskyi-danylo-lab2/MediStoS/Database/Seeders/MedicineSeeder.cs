using MediStoS.Database.Models;

namespace MediStoS.Database.Seeders;

public static class MedicineSeeder
{
    private static readonly string[] Names = ["MedAlpha", "BetaCure", "ZetaHeal", "XenMed", "OmniCure", "AquaDrop", "ThermaFix", "NovaHeal", "QuickRelief", "ImmunoPlus"];
    private static readonly string[] Manufacturers = ["HealthCorp", "MediGen", "PharmaX", "WellnessLabs", "CureAll", "BioNova"];

    public static List<Medicine> GetSeedMedicines(int count = 10)
    {
        Random rnd = new();
        var list = new List<Medicine>();

        for (int i = 0; i < count; i++)
        {
            list.Add(new Medicine
            {
                Name = Names[rnd.Next(Names.Length)] + i,
                Manufacturer = Manufacturers[rnd.Next(Manufacturers.Length)],
                Description = "Sample description " + i,
                MinTemperature = rnd.Next(-10, 5),
                MaxTemperature = rnd.Next(6, 30),
                MinHumidity = rnd.Next(10, 40),
                MaxHumidity = rnd.Next(41, 80)
            });
        }

        return list;
    }
}
