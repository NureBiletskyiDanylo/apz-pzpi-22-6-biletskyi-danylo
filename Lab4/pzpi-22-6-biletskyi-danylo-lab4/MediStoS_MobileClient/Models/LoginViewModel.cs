using MediStoS_MobileClient.DataTransferModels;
using MediStoS_MobileClient.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MediStoS_Mobile.Models;

public class LoginViewModel : INotifyPropertyChanged
{
    private readonly AuthService authService = new AuthService();
    private string _email;
    private string _password;

    public string Email
    {
        get => _email;
        set
        {
            _email = value;
            OnPropertyChanged();
        }
    }

    public string Password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged();
        }
    }

    public ICommand LoginCommand { get; }

    public LoginViewModel()
    {
        LoginCommand = new Command(OnLoginClicked);
    }

    private async void OnLoginClicked()
    {
        if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
        {
            await Shell.Current.DisplayAlert("Error", "Please enter both email and password", "OK");
            return;
        }

        AccountDto? account = await authService.Login(Email, Password);
        if (account == null)
        {
            await Shell.Current.DisplayAlert("Error", "Email or password are wrong", "OK");
            return;
        }

        await SecureStorage.SetAsync("auth_token", account.Token);
        await Shell.Current.GoToAsync("//WarehousesPage");
    }


    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
