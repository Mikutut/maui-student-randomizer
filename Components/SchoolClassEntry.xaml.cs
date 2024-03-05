using StudentRandomizer.Models;
using System.ComponentModel;

namespace StudentRandomizer.Components;

public partial class SchoolClassEntry : ContentView, INotifyPropertyChanged
{
	public static BindableProperty SchoolClassProperty = BindableProperty.Create(
		"SchoolClass",
		typeof(SchoolClass),
		typeof(SchoolClassEntry),
		null);

	public SchoolClass SchoolClass
	{
		get => (SchoolClass)GetValue(SchoolClassProperty);
		set
		{
			SetValue(SchoolClassProperty, value);
			OnPropertyChanged("SchoolClass");
		}
	}

	public event Action<SchoolClass>? OnDelete;
	public event Action<SchoolClass>? OnUpdate;
	public event Action<SchoolClass>? OnAddStudent;
	public event Action<SchoolClass>? OnCheckAttendance;
	public event Action<SchoolClass>? OnNewRoll;

	public SchoolClassEntry()
	{
		InitializeComponent();
	}

	private void schoolClassEntry_deleteButton_Clicked(object sender, EventArgs e)
	{
		OnDelete?.Invoke(SchoolClass);
	}

	private void schoolClassEntry_updateButton_Clicked(object sender, EventArgs e)
	{
		OnUpdate?.Invoke(SchoolClass);
	}

	private void schoolClassEntry_addStudentButton_Clicked(object sender, EventArgs e)
	{
		OnAddStudent?.Invoke(SchoolClass);
	}

	private void schoolClassEntry_checkAttendanceButton_Clicked(object sender, EventArgs e)
	{
		OnCheckAttendance?.Invoke(SchoolClass);
	}

	private void schoolClassEntry_rollStudentButton_Clicked(object sender, EventArgs e)
	{
		OnNewRoll?.Invoke(SchoolClass);
    }
}