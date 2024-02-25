using StudentRandomizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRandomizer.Services.Students.Inputs
{
	public class GetAllStudentsInput
	{
		public SchoolClass? SchoolClass { get; set; }
		public SortStudentsInput? Sorting { get; set; }
	}
}
