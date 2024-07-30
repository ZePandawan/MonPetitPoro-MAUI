using Microsoft.Extensions.Configuration;
namespace MonPetitPoro;
using MonPetitPoro.Pages;

public partial class MainPage : ContentPage
{
	IConfiguration configuration;

    public MainPage(IConfiguration config)
	{
		InitializeComponent();
		configuration = config;
        NavigationPage.SetHasNavigationBar(this, false);
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        AuthenticationToken authenticationToken = new AuthenticationToken();
        string result = await authenticationToken.GetAuthTokenAsync();
        if (result != null)
        {
			//Navigation.PushAsync(new AccueilPage());
			Application.Current.MainPage = new NavigationPage(new AccueilPage(configuration));
		}

    }

    private void OnConnexionBtnClicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(new ConnexionPage(configuration));
	}

	private void OnInscriptionBtnClicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(new InscriptionPage(configuration));
	}

	private void OnListClicked(object sender, EventArgs e){
		Navigation.PushAsync(new TeamsPage());
	}


    

}

