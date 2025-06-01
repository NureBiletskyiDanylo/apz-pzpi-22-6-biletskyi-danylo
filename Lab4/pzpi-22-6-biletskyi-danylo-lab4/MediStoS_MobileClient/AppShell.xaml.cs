namespace MediStoS_MobileClient;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(WarehousesPage), typeof(WarehousesPage));
        Routing.RegisterRoute(nameof(WarehouseDetailPage), typeof(WarehouseDetailPage));
        CheckAuthStatus();
    }

    private async void CheckAuthStatus()
    {
        var token = await SecureStorage.GetAsync("auth_token");

        if (string.IsNullOrEmpty(token))
        {
            await GoToAsync($"//{nameof(LoginPage)}");
        }
        else
        {
            await GoToAsync($"//{nameof(WarehousesPage)}");
        }
    }
}
