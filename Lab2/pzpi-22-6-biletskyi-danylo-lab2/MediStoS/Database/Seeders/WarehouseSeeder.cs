using MediStoS.Database.Models;

namespace MediStoS.Database.Seeders;

public class WarehouseSeeder
{
    private static readonly string[] Names = [
            "North Depot", "South Storage", "East Facility", "West Hub",
        "Central Unit", "Delta Warehouse", "Omega Storage", "Sigma Depot",
        "Nova Hub", "Prime Storage"
        ];

    private static readonly string[] Addresses = [
        "123 Alpha St.", "456 Beta Ave.", "789 Gamma Rd.", "101 Delta Blvd.",
        "202 Epsilon Ln.", "303 Zeta Dr.", "404 Theta Ct.", "505 Iota Way"
    ];

    public static List<Warehouse> GetSeedWarehouses(int count = 10)
    {
        Random rnd = new();
        var list = new List<Warehouse>();

        for (int i = 0; i < count; i++)
        {
            list.Add(new Warehouse
            {
                Name = Names[rnd.Next(Names.Length)] + " " + (char)('A' + i),
                Address = Addresses[rnd.Next(Addresses.Length)],
                MinTemperature = rnd.Next(-20, 0),
                MaxTemperature = rnd.Next(1, 25),
                MinHumidity = rnd.Next(10, 40),
                MaxHumidity = rnd.Next(41, 90),
                CreatedAt = DateTime.UtcNow.AddDays(-rnd.Next(0, 365))
            });
        }

        return list;
    }

}
