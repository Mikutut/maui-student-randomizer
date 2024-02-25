using StudentRandomizer.Models;
using StudentRandomizer.Services.Common;
using StudentRandomizer.Services.SchoolClasses.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRandomizer.Services.SchoolClasses
{
	public interface ISchoolClassDataService
	{
		ICollection<SchoolClass> GetAll(GetAllSchoolClassesInput input);
		SchoolClass Get(Guid schoolClassRefId);
		void AddStudent(Guid schoolClassRefId, Guid studentRefId);
		void RemoveStudent(Guid schoolClassRefId, Guid studentRefId);
		SchoolClass Create(CreateSchoolClassInput input);
		SchoolClass Update(UpdateSchoolClassInput input);
		void Delete(Guid schoolClassRefId);
	}
}
