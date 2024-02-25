using StudentRandomizer.Interfaces;
using StudentRandomizer.Models;
using StudentRandomizer.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRandomizer.Services.SchoolClasses
{
    public class SchoolClassRollManagementService : IRollManagementService<SchoolClass>
	{
		private readonly ISchoolClassDataService _schoolClassDataService;
		private readonly IRepository<RollScope> _rollScopeRepository;

		public SchoolClassRollManagementService(ISchoolClassDataService schoolClassDataService,
												IRepository<RollScope> rollScopeRepository)
		{
			_schoolClassDataService = schoolClassDataService;
			_rollScopeRepository = rollScopeRepository;
		}

		public IRoll GetRoll(Guid rollScopeOwnerRefId, Guid rollRefId)
		{
			var schoolClass = _schoolClassDataService.Get(rollScopeOwnerRefId);

			var roll = schoolClass.RollScope.Rolls
				.FirstOrDefault(x => x.RollRefId.Equals(rollRefId));

			if(roll == null)
			{
				throw new ApplicationException($"Roll with RefId: '{rollRefId}' could not be found.");
			}

			return roll;
		}

		public ICollection<IRoll> GetCurrentRolls(Guid rollScopeOwnerRefId)
		{
			var schoolClass = _schoolClassDataService.Get(rollScopeOwnerRefId);

			return (ICollection<IRoll>) schoolClass.RollScope.Rolls;
		}

		public ICollection<IRoll> GetArchivalRolls(Guid rollScopeOwnerRefId)
		{
			var schoolClass = _schoolClassDataService.Get(rollScopeOwnerRefId);

			return (ICollection<IRoll>) schoolClass.RollScope.ArchivalRolls;
		}

		public ICollection<IRoll> GetAllRolls(Guid rollScopeOwnerRefId)
		{
			var schoolClass = _schoolClassDataService.Get(rollScopeOwnerRefId);

			ICollection<IRoll> rolls = schoolClass.RollScope.Rolls
				.Select(x => (IRoll) x)
				.Concat(schoolClass.RollScope.ArchivalRolls
					.Select(x => (IRoll)x))
				.ToList();

			return rolls;
		}

		public RollScope GetRollScope(Guid rollScopeOwnerRefId)
		{
			var schoolClass = _schoolClassDataService.Get(rollScopeOwnerRefId);

			return schoolClass.RollScope;
		}

		public IRoll NewRoll(Guid rollScopeOwnerRefId)
		{
			var schoolClass = _schoolClassDataService.Get(rollScopeOwnerRefId);
			var rollScope = schoolClass.RollScope;

			var rollBoundaries = GetRollBoundaries(schoolClass);

			var existingRolls = schoolClass.RollScope.Rolls;

			if (existingRolls.Count >= rollBoundaries.Item2)
			{
				ArchiveCurrentRolls(rollScopeOwnerRefId);
				existingRolls = schoolClass.RollScope.Rolls;
			}

			uint newRollValue = default(uint);
			Random rnd = new Random();
			do
			{
				newRollValue = (uint)rnd.Next((int)rollBoundaries.Item1, (int)rollBoundaries.Item2 + 1);
			}
			while (existingRolls
				.Select(x => x.Value)
				.ToList()
				.Contains(newRollValue)
			);

			CurrentRoll newRoll = new CurrentRoll()
			{
				Value = newRollValue
			};

			rollScope.Rolls.Add(newRoll);
			_rollScopeRepository.Update(rollScope);
			_rollScopeRepository.SaveChanges();

			return newRoll;
		}

		public void WipeCurrentRolls(Guid rollScopeOwnerRefId)
		{
			var schoolClass = _schoolClassDataService.Get(rollScopeOwnerRefId);

			var rollScope = schoolClass.RollScope;

			rollScope.Rolls.Clear();

			_rollScopeRepository.Update(rollScope);
			_rollScopeRepository.SaveChanges();
		}

		public void WipeArchivalRolls(Guid rollScopeOwnerRefId)
		{
			var schoolClass = _schoolClassDataService.Get(rollScopeOwnerRefId);

			var rollScope = schoolClass.RollScope;

			rollScope.ArchivalRolls.Clear();

			_rollScopeRepository.Update(rollScope);
			_rollScopeRepository.SaveChanges();
		}

		public void WipeRolls(Guid rollScopeOwnerRefId)
		{
			var schoolClass = _schoolClassDataService.Get(rollScopeOwnerRefId);

			var rollScope = schoolClass.RollScope;

			rollScope.Rolls.Clear();
			rollScope.ArchivalRolls.Clear();

			_rollScopeRepository.Update(rollScope);
			_rollScopeRepository.SaveChanges();
		}

		public void ArchiveCurrentRolls(Guid rollScopeOwnerRefId)
		{
			var schoolClass = _schoolClassDataService.Get(rollScopeOwnerRefId);

			var rollScope = schoolClass.RollScope;

			foreach(IRoll roll in rollScope.Rolls)
			{
				rollScope.ArchivalRolls.Add((ArchivalRoll) roll);
			}

			rollScope.Rolls.Clear();

			_rollScopeRepository.Update(rollScope);
			_rollScopeRepository.SaveChanges();
		}

		private (uint, uint) GetRollBoundaries(SchoolClass schoolClass)
		{
			var lastOrderIndex = schoolClass.Students
				.OrderByDescending(x => x.OrderNumber)
				.Select(x => x.OrderNumber)
				.FirstOrDefault();

			if(lastOrderIndex == 0)
			{
				lastOrderIndex = 1;
			}

			return (1, lastOrderIndex);
		}
	}
}
