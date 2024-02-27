using StudentRandomizer.Models;
using System.ComponentModel;

namespace StudentRandomizer.Components;

public partial class StudentEntry : ContentView, INotifyPropertyChanged
{
	public static BindableProperty StudentProperty = BindableProperty.Create(
		"Student",
		typeof(Student),
		typeof(StudentEntry),
		null);

	public Student Student
	{
		get => (Student)GetValue(StudentProperty);
		set
		{
			SetValue(StudentProperty, value);
			OnPropertyChanged("Student");
		}
	}

	public event Action<Student>? OnDelete;
	public event Action<Student>? OnUpdate;

	public StudentEntry()
	{
		InitializeComponent();
		BindingContext = Student;
	}

	private void studentEntry_deleteButton_Clicked(object sender, EventArgs e)
	{
		OnDelete?.Invoke(Student);
	}

	private void studentEntry_updateButton_Clicked(object sender, EventArgs e)
	{
		OnUpdate?.Invoke(Student);
	}
}