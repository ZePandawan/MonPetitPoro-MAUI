namespace MonPetitPoro;
using System;
using System.Text.RegularExpressions;
using System.Net.Mail;

using MySql.Data.MySqlClient;

using BCrypt.Net;
using Microsoft.Extensions.Configuration;
using MonPetitPoro.DB;

public partial class InscriptionPage : ContentPage
{
    IConfiguration configuration;
    public InscriptionPage(IConfiguration config)
    {
        InitializeComponent();
        configuration = config;
    }

    private void OnInscriptionBtnClicked(object sender, EventArgs e)
    {
        string username = usernameEntry.Text;
        string password = passwordEntry.Text;
        string email = emailEntry.Text;
        string confirmPassword = confirmPasswordEntry.Text;
        string passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";

        if(username == "" || password == "" || email == "" || confirmPassword == ""){
            DisplayAlert("Erreur", "Veuillez remplir tous les champs", "OK");
            return;
        }

        if(password != confirmPassword){
            DisplayAlert("Erreur", "Les mots de passe ne correspondent pas", "OK");
            return;
        }

        if(!IsValidEmail(email)){
            DisplayAlert("Erreur", "L'email n'est pas valide", "OK");
            return;
        }

        if(!Regex.IsMatch(password, passwordPattern)){
            DisplayAlert("Erreur", "Le mot de passe doit contenir au moins 8 caractères, une majuscule, une minuscule, un chiffre et un caractère spécial", "OK");
            return;
        }

        try{
            var settings = configuration.GetRequiredSection("Settings").Get<DatabaseSettings>();

            // Ajout de l'utilisateur dans la base de données avec un mot de passe hashé
            MySqlConnection connection = new MySqlConnection($"server={settings.Server};user={settings.User};database={settings.Database};port=3306;password={settings.Password}");
            connection.Open();

            AuthenticationToken authenticationToken = new AuthenticationToken();
            string token = authenticationToken.GenerateToken();

            string query = $"INSERT INTO Users (username, password, email, token) VALUES ('{username}', '{BCrypt.HashPassword(password)}', '{email}', '{token}')";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();

            connection.Close();
        }catch(Exception ex){
            DisplayAlert("Erreur", ex.Message, "OK");
            return;
        }
    }

    private void OnConnexionBtnClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ConnexionPage(configuration));
    }

    public bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return true;
        }
        catch
        {
            return false;
        }
    }
}