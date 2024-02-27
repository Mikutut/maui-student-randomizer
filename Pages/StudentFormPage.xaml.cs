using StudentRandomizer.Models;
using StudentRandomizer.Services.Students;
using StudentRandomizer.Services.Students.Inputs;

namespace StudentRandomizer.Pages;

[QueryProperty(nameof(Student), "Student")]
public partial class StudentFormPage : ContentPage
{
	private readonly IStudentDataService _studentDataService;

	private Student? student = null;
	private bool IsEdit = false;

	public Student? Student
	{
		get => student;
		set
		{
			student = value;
			OnPropertyChanged("Student");
			SetBindingContext();
		}
	}

	public StudentFormPage(IStudentDataService studentDataService)
	{
		_studentDataService = studentDataService;
		InitializeComponent();
		SetBindingContext();
	}

	private void SetBindingContext()
	{
		if(Student == null)
		{
			BindingContext = new CreateStudentInput();
			IsEdit = false;
		}
		else
		{
			var updateStudent = new UpdateStudentInput();
			updateStudent.StudentRefId = Student.StudentRefId;
			updateStudent.FirstName = Student.FirstName;
			updateStudent.LastName = Student.LastName;
			updateStudent.SchoolClassRefId = Student.Class?
				.SchoolClass?
				.SchoolClassRefId;

			BindingContext = updateStudent;
			IsEdit = true;
		}
	}

	private async void studentFormPage_cancelButton_Clicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("//students/list");
	}

	private async void studentFormPage_submitButton_Clicked(object sender, EventArgs e)
	{
		try
		{
			if(IsEdit)
			{
				_studentDataService.Update((UpdateStudentInput) BindingContext);
			}
			else
			{
				_studentDataService.Create((CreateStudentInput) BindingContext);
			}
		}
		catch(Exception ex)
		{
			await DisplayAlert("Formularz ucznia", $"Operacja nie powiodła się.\n{ex.Message}\n{ex.InnerException?.Message}", "OK");
		}

		await Shell.Current.GoToAsync("//students/list");
	}
}