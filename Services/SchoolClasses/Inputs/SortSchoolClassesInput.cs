using StudentRandomizer.Enums.SchoolClasses;
using StudentRandomizer.Enums.Sorting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRandomizer.Services.SchoolClasses.Inputs
{
	public class SortSchoolClassesInput
	{
		public SortSchoolClassesBy SortBy { get; set; }
		public SortingDirection Direction { get; set; }
	}
}
