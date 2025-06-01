using MediStoS_MobileClient.DataTransferModels;
using MediStoS_MobileClient.ServerValues;
using System.Net.Http.Headers;
using System.Text.Json;

namespace MediStoS_MobileClient.Services;

public class WarehouseService
{
    private readonly HttpClient _httpClient;

    public WarehouseService()
    {
        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
        };
        _httpClient = new HttpClient(handler);
        _httpClient.BaseAddress = new Uri(ServerData.API_URL);
    }

    public async Task<List<WarehouseDto>> GetWarehousesAsync()
    {
        try
        {
            var token = await SecureStorage.GetAsync("auth_token");
            if (string.IsNullOrEmpty(token))
            {
                Console.WriteLine("No token found");
                return new List<WarehouseDto>();
            }

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync("/api/warehouse");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<WarehouseDto>>(content);
            }
            return new List<WarehouseDto>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching warehouses: {ex.Message}");
            return new List<WarehouseDto>();
        }
    }

    public async Task<List<SensorDto>> GetSensorsByWarehouseIdAsync(int warehouseId)
    {
        try
        {
            var token = await SecureStorage.GetAsync("auth_token");
            if (string.IsNullOrEmpty(token))
            {
                Console.WriteLine("No authentication token found");
                return new List<SensorDto>();
            }

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync($"/api/sensor/{warehouseId}/Sensors");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<SensorDto>>(content);
            }

            return new List<SensorDto>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching sensors: {ex.Message}");
            return new List<SensorDto>();
        }
        finally
        {
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
