namespace MonPetitPoro;

using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Diagnostics;

public partial class TeamsPage : ContentPage
{
    public TeamsPage()
    {
        InitializeComponent();
        GetTeamsInfo();
    }

    public async void GetTeamsInfo()
    {
        //HttpClient client = new HttpClient();
        /*
        var response = await client.GetAsync("http://bot.nightjs.ovh:6969/teams");
        //var response = await client.GetAsync("https://google.fr");
        Debug.WriteLine(response);*/

        Uri uri = new Uri("http://bot.nightjs.ovh:6969/teams");
        HttpClient client = new();

        try{
            
            HttpResponseMessage response = await client.GetAsync(uri);
            JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions(){
                PropertyNameCaseInsensitive = true
            };
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("OK :",content);
                
            }
        }catch(Exception ex){
            Debug.WriteLine($"Erreur : {ex.Message}");
        }
    }
}