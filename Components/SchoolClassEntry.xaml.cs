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
			OnPropertyChanged("StudentsCount");
		}
	}

	public int StudentsCount
	{
		get => SchoolClass?.Students.Count ?? 0;
	}

	public event Action<SchoolClass>? OnDelete;
	public event Action<SchoolClass>? OnUpdate;
	public event Action? OnAddStudent;

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
		OnAddStudent?.Invoke();
	}
}