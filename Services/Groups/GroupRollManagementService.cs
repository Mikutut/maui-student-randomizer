using StudentRandomizer.Interfaces;
using StudentRandomizer.Models;
using StudentRandomizer.Services.Common;
using StudentRandomizer.Services.LuckyNumbers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRandomizer.Services.Groups
{
    public class GroupRollManagementService : IRollManagementService<Group>
	{
		private readonly IGroupDataService _groupDataService;
		private readonly ILuckyNumberDataService _luckyNumberDataService;
		private readonly IRepository<RollScope> _rollScopeRepository;

		public GroupRollManagementService(IGroupDataService groupDataService,
										  ILuckyNumberDataService luckyNumberDataService,
										  IRepository<RollScope> rollScopeRepository)
		{
			_groupDataService = groupDataService;
			_luckyNumberDataService = luckyNumberDataService;
			_rollScopeRepository = rollScopeRepository;
		}

		public IRoll GetRoll(Guid rollScopeOwnerRefId, Guid rollRefId)
		{
			var group = _groupDataService.Get(rollScopeOwnerRefId);

			var roll = group.RollScope.Rolls
				.FirstOrDefault(x => x.RollRefId.Equals(rollRefId));

			if(roll == null)
			{
				throw new ApplicationException($"Roll with RefId: '{rollRefId}' could not be found.");
			}

			return roll;
		}

		public ICollection<IRoll> GetCurrentRolls(Guid rollScopeOwnerRefId)
		{
			var group = _groupDataService.Get(rollScopeOwnerRefId);

			return (ICollection<IRoll>) group.RollScope.Rolls;
		}

		public ICollection<IRoll> GetArchivalRolls(Guid rollScopeOwnerRefId)
		{
			var group = _groupDataService.Get(rollScopeOwnerRefId);

			return (ICollection<IRoll>) group.RollScope.ArchivalRolls;
		}

		public ICollection<IRoll> GetAllRolls(Guid rollScopeOwnerRefId)
		{
			var group = _groupDataService.Get(rollScopeOwnerRefId);

			ICollection<IRoll> rolls = group.RollScope.Rolls
				.Select(x => (IRoll) x)
				.Concat(group.RollScope.ArchivalRolls
					.Select(x => (IRoll)x))
				.ToList();

			return rolls;
		}

		public RollScope GetRollScope(Guid rollScopeOwnerRefId)
		{
			var group = _groupDataService.Get(rollScopeOwnerRefId);

			return group.RollScope;
		}

		public IRoll NewRoll(Guid rollScopeOwnerRefId)
		{
			var group = _groupDataService.Get(rollScopeOwnerRefId);
			var rollScope = group.RollScope;
			var luckyNumber = _luckyNumberDataService.GetOrCreate(DateTime.UtcNow);

			var allowedNumbers = GetAllowedOrderNumbers(group);

			var existingRolls = group.RollScope.Rolls;

			if (existingRolls.Count >= group.Students.Count)
			{
				ArchiveCurrentRolls(rollScopeOwnerRefId);
				existingRolls = group.RollScope.Rolls;
			}

			var rollBoundaries = (
				allowedNumbers.OrderByDescending(x => x)
					.Last(),
				allowedNumbers.OrderByDescending(x => x)
					.First()
				);

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
				|| newRollValue == luckyNumber.Value
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
			var group = _groupDataService.Get(rollScopeOwnerRefId);

			var rollScope = group.RollScope;

			rollScope.Rolls.Clear();

			_rollScopeRepository.Update(rollScope);
			_rollScopeRepository.SaveChanges();
		}

		public void WipeArchivalRolls(Guid rollScopeOwnerRefId)
		{
			var group = _groupDataService.Get(rollScopeOwnerRefId);

			var rollScope = group.RollScope;

			rollScope.ArchivalRolls.Clear();

			_rollScopeRepository.Update(rollScope);
			_rollScopeRepository.SaveChanges();
		}

		public void WipeRolls(Guid rollScopeOwnerRefId)
		{
			var group = _groupDataService.Get(rollScopeOwnerRefId);

			var rollScope = group.RollScope;

			rollScope.Rolls.Clear();
			rollScope.ArchivalRolls.Clear();

			_rollScopeRepository.Update(rollScope);
			_rollScopeRepository.SaveChanges();
		}

		public void ArchiveCurrentRolls(Guid rollScopeOwnerRefId)
		{
			var group = _groupDataService.Get(rollScopeOwnerRefId);

			var rollScope = group.RollScope;

			foreach(CurrentRoll roll in rollScope.Rolls)
			{
				rollScope.ArchivalRolls.Add(CurrentRoll.ToArchival(roll));
			}

			rollScope.Rolls.Clear();

			_rollScopeRepository.Update(rollScope);
			_rollScopeRepository.SaveChanges();
		}

		private List<GroupEntry> FilterStudents(Group group)
		{
			var studentEntries = group.Students;

			studentEntries = studentEntries
				.Where(x => x.Student.Attendance
					.FirstOrDefault(y => y.Date.Date.Equals(DateTime.UtcNow))?
					.IsPresent == true)
				.ToList();

			return studentEntries;
		}

		private List<uint> GetAllowedOrderNumbers(Group group)
		{
			return FilterStudents(group)
				.Select(x => x.OrderNumber)
				.ToList();
		}
	}
}
