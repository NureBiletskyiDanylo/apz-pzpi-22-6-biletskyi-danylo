using MediStoS_MobileClient.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MediStoS_MobileClient.DataTransferModels
{
    public class SensorDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("serial_number")]
        public string SerialNumber { get; set; } = string.Empty;
        [JsonPropertyName("value")]
        public float Value { get; set; }

    }
}
