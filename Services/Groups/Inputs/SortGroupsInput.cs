using StudentRandomizer.Enums.SchoolClasses;
using StudentRandomizer.Enums.Sorting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRandomizer.Services.Groups.Inputs
{
	public class SortGroupsInput
	{
		public SortGroupsBy SortBy { get; set; }
		public SortingDirection Direction { get; set; }
	}
}
