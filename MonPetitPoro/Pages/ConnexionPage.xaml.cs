namespace MonPetitPoro;
using System;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Maui.Controls;
using MonPetitPoro.DB;
using System.ComponentModel;
using System.Diagnostics;
using MonPetitPoro.Pages;

public partial class ConnexionPage : ContentPage
{
    private IConfiguration configuration;
    public ConnexionPage(IConfiguration config)
    {
        InitializeComponent();
        configuration = config;
    }

    /*
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        AuthenticationToken authenticationToken = new AuthenticationToken();
        string result = await authenticationToken.GetAuthTokenAsync();
        if (result != null)
        {
            DisplayAlert("OUIIII", result, "OK");
        }
        
    }
    */

    private void OnConnexionBtnClicked(object sender, EventArgs e)
    {
        try{
            
            var settings = configuration.GetRequiredSection("Settings").Get<DatabaseSettings>();

            string username = usernameEntry.Text;
            string passwordDB = passwordEntry.Text;
            
            
            try{
                MySqlConnection connection = new MySqlConnection($"server={settings.Server};user={settings.User};database={settings.Database};port=3306;password={settings.Password}");
                connection.Open();
                string query = $"SELECT * FROM Users WHERE username='{username}'";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                if(reader.Read()){
                    if(BCrypt.Net.BCrypt.Verify(passwordDB, reader["password"].ToString())){
                        DisplayAlert("Succès", "Connexion réussie", "OK");
                        ConnexionReussie(reader["token"].ToString());
                        Application.Current.MainPage = new NavigationPage(new AccueilPage(configuration));
                    }
                    else{
                        DisplayAlert("Erreur", "Mot de passe incorrect", "OK");
                    }
            }

            //Navigation.PushAsync(new TeamsPage());
            }catch(Exception ex){
                DisplayAlert("Erreur", ex.Message, "OK");
                return;
            }
            
        }catch(Exception ex){
            DisplayAlert("Erreur", ex.Message, "OK");
            return;
        }
    }

    private void ConnexionReussie(string token)
    {
        AuthenticationToken authToken = new AuthenticationToken();
        authToken.SaveAuthTokenAsync(token);
    }

    private void OnInscriptionBtnClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new InscriptionPage(configuration));
    }
}