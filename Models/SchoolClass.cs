using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRandomizer.Models
{
	public class SchoolClass : INotifyPropertyChanged
	{
		private long id;
		private Guid schoolClassRefId = Guid.NewGuid();
		private string name = string.Empty;
		private DateTime creationDate = DateTime.UtcNow;
		private DateTime? modificationDate;
		private List<SchoolClassEntry> students = new List<SchoolClassEntry>();
		private RollScope? rollScope;
		public long Id 
		{ 
			get => id; 
			set
			{
				id = value;
				OnPropertyChanged("Id");
			}
		}
		public Guid SchoolClassRefId 
		{
			get => schoolClassRefId; 
			set
			{
				schoolClassRefId = value;
				OnPropertyChanged("SchoolClassRefId");
			}
		}
		public string Name 
		{ 
			get => name; 
			set
			{
				name = value;
				OnPropertyChanged("Name");
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
		public List<SchoolClassEntry> Students 
		{
			get => students;
			set
			{
				students = value;
				OnPropertyChanged("Students");
			}
		}
		public RollScope RollScope
		{
			get => rollScope;
			set
			{
				rollScope = value;
				OnPropertyChanged("RollScope");
			}
		}

		public event PropertyChangedEventHandler? PropertyChanged;

		private void OnPropertyChanged(string? propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
