using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRandomizer.Services.SchoolClasses.Inputs
{
	public class UpdateSchoolClassInput
	{
		public Guid SchoolClassRefId { get; set; }
		public string Name { get; set; }
	}
}
