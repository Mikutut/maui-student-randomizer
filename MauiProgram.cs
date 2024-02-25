using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StudentRandomizer.EntityFrameworkCore;
using StudentRandomizer.Models;
using StudentRandomizer.Services.Common;
using StudentRandomizer.Services.Groups;
using StudentRandomizer.Services.SchoolClasses;
using StudentRandomizer.Services.Students;

namespace StudentRandomizer;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.AddDatabase()
			.ConfigureRepositories()
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
			{
				if(!Directory.Exists(AppConsts.AppDataPath))
				{
					Directory.CreateDirectory(AppConsts.AppDataPath);
				}

				opts.UseSqlite($"Data Source={AppConsts.DbPath}");
			});

		return builder;
	}

	public static MauiAppBuilder ConfigureRepositories(this MauiAppBuilder builder)
	{
		builder.Services
			.AddScoped<IRepository<Student>, EfRepository<Student>>()
			.AddScoped<IRepository<SchoolClass>, EfRepository<SchoolClass>>()
			.AddScoped<IRepository<SchoolClassEntry>, EfRepository<SchoolClassEntry>>()
			.AddScoped<IRepository<Group>, EfRepository<Group>>()
			.AddScoped<IRepository<GroupEntry>, EfRepository<GroupEntry>>()
			.AddScoped<IRepository<RollScope>, EfRepository<RollScope>>()
			.AddScoped<IRepository<Roll>, EfRepository<Roll>>();

		return builder;
	}

	public static MauiAppBuilder ConfigureServices(this MauiAppBuilder builder)
	{
		builder.Services
			.AddScoped<IStudentDataService, StudentDataService>()
			.AddScoped<ISchoolClassDataService, SchoolClassDataService>()
			.AddScoped<IGroupDataService, GroupDataService>();

		return builder;
	}
}
