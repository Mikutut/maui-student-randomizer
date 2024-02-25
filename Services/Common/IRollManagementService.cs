using StudentRandomizer.Interfaces;
using StudentRandomizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRandomizer.Services.Common
{
    public interface IRollManagementService<T>
		where T: class
	{
		ICollection<IRoll> GetAllRolls(Guid rollScopeOwnerRefId);
		ICollection<IRoll> GetCurrentRolls(Guid rollScopeOwnerRefId);
		ICollection<IRoll> GetArchivalRolls(Guid rollScopeOwnerRefId);
		IRoll NewRoll(Guid rollScopeOwnerRefId);
		IRoll GetRoll(Guid rollScopeOwnerRefId, Guid rollRefId);
		void WipeCurrentRolls(Guid rollScopeOwnerRefId);
		void WipeArchivalRolls(Guid rollScopeOwnerRefId);
		void WipeRolls(Guid rollScopeOwnerRefId);
		void ArchiveCurrentRolls(Guid rollScopeOwnerRefId);
		RollScope GetRollScope(Guid rollScopeOwnerRefId);
	}
}
