using StudentRandomizer.Models;
using StudentRandomizer.Services.Groups;
using StudentRandomizer.Services.Students;
using StudentRandomizer.Services.Students.Inputs;
using StudentRandomizer.Enums.Sorting;
using StudentRandomizer.Enums.Students;

namespace StudentRandomizer.Pages;

[QueryProperty(nameof(Group), nameof(Group))]
public partial class GroupAddStudentPage : ContentPage
{
	private readonly IGroupDataService _groupDataService;
	private readonly IStudentDataService _studentDataService;
	private List<Student> students = new List<Student>();
	private Group group;
	private Student selectedStudent;

	public Group Group
	{
		get => group;
		set
		{
			group = value;
			OnPropertyChanged("Group");
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

	public GroupAddStudentPage(IGroupDataService groupDataService,
							   IStudentDataService studentDataService)
	{
		_groupDataService = groupDataService;
		_studentDataService = studentDataService;
		InitializeComponent();
	}

	private async void LoadStudents()
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
			.Except(Group.Students
				.Select(y => y.Student!))
			.ToList();

		groupAddStudentPage_studentPicker.ItemsSource = Students;
	}

	private async void groupAddStudentPage_cancelButton_Clicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("//groups/list");
    }

	private async void groupAddStudentPage_submitButton_Clicked(object sender, EventArgs e)
	{
		if(selectedStudent == null)
		{
			await DisplayAlert("Dodawanie ucznia", "Nie wybrano żadnego ucznia.", "OK");
			return;
		}

		try
		{
			_groupDataService.AddStudent(Group.GroupRefId, selectedStudent.StudentRefId);
		}
		catch(Exception ex)
		{
			await DisplayAlert("Dodawanie ucznia", $"Nie udało się dodać ucznia.\n{ex.Message}\n{ex.InnerException?.Message}", "OK");
		}

		await Shell.Current.GoToAsync("//groups/list");
    }

	private void groupAddStudentPage_studentPicker_SelectedIndexChanged(object sender, EventArgs e)
	{
		var picker = (Picker)sender;
		var selectedIndex = picker.SelectedIndex;

		if(selectedIndex != -1)
		{
			selectedStudent = (Student) picker.SelectedItem;
		}
	}
}