using CommunityToolkit.Maui;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StudentRandomizer.Components;
using StudentRandomizer.EntityFrameworkCore;
using StudentRandomizer.Models;
using StudentRandomizer.Pages;
using StudentRandomizer.Services.Common;
using StudentRandomizer.Services.ExternalData;
using StudentRandomizer.Services.Groups;
using StudentRandomizer.Services.LuckyNumbers;
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
			.UseMauiCommunityToolkit()
			.AddDatabase()
			.ConfigureRepositories()
			.ConfigureServices()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				fonts.AddFont("Font Awesome 6 Brands-Regular-400.otf", "FontAwesomeBrands");
				fonts.AddFont("Font Awesome 6 Free-Regular-400.otf", "FontAwesome");
				fonts.AddFont("Font Awesome 6 Free-Solid-900.otf", "FontAwesomeSolid");
			})
			.ConfigurePages();

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
				DatabaseContext.ConfigureDatabase(opts);
			});

		return builder;
	}

	public static MauiAppBuilder ConfigurePages(this MauiAppBuilder builder)
	{
		builder.Services
			.AddSingleton<AppShell>()
			.AddSingleton<StudentsListPage>()
			.AddSingleton<StudentFormPage>()
			.AddSingleton<GroupsListPage>()
			.AddSingleton<GroupFormPage>()
			.AddSingleton<GroupAddStudentPage>()
			.AddSingleton<SchoolClassesListPage>()
			.AddSingleton<SchoolClassFormPage>()
			.AddSingleton<SchoolClassAddStudentPage>()
			.AddSingleton<NewRollPage>()
			.AddSingleton<LuckyNumberPage>()
			.AddSingleton<CheckAttendencePage>()
			.AddSingleton<ExternalDataPage>()
			.AddSingleton<MainPage>();

		return builder;
	}

	public static MauiAppBuilder ConfigureRepositories(this MauiAppBuilder builder)
	{
		builder.Services
			.AddTransient<IRepository<Student>, EfRepository<Student>>()
			.AddTransient<IRepository<SchoolClass>, EfRepository<SchoolClass>>()
			.AddTransient<IRepository<Models.SchoolClassEntry>, EfRepository<Models.SchoolClassEntry>>()
			.AddTransient<IRepository<Group>, EfRepository<Group>>()
			.AddTransient<IRepository<Models.GroupEntry>, EfRepository<Models.GroupEntry>>()
			.AddTransient<IRepository<RollScope>, EfRepository<RollScope>>()
			.AddTransient<IRepository<Roll>, EfRepository<Roll>>()
			.AddTransient<IRepository<LuckyNumber>, EfRepository<LuckyNumber>>()
			.AddTransient<IRepository<AttendanceRecord>, EfRepository<AttendanceRecord>>();

		return builder;
	}

	public static MauiAppBuilder ConfigureServices(this MauiAppBuilder builder)
	{
		builder.Services
			.AddTransient<IStudentDataService, StudentDataService>()
			.AddTransient<ISchoolClassDataService, SchoolClassDataService>()
			.AddTransient<IGroupDataService, GroupDataService>()
			.AddTransient<IRollManagementService<SchoolClass>, SchoolClassRollManagementService>()
			.AddTransient<IRollManagementService<Group>, GroupRollManagementService>()
			.AddTransient<ILuckyNumberDataService, LuckyNumberDataService>()
			.AddTransient<IExternalDataService, ExternalDataService>();

		return builder;
	}
}
