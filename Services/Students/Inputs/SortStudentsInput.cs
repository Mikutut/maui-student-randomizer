using StudentRandomizer.Enums.Sorting;
using StudentRandomizer.Enums.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRandomizer.Services.Students.Inputs
{
	public class SortStudentsInput
	{
		public SortStudentsBy SortBy { get; set; }
		public SortingDirection Direction { get; set; }
	}
}
