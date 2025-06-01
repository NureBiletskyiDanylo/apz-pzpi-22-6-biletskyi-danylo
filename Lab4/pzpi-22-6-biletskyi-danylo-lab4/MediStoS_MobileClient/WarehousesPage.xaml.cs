using MediStoS_MobileClient.DataTransferModels;
using MediStoS_MobileClient.Models;

namespace MediStoS_MobileClient;

public partial class WarehousesPage : ContentPage
{
	public WarehousesPage(WarehouseViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    protected override void OnAppearing()
    {
		base.OnAppearing();
		if (BindingContext is WarehouseViewModel viewModel)
		{
			viewModel.LoadWarehousesCommand.Execute(null);
		}
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        await SecureStorage.SetAsync("auth_token", string.Empty);
        SecureStorage.Remove("auth_token");

        await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
    }

}