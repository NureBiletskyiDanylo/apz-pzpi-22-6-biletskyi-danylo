namespace MediStoS_MobileClient;

using MediStoS_MobileClient.DataTransferModels;
using MediStoS_MobileClient.Models;

[QueryProperty( nameof(Warehouse), "Warehouse")]
public partial class WarehouseDetailPage : ContentPage
{
    private WarehouseDto _warehouse;

    public WarehouseDto Warehouse
    {
        get => _warehouse;
        set
        {
            _warehouse = value;
            OnPropertyChanged();

            if (BindingContext is WarehouseDetailViewModel viewModel)
            {
                viewModel.Warehouse = value;
                viewModel.LoadDataCommand.Execute(value.Id);
            }
        }
    }
    public WarehouseDetailPage(WarehouseDetailViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
	}
}