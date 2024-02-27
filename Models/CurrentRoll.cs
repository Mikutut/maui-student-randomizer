using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentRandomizer.Interfaces;

namespace StudentRandomizer.Models
{
    public class CurrentRoll : IRoll, INotifyPropertyChanged
	{
		private long id;
		private Guid rollRefId = Guid.NewGuid();
		private RollScope scope;
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

		public static ArchivalRoll ToArchival(CurrentRoll roll)
		{
			return new ArchivalRoll()
			{
				RollRefId = roll.RollRefId,
				RollScopeId = roll.RollScopeId,
				Scope = roll.Scope,
				Value = roll.Value
			};
		}

		private void OnPropertyChanged(string? propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
