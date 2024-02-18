using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StudentRandomizer.EntityFrameworkCore;

namespace StudentRandomizer;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.AddDatabase()
			.ConfigureServices()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}

	public static MauiAppBuilder AddDatabase(this MauiAppBuilder builder)
	{
		builder.Services
			.AddDbContext<DatabaseContext>(opts => 
				opts.UseSqlite($"Data Source={AppConsts.DbPath}"));

		return builder;
	}

	public static MauiAppBuilder ConfigureServices(this MauiAppBuilder builder)
	{
		return builder;
	}
}
