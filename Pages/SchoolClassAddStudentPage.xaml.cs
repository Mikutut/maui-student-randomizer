using StudentRandomizer.Models;
using StudentRandomizer.Services.SchoolClasses;
using StudentRandomizer.Services.Students;
using StudentRandomizer.Services.Students.Inputs;
using StudentRandomizer.Enums.Sorting;
using StudentRandomizer.Enums.Students;

namespace StudentRandomizer.Pages;

[QueryProperty(nameof(SchoolClass), nameof(SchoolClass))]
public partial class SchoolClassAddStudentPage : ContentPage
{
	private readonly ISchoolClassDataService _schoolClassDataService;
	private readonly IStudentDataService _studentDataService;
	private List<Student> students = new List<Student>();
	private SchoolClass schoolClass;
	private Student selectedStudent;

	public SchoolClass SchoolClass
	{
		get => schoolClass;
		set
		{
			schoolClass = value;
			OnPropertyChanged("SchoolClass");
			LoadStudents();
		}
	}

	public List<Student> Students
	{
		get => students;
		set
		{
			students = value;
			OnPropertyChanged("Students");
		}
	}

	public SchoolClassAddStudentPage(ISchoolClassDataService schoolClassDataService,
							   IStudentDataService studentDataService)
	{
		_schoolClassDataService = schoolClassDataService;
		_studentDataService = studentDataService;
		InitializeComponent();
	}

	private void LoadStudents()
	{
		Students = _studentDataService.GetAll(new GetAllStudentsInput()
		{
			Sorting = new SortStudentsInput()
			{
				SortBy = SortStudentsBy.LastName,
				Direction = SortingDirection.Ascending
			}
		})
			.ToList()
			.Except(SchoolClass.Students
				.Select(y => y.Student!))
			.ToList();

		schoolClassAddStudentPage_studentPicker.ItemsSource = Students;
	}

	private async void schoolClassAddStudentPage_cancelButton_Clicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("//schoolClasses/list");
    }

	private async void schoolClassAddStudentPage_submitButton_Clicked(object sender, EventArgs e)
	{
		if(selectedStudent == null)
		{
			await DisplayAlert("Dodawanie ucznia", "Nie wybrano żadnego ucznia.", "OK");
			return;
		}

		try
		{
			_schoolClassDataService.AddStudent(SchoolClass.SchoolClassRefId, selectedStudent.StudentRefId);
		}
		catch(Exception ex)
		{
			await DisplayAlert("Dodawanie ucznia", $"Nie udało się dodać ucznia.\n{ex.Message}\n{ex.InnerException?.Message}", "OK");
		}

		await Shell.Current.GoToAsync("//schoolClasses/list");
    }

	private void schoolClassAddStudentPage_studentPicker_SelectedIndexChanged(object sender, EventArgs e)
	{
		var picker = (Picker)sender;
		var selectedIndex = picker.SelectedIndex;

		if(selectedIndex != -1)
		{
			selectedStudent = (Student) picker.SelectedItem;
		}
	}
}