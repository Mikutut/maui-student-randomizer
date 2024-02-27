using StudentRandomizer.Models;
using StudentRandomizer.Services.SchoolClasses;
using StudentRandomizer.Services.SchoolClasses.Inputs;

namespace StudentRandomizer.Pages;

public partial class SchoolClassesListPage : ContentPage
{
	private readonly ISchoolClassDataService _schoolClassDataService;

	private List<SchoolClass> schoolClasss = new List<SchoolClass>();

	public List<SchoolClass> SchoolClasses
	{
		get => schoolClasss;
		set
		{
			schoolClasss = value;
			OnPropertyChanged("SchoolClasses");
		}
	}

	public SchoolClassesListPage(ISchoolClassDataService schoolClassDataService)
	{
		_schoolClassDataService = schoolClassDataService;
		RefreshSchoolClassesList();
		InitializeComponent();

		BindingContext = this;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		RefreshSchoolClassesList();
	}

	private void RefreshSchoolClassesList()
	{
		SchoolClasses = _schoolClassDataService.GetAll(new GetAllSchoolClassesInput())
			.ToList();
	}

	private async void schoolClassesListPage_homeToolbarItem_Clicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("//home");
	}

	private async void SchoolClassEntry_OnDelete(SchoolClass obj)
	{
		var answer = await DisplayAlert("Usuwanie klasy", "Czy na pewno chcesz usunąć tą klasę?", "Tak", "Nie");

		if(answer)
		{
			try
			{
				_schoolClassDataService.Delete(obj.SchoolClassRefId);
			}
			catch
			{
				await DisplayAlert("Usuwanie klasy", "Nie udało się usunąć klasy.", "OK");
			}

			RefreshSchoolClassesList();
		}
	}

	private async void SchoolClassEntry_OnUpdate(SchoolClass obj)
	{
		var queryParams = new Dictionary<string, object>
		{
			{ "SchoolClass", obj }
		};

		await Shell.Current.GoToAsync("//schoolClasses/form", queryParams);
	}

	private async void schoolClassesListPage_createToolbarItem_Clicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("//schoolClasses/form");
	}

	private async void SchoolClassEntry_OnAddStudent(SchoolClass schoolClass)
	{
		var queryParams = new Dictionary<string, object>
		{
			{ "SchoolClass", schoolClass }
		};
		await Shell.Current.GoToAsync("//schoolClasses/addstudent", queryParams);
	}

	private async void SchoolClassEntry_OnNewRoll(SchoolClass obj)
	{
		var queryParams = new Dictionary<string, object>
		{
			{ "SchoolClass", obj }
		};
		await Shell.Current.GoToAsync("//newroll", queryParams);
    }
}