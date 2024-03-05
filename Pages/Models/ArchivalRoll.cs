using StudentRandomizer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRandomizer.Pages.Models
{
	public class ArchivalRoll : INotifyPropertyChanged
	{
		private Roll? roll = null;
		private Student? student = null;

		public Roll? Roll 
		{ 
			get => roll; 
			set
			{
				roll = value;
				OnPropertyChanged("Roll");
			}
		}

		public Student? Student
		{
			get => student;
			set
			{
				student = value;
				OnPropertyChanged("Student");
			}
		}

		public event PropertyChangedEventHandler? PropertyChanged;

		private void OnPropertyChanged(string? propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
