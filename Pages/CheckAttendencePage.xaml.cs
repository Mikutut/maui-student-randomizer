using StudentRandomizer.Models;
using StudentRandomizer.Services.Common;
using System.Collections.ObjectModel;

namespace StudentRandomizer.Pages;

[QueryProperty(nameof(SchoolClass), nameof(SchoolClass))]
public partial class CheckAttendencePage : ContentPage
{
	private readonly IRepository<AttendanceRecord> _attendances;

	private SchoolClass schoolClass;
	public SchoolClass SchoolClass
	{
		get => schoolClass;
		set
		{
			schoolClass = value;
			OnPropertyChanged("SchoolClass");

			if(schoolClass != null)
			{
				ReloadAttendance();
			}
		}
	}

	private ObservableCollection<AttendanceRecord> attendanceRecords = new ObservableCollection<AttendanceRecord>();
	public ObservableCollection<AttendanceRecord> Attendance
	{
		get => attendanceRecords;
		set
		{
			attendanceRecords = value;
			OnPropertyChanged("Attendance");
		}
	}

	public CheckAttendencePage(IRepository<AttendanceRecord> attendances)
	{
		_attendances = attendances;
		InitializeComponent();
	}

	private void ReloadAttendance()
	{
		var students = SchoolClass.Students
			.Select(x => x.Student)
			.ToList();

		Attendance.Clear();
		foreach(AttendanceRecord record in students
			.SelectMany(y => y.Attendance)
			.Where(x => x.Date.Date.Equals(DateTime.UtcNow.Date))
			.ToList())
		{
			Attendance.Add(record);
		}

		if(Attendance.Count == 0 && students.Count != 0)
		{
			foreach(Student student in students)
			{
				var attendance = new AttendanceRecord()
				{
					StudentId = student.Id,
					Student = student,
					IsPresent = false
				};
				Attendance.Add(attendance);
				_attendances.Insert(attendance);
			}
			_attendances.SaveChanges();
		}
	}

	private async void checkAttendancePage_cancelButton_Clicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("//schoolClasses/list");
	}

	private async void checkAttendancePage_submitButton_Clicked(object sender, EventArgs e)
	{
		foreach(AttendanceRecord attendanceRecord in Attendance)
		{
			_attendances.Update(attendanceRecord);
		}
		_attendances.SaveChanges();

		await Shell.Current.GoToAsync("//schoolClasses/list");
	}

	private void checkAttendancePage_recordEntry_OnUpdate(AttendanceRecord obj)
	{
		var currentAttendanceRecord = Attendance
			.First(x => x.AttendanceRecordRefId.Equals(obj.AttendanceRecordRefId));

		currentAttendanceRecord.IsPresent = obj.IsPresent;
	}
}