using StudentRandomizer.Models;

namespace StudentRandomizer.Components;

public partial class AttendanceRecordEntry : ContentView
{
	public static BindableProperty RecordProperty = BindableProperty.Create(
		"Record",
		typeof(AttendanceRecord),
		typeof(AttendanceRecordEntry),
		null);

	public AttendanceRecord Record
	{
		get => (AttendanceRecord)GetValue(RecordProperty);
		set
		{
			SetValue(RecordProperty, value);
			OnPropertyChanged("Record");
		}
	}

	public event Action<AttendanceRecord>? OnUpdate;

	public AttendanceRecordEntry()
	{
		InitializeComponent();
	}

	private void attendanceRecordEntry_Checkbox_CheckedChanged(object sender, CheckedChangedEventArgs e)
	{
		var checkbox = (CheckBox)sender;
		var attendanceRecord = (AttendanceRecord) checkbox.BindingContext;

		attendanceRecord.IsPresent = e.Value;

		OnUpdate?.Invoke(attendanceRecord);
	}
}