using StudentRandomizer.Models;
using StudentRandomizer.Services.Common;
using StudentRandomizer.Services.Groups.Inputs;
using StudentRandomizer.Services.SchoolClasses.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRandomizer.Services.Groups
{
	public interface IGroupDataService
	{
		ICollection<Group> GetAll(GetAllGroupsInput input);
		Group Get(Guid groupRefId);
		void AddStudent(Guid groupRefId, Guid studentRefId);
		void RemoveStudent(Guid groupRefId, Guid studentRefId);
		Group Create(CreateGroupInput input);
		Group Update(UpdateGroupInput input);
		void Delete(Guid groupRefId);
	}
}
