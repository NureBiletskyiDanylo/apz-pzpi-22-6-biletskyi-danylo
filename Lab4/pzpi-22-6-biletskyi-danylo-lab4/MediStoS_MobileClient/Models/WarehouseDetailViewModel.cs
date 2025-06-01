using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MediStoS_MobileClient.Services;
using MediStoS_MobileClient.DataTransferModels;

namespace MediStoS_MobileClient.Models
{
    public class WarehouseDetailViewModel : INotifyPropertyChanged
    {
        private readonly WarehouseService _warehouseService;
        private WarehouseDto _warehouse;
        private List<SensorDto> _sensors;
        private bool _isLoading;

        public WarehouseDto Warehouse
        {
            get => _warehouse;
            set
            {
                _warehouse = value;
                OnPropertyChanged();
            }
        }

        public List<SensorDto> Sensors
        {
            get => _sensors;
            set
            {
                _sensors = value;
                OnPropertyChanged();
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoadDataCommand { get; }

        public WarehouseDetailViewModel(WarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
            LoadDataCommand = new Command<int>(async (id) => await LoadData(id));
        }

        private async Task LoadData(int warehouseId)
        {
            IsLoading = true;
            try
            {
                Sensors = await _warehouseService.GetSensorsByWarehouseIdAsync(warehouseId);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Failed to load data: {ex.Message}", "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool IsValueInRange(SensorDto sensor)
        {
            if (Warehouse == null || sensor == null) return false;

            if (sensor.Type == "Temperature")
            {
                return sensor.Value >= Warehouse.MinTemperature &&
                       sensor.Value <= Warehouse.MaxTemperature;
            }
            else if (sensor.Type == "Humidity")
            {
                return sensor.Value >= Warehouse.MinHumidity &&
                       sensor.Value <= Warehouse.MaxHumidity;
            }
            return false;
        }

    }
}
