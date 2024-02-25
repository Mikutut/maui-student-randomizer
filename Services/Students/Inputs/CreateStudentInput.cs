using StudentRandomizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRandomizer.Services.Students.Inputs
{
	public class CreateStudentInput
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public Guid? SchoolClassRefId { get; set; }
	}
}
