using Microsoft.Extensions.Configuration;

namespace MonPetitPoro.Pages;

public partial class AccueilPage : ContentPage
{
    IConfiguration configuration;
    public AccueilPage(IConfiguration _config)
	{
		InitializeComponent();
        configuration = _config;
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
		AuthenticationToken authenticationToken = new AuthenticationToken();
		authenticationToken.ResetAuthToken();
        Application.Current.MainPage = new NavigationPage(new MainPage(configuration));
    }
}