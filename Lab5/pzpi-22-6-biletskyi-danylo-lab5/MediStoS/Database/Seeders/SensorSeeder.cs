using MediStoS.Database.Models;

namespace MediStoS.Database.Seeders;

using MediStoS.Enums;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public static class SensorSeeder
{
    private static readonly Random _random = new Random();

    public static List<Sensor> GetSeedSensors(int[] warehouses, int count = 20)
    {
        var sensors = new List<Sensor>();

        for (int i = 0; i < count; i++)
        {
            var type = (SensorType)_random.Next(0, 2);
            float value = type == SensorType.Temperature
                ? (float)(_random.NextDouble() * 40 - 10)
                : (float)(_random.NextDouble() * 60 + 30);

            var sensor = new Sensor
            {
                Type = type,
                SerialNumber = $"S-{_random.Next(1000, 9999)}",
                Value = (float)Math.Round(value, 2),
                WarehouseId = warehouses[_random.Next(warehouses.Length)]
            };

            sensors.Add(sensor);
        }

        return sensors;
    }
}

