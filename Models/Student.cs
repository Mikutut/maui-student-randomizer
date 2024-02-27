using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRandomizer.Models
{
	public class Student : INotifyPropertyChanged
	{
		private long id;
		private Guid studentRefId = Guid.NewGuid();
		private string firstName = string.Empty;
		private string lastName = string.Empty;
		private SchoolClassEntry? schoolClass;
		private List<GroupEntry> groups = new List<GroupEntry>();
		private DateTime creationDate = DateTime.UtcNow;
		private DateTime? modificationDate;
		private List<AttendanceRecord> attendance = new List<AttendanceRecord>();

		public long Id 
		{
			get => id;
			set
			{
				id = value;
				OnPropertyChanged("Id");
			}
		}
		public Guid StudentRefId 
		{ 
			get => studentRefId; 
			set
			{
				studentRefId = value;
				OnPropertyChanged("StudentRefId");
			}
		}
		public string FirstName 
		{
			get => firstName;
			set
			{
				firstName = value;
				OnPropertyChanged("FirstName");
			}
		}
		public string LastName 
		{
			get => lastName;
			set
			{
				lastName = value;
				OnPropertyChanged("LastName");
			}
		}
		public string FullName
		{
			get
			{
				return FirstName + " " + LastName;
			}
		}
		public SchoolClassEntry? Class 
		{
			get => schoolClass;
			set
			{
				schoolClass = value;
				OnPropertyChanged("SchoolClassEntry");
			}
		}
		public List<GroupEntry> Groups 
		{
			get => groups;
			set
			{
				groups = value;
				OnPropertyChanged("Groups");
			}
		}

		public List<AttendanceRecord> Attendance
		{
			get => attendance;
			set
			{
				attendance = value;
				OnPropertyChanged("Attendance");
			}
		}
		public DateTime CreationDate 
		{
			get => creationDate;
			set
			{
				creationDate = value;
				OnPropertyChanged("CreationDate");
			}
		}
		public DateTime? ModificationDate 
		{ 
			get => modificationDate;
			set
			{
				modificationDate = value;
				OnPropertyChanged("ModificationDate");
			}
		}

		public event PropertyChangedEventHandler? PropertyChanged;

		private void OnPropertyChanged(string? propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
