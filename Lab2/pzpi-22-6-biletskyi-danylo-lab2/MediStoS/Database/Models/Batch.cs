﻿using MediStoS.DataTransferObjects;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;

namespace MediStoS.Database.Models;

public class Batch
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("batch_number")]
    public string BatchNumber { get; set; } = string.Empty;
    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }
    [JsonPropertyName("manufacture_date")]
    public DateTime ManufactureDate { get; set; }
    [JsonPropertyName("expiration_date")]
    public DateTime ExpirationDate { get; set; }
    [JsonPropertyName("warehouse_id")]
    public int WarehouseId { get; set; }
    [JsonIgnore]
    public Warehouse Warehouse { get; set; } = null!;
    [JsonPropertyName("user_id")]
    public int UserId { get; set; }
    [JsonIgnore]
    public User User { get; set; } = null!;
    [JsonPropertyName("medicine_id")]
    public int MedicineId { get; set; }
    [JsonIgnore]
    public Medicine Medicine { get; set; } = null!;
}
