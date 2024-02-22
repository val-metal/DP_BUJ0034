using DP_BUJ0034.Engine;
using DP_BUJ0034.ViewModels;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Hosting;

namespace DP_BUJ0034;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
		builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<SelectLevelMenu>();
        builder.Services.AddSingleton<Setting>();
        builder.Services.AddTransient<FinishPage>();
		
		builder=RegisterServices(builder);
		builder = RegisterViewModels(builder);

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
    {
		mauiAppBuilder.Services.AddSingleton<MainPageViewModel>();
        mauiAppBuilder.Services.AddSingleton<FinishPageViewModel>();
        mauiAppBuilder.Services.AddSingleton<SettingViewModel>();
        mauiAppBuilder.Services.AddSingleton<SelectLevelViewModel>();

        return mauiAppBuilder;
    }
    public static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder)
    {
		mauiAppBuilder.Services.AddSingleton<AudioPlayerWrapper>();
        mauiAppBuilder.Services.AddSingleton<ScoreLoader>();
		mauiAppBuilder.Services.AddSingleton<TextureProvider>();

        return mauiAppBuilder;
    }
}
