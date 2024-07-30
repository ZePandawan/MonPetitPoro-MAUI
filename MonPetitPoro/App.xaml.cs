using Microsoft.Extensions.Configuration;

namespace MonPetitPoro;

public partial class App : Application
{
    private IConfiguration configuration;
    public App()
	{
		InitializeComponent();
        MainPage = new AppShell();
	}

    /*
    protected override async void OnStart()
    {

        base.OnStart();

        AuthenticationToken authenticationToken = new AuthenticationToken();
        string isLoggedIn = await authenticationToken.GetAuthTokenAsync();
        if (isLoggedIn != null)
        {
            MainPage = new NavigationPage(new MainPage(configuration)); // Page principale de l'application
        }
        else
        {
            MainPage = new NavigationPage(new ConnexionPage(App.Current.Services.GetService<IConfiguration>())); // Page de connexion
        }
    }
    */
}
