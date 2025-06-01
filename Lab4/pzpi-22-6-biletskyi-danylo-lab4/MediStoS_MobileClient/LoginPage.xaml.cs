using MediStoS_Mobile.Models;

namespace MediStoS_MobileClient;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}