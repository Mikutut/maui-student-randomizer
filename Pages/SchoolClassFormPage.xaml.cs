using StudentRandomizer.Models;
using StudentRandomizer.Services.SchoolClasses;
using StudentRandomizer.Services.SchoolClasses.Inputs;

namespace StudentRandomizer.Pages;

[QueryProperty(nameof(SchoolClass), "SchoolClass")]
public partial class SchoolClassFormPage : ContentPage
{
	private readonly ISchoolClassDataService _schoolClassDataService;

	private SchoolClass? schoolClass = null;
	private bool IsEdit = false;

	public SchoolClass? SchoolClass
	{
		get => schoolClass;
		set
		{
			schoolClass = value;
			OnPropertyChanged("SchoolClass");
			SetBindingContext();
		}
	}

	public SchoolClassFormPage(ISchoolClassDataService schoolClassDataService)
	{
		_schoolClassDataService = schoolClassDataService;
		InitializeComponent();
		SetBindingContext();
	}

	private void SetBindingContext()
	{
		if(SchoolClass == null)
		{
			BindingContext = new CreateSchoolClassInput();
			IsEdit = false;
		}
		else
		{
			var updateStudent = new UpdateSchoolClassInput();
			updateStudent.SchoolClassRefId = SchoolClass.SchoolClassRefId;
			updateStudent.Name = SchoolClass.Name;

			BindingContext = updateStudent;
			IsEdit = true;
		}
	}

	private async void schoolClassFormPage_cancelButton_Clicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("//schoolClasses/list");
	}

	private async void schoolClassFormPage_submitButton_Clicked(object sender, EventArgs e)
	{
		try
		{
			if(IsEdit)
			{
				_schoolClassDataService.Update((UpdateSchoolClassInput) BindingContext);
			}
			else
			{
				_schoolClassDataService.Create((CreateSchoolClassInput) BindingContext);
			}
		}
		catch(Exception ex)
		{
			await DisplayAlert("Formularz klasy", $"Operacja nie powiodła się.\n{ex.Message}\n{ex.InnerException?.Message}", "OK");
		}

		await Shell.Current.GoToAsync("//schoolClasses/list");
	}
}