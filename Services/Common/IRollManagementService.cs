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
		ICollection<Roll> GetCurrentRolls(Guid rollScopeOwnerRefId);
		Roll NewRoll(Guid rollScopeOwnerRefId);
		Roll GetRoll(Guid rollScopeOwnerRefId, Guid rollRefId);
		void WipeCurrentRolls(Guid rollScopeOwnerRefId);
		RollScope GetRollScope(Guid rollScopeOwnerRefId);
	}
}
