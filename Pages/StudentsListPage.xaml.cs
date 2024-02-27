using StudentRandomizer.Models;
using StudentRandomizer.Services.Students;
using StudentRandomizer.Services.Students.Inputs;

namespace StudentRandomizer.Pages;

public partial class StudentsListPage : ContentPage
{
	private readonly IStudentDataService _studentDataService;

	private List<Student> students = new List<Student>();

	public List<Student> Students
	{
		get => students;
		set
		{
			students = value;
			OnPropertyChanged("Students");
		}
	}

	public StudentsListPage(IStudentDataService studentDataService)
	{
		_studentDataService = studentDataService;
		RefreshStudentsList();
		InitializeComponent();

		BindingContext = this;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		RefreshStudentsList();
	}

	private void RefreshStudentsList()
	{
		Students = _studentDataService.GetAll(new GetAllStudentsInput())
			.ToList();
	}

	private async void studentsListPage_homeToolbarItem_Clicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("//home");
	}

	private async void StudentEntry_OnDelete(Student obj)
	{
		var answer = await DisplayAlert("Usuwanie ucznia", "Czy na pewno chcesz usunąć tego ucznia", "Tak", "Nie");

		if(answer)
		{
			try
			{
				_studentDataService.Delete(obj.StudentRefId);
			}
			catch
			{
				await DisplayAlert("Usuwanie ucznia", "Nie udało się usunąć ucznia.", "OK");
			}

			RefreshStudentsList();
		}
	}

	private async void StudentEntry_OnUpdate(Student obj)
	{
		var queryParams = new Dictionary<string, object>
		{
			{ "Student", obj }
		};

		await Shell.Current.GoToAsync("//students/form", queryParams);
	}

	private async void studentsListPage_createToolbarItem_Clicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("//students/form");
	}
}