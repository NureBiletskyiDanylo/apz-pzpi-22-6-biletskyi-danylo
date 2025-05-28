using System.Text.Json.Serialization;

namespace MediStoS.DataTransferObjects;

public class SensorDto
{
    public int Id { get; set; }
    public string Type { get; set; } = string.Empty;
    [JsonPropertyName("serial_number")]
    public string SerialNumber { get; set; } = string.Empty;
    public float Value { get; set; }
    [JsonPropertyName("warehouse_id")]
    public int WarehouseId { get; set; }
}
