using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRandomizer.Models
{
	public class Group : INotifyPropertyChanged
	{
		private long id;
		private Guid groupRefId = Guid.NewGuid();
		private string name = string.Empty;
		private List<GroupEntry> students = new List<GroupEntry>();
		private RollScope? rollScope = null;
		private DateTime creationDate = DateTime.UtcNow;
		private DateTime? modificationDate;

		public event PropertyChangedEventHandler? PropertyChanged;

		public long Id
		{
			get => id;
			set
			{
				id = value;
				OnPropertyChanged("Id");
			}
		}
		public Guid GroupRefId 
		{
			get => groupRefId;
			set
			{
				groupRefId = value;
				OnPropertyChanged("GroupRefId");
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
		public List<GroupEntry> Students 
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
				ModificationDate = value;
				OnPropertyChanged("ModificationDate");
			}
		}

		private void OnPropertyChanged(string? propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
