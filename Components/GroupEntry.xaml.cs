using StudentRandomizer.Models;
using System.ComponentModel;

namespace StudentRandomizer.Components;

public partial class GroupEntry : ContentView, INotifyPropertyChanged
{
	public static BindableProperty GroupProperty = BindableProperty.Create(
		"Group",
		typeof(Group),
		typeof(GroupEntry),
		null);

	private int studentsCount = 0;

	public Group Group
	{
		get => (Group)GetValue(GroupProperty);
		set
		{
			SetValue(GroupProperty, value);
			OnPropertyChanged("Group");
			ReloadStudentsCount();
		}
	}

	public int StudentsCount
	{
		get => studentsCount;
		set
		{
			studentsCount = value;
			OnPropertyChanged("StudentsCount");
		}
	}

	public event Action<Group>? OnDelete;
	public event Action<Group>? OnUpdate;
	public event Action<Group>? OnAddStudent;
	public event Action<Group>? OnNewRoll;

	public GroupEntry()
	{
		InitializeComponent();
	}

	public void ReloadStudentsCount()
	{
		StudentsCount = Group.Students?.AsEnumerable()
			.Count() ?? 0;
	}

	private void groupEntry_deleteButton_Clicked(object sender, EventArgs e)
	{
		OnDelete?.Invoke(Group);
	}

	private void groupEntry_updateButton_Clicked(object sender, EventArgs e)
	{
		OnUpdate?.Invoke(Group);
	}

	private void groupEntry_addStudentButton_Clicked(object sender, EventArgs e)
	{
		OnAddStudent?.Invoke(Group);
	}

	private void groupClassEntry_rollStudentButton_Clicked(object sender, EventArgs e)
	{
		OnNewRoll?.Invoke(Group);
    }
}