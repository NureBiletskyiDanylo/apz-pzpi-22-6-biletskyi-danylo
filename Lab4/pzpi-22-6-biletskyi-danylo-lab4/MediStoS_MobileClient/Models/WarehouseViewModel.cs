using MediStoS_MobileClient.DataTransferModels;
using MediStoS_MobileClient.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MediStoS_MobileClient.Models
{
    public class WarehouseViewModel : INotifyPropertyChanged
    {
        private readonly WarehouseService _warehouseService;
        private List<WarehouseDto> _warehouses;
        private bool _isLoading;

        public List<WarehouseDto> Warehouses
        {
            get => _warehouses;
            set
            {
                _warehouses = value;
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

        public ICommand LoadWarehousesCommand { get; }
        public ICommand WarehouseSelectedCommand { get; }
        public WarehouseViewModel(WarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
            Warehouses = new List<WarehouseDto>();
            LoadWarehousesCommand = new Command(async () => await LoadWarehouses());
            WarehouseSelectedCommand = new Command<WarehouseDto>(OnWarehouseSelected);
        }

        private async Task LoadWarehouses()
        {
            IsLoading = true;
            try
            {
                Warehouses = await _warehouseService.GetWarehousesAsync();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Failed to load warehouses: {ex.Message}", "OK");
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

        private async void OnWarehouseSelected(WarehouseDto warehouse)
        {
            if (warehouse == null) return;

            await Shell.Current.GoToAsync(nameof(WarehouseDetailPage), new Dictionary<string, object>
    {
        { "Warehouse", warehouse }
    });
        }

    }
}
