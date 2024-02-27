using StudentRandomizer.Models;
using StudentRandomizer.Services.Common;
using StudentRandomizer.Services.Groups;
using StudentRandomizer.Services.SchoolClasses;

namespace StudentRandomizer.Pages;

[QueryProperty(nameof(Group), nameof(Group))]
[QueryProperty(nameof(SchoolClass), nameof(SchoolClass))]
public partial class NewRollPage : ContentPage
{
	private readonly IGroupDataService _groupDataService;
	private readonly ISchoolClassDataService _schoolClassDataService;
	private readonly IRollManagementService<Group> _groupRollManagementService;
	private readonly IRollManagementService<SchoolClass> _schoolClassRollManagementService;

	private Group? group = null;
	private Models.GroupEntry? groupEntry = null;
	private SchoolClass? schoolClass = null;
	private Models.SchoolClassEntry? schoolClassEntry = null;
	private uint? orderNumber = null;
	private Student student;

	public Group? Group
	{
		get => group;
		set
		{
			group = value;
			OnPropertyChanged("Group");
			
			if(group != null)
			{
				RollStudentFromGroup();
			}
		}
	}

	public Models.GroupEntry? GroupEntry
	{
		get => groupEntry;
		set
		{
			groupEntry = value;
			OnPropertyChanged("GroupEntry");
		}
	}

	public SchoolClass? SchoolClass
	{
		get => schoolClass;
		set
		{
			schoolClass = value;
			OnPropertyChanged("SchoolClass");

			if(schoolClass != null)
			{
				RollStudentFromClass();
			}
		}
	}

	public Models.SchoolClassEntry? SchoolClassEntry
	{
		get => schoolClassEntry;
		set
		{
			schoolClassEntry = value;
			OnPropertyChanged("GroupEntry");
		}
	}

	public Student Student
	{
		get => student;
		set
		{
			student = value;
			OnPropertyChanged("Student");
		}
	}

	public uint? OrderNumber
	{
		get => orderNumber;
		set
		{
			orderNumber = value;
			OnPropertyChanged("OrderNumber");
		}
	}

	public NewRollPage(IRollManagementService<Group> groupRollManagementService,
					   IRollManagementService<SchoolClass> schoolClassRollManagementService)
	{
		_groupRollManagementService = groupRollManagementService;
		_schoolClassRollManagementService = schoolClassRollManagementService;
		InitializeComponent();
	}

	private void RollStudentFromClass()
	{
		var roll = _schoolClassRollManagementService.NewRoll(SchoolClass.SchoolClassRefId);
		var studentRoll = SchoolClass.Students
			.FirstOrDefault(x => x.OrderNumber == (uint)roll.Value);

		if(studentRoll == null)
		{
			DisplayAlert("Losowanie ucznia", "Brak uczniów do losowania", "OK");
			Shell.Current.GoToAsync("//schoolClasses/list");
			return;
		}

		Student = studentRoll?.Student;
		SchoolClassEntry = studentRoll;
		OrderNumber = studentRoll.OrderNumber;
	}

	private void RollStudentFromGroup()
	{
		var roll = _groupRollManagementService.NewRoll(Group.GroupRefId);
		var studentRoll = Group.Students
			.FirstOrDefault(x => x.OrderNumber == (uint)roll.Value);

		if(studentRoll == null)
		{
			DisplayAlert("Losowanie ucznia", "Brak uczniów do losowania", "OK");
			Shell.Current.GoToAsync("//groups/list");
			return;
		}

		Student = studentRoll.Student;
		GroupEntry = studentRoll;
		OrderNumber = studentRoll.OrderNumber;
	}

	private async void newRollPage_homeToolbarItem_Clicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("//home");
    }

	private async void newRollPage_rerollButton_Clicked(object obj)
	{
		if(SchoolClass != null)
		{
			RollStudentFromClass();
		}
		else if(Group != null)
		{
			RollStudentFromGroup();
		}
		else
		{
			await DisplayAlert("Losowanie ucznia", "Nie można wylosować ucznia z nieznanego powodu.", "OK");
		}
	}
}