using System.Reflection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace MonPetitPoro;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var a = Assembly.GetExecutingAssembly();
		using var stream = a.GetManifestResourceStream("MonPetitPoro.Resources.Raw.appsettings.json");

		var config = new ConfigurationBuilder()
			.AddJsonStream(stream)
			.Build();

		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("friz-quadrata-regular.ttf", "FrizQuadrataRegular");
				fonts.AddFont("Montserrat-Black.otf", "MontserratBlack");
				//fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				//fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif
		// Add configuration to the builder
		builder.Configuration.AddConfiguration(config);
		builder.Services.AddSingleton(config);
		builder.Services.AddTransient<MainPage>();
		builder.Services.AddTransient<ConnexionPage>();
		builder.Services.AddTransient<InscriptionPage>();

		return builder.Build();
	}
}
