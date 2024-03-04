using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRandomizer.Models
{
    public class Roll : INotifyPropertyChanged
	{
		private long id;
		private Guid rollRefId = Guid.NewGuid();
		private RollScope scope;
		private long indexNumber;
		private long value;
		private DateTime creationDate = DateTime.UtcNow;
		public long Id 
		{ 
			get => id; 
			set
			{
				id = value;
				OnPropertyChanged("Id");
			}
		}
		public Guid RollRefId 
		{
			get => rollRefId; 
			set
			{
				rollRefId = value;
				OnPropertyChanged("RollRefId");
			}
		}
		public long RollScopeId { get; set; }
		public RollScope Scope 
		{ 
			get => scope; 
			set
			{
				scope = value;
				OnPropertyChanged("Scope");
			}
		}
		public long IndexNumber 
		{ 
			get => indexNumber;
			set
			{
				this.indexNumber = value;
				OnPropertyChanged("IndexNumber");
			} 
		}
		public long Value 
		{ 
			get => value;
			set
			{
				this.value = value;
				OnPropertyChanged("Value");
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
		public event PropertyChangedEventHandler? PropertyChanged;

		private void OnPropertyChanged(string? propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
