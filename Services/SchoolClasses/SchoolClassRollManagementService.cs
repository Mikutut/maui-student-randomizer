using StudentRandomizer.Models;
using StudentRandomizer.Services.Common;
using StudentRandomizer.Services.LuckyNumbers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRandomizer.Services.SchoolClasses
{
    public class SchoolClassRollManagementService : IRollManagementService<SchoolClass>
	{
		private readonly ISchoolClassDataService _schoolClassDataService;
		private readonly ILuckyNumberDataService _luckyNumberDataService;
		private readonly IRepository<RollScope> _rollScopeRepository;

		public SchoolClassRollManagementService(ISchoolClassDataService schoolClassDataService,
												ILuckyNumberDataService luckyNumberDataService,
												IRepository<RollScope> rollScopeRepository)
		{
			_schoolClassDataService = schoolClassDataService;
			_luckyNumberDataService = luckyNumberDataService;
			_rollScopeRepository = rollScopeRepository;
		}

		public Roll GetRoll(Guid rollScopeOwnerRefId, Guid rollRefId)
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

		public ICollection<Roll> GetCurrentRolls(Guid rollScopeOwnerRefId)
		{
			var schoolClass = _schoolClassDataService.Get(rollScopeOwnerRefId);

			return (ICollection<Roll>) schoolClass.RollScope.Rolls;
		}

		public RollScope GetRollScope(Guid rollScopeOwnerRefId)
		{
			var schoolClass = _schoolClassDataService.Get(rollScopeOwnerRefId);

			return schoolClass.RollScope;
		}

		public Roll NewRoll(Guid rollScopeOwnerRefId)
		{
			var schoolClass = _schoolClassDataService.Get(rollScopeOwnerRefId);
			var rollScope = schoolClass.RollScope;
			var allowedNumbers = GetAllowedOrderNumbers(schoolClass);

			if(allowedNumbers.Count == 0)
			{
				throw new ArgumentException($"No student in class with RefId: '{rollScopeOwnerRefId}' is elligible for rolling.");
			}

			var existingRolls = schoolClass.RollScope.Rolls;
			var highestRollIndexNumber = existingRolls
				.OrderByDescending(x => x.IndexNumber)
				.Select(x => x.IndexNumber)
				.FirstOrDefault();

			var rollBoundaries = (
				allowedNumbers.Min(),
				allowedNumbers.Max()
			);

			Random rnd = new Random();
			uint newRollValue = default(uint);

			do
			{
				newRollValue = (uint)rnd.Next((int)rollBoundaries.Item1, (int)rollBoundaries.Item2 + 1);
			}
			while (!allowedNumbers.Any(x => x == newRollValue));

			Roll newRoll = new Roll()
			{
				Value = newRollValue,
				IndexNumber = highestRollIndexNumber + 1
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

		private List<SchoolClassEntry> FilterStudents(SchoolClass schoolClass)
		{
			var studentEntries = schoolClass.Students;

			studentEntries = FilterStudentsByAttendance(schoolClass, studentEntries);
			studentEntries = FilterStudentsByRollTimeout(schoolClass, studentEntries);
			studentEntries = FilterStudentsByLuckyNumber(schoolClass, studentEntries);

			return studentEntries;
		}

		private List<SchoolClassEntry> FilterStudentsByAttendance(SchoolClass schoolClass,
																  List<SchoolClassEntry> studentEntries)
		{
			studentEntries = studentEntries
				.Where(x => x.Student.Attendance
					.FirstOrDefault(y => y.Date.Date.Equals(DateTime.UtcNow.Date))?
					.IsPresent == true)
				.ToList();

			return studentEntries;
		}

		private List<SchoolClassEntry> FilterStudentsByRollTimeout(SchoolClass schoolClass,
																   List<SchoolClassEntry> studentEntries)
		{
			var existingRolls = schoolClass.RollScope.Rolls;
			var highestRollIndexNumber = existingRolls
				.OrderByDescending(x => x.IndexNumber)
				.Select(x => x.IndexNumber)
				.FirstOrDefault();

			if(highestRollIndexNumber == 0)
			{
				return studentEntries;
			}

			var highestRollsPerStudent = existingRolls
				.GroupBy(r => r.Value,
						 r => r,
						 (value, rolls) => new
						 {
							 OrderNumber = value,
							 IndexNumber = rolls.Max(r => r.IndexNumber)
						 });

			var timedOutStudents = studentEntries
				.Where(x => highestRollsPerStudent
					.Any(y => y.OrderNumber == x.OrderNumber
						 && Math.Max(0, highestRollIndexNumber - y.IndexNumber) < 3))
				.ToList();

			studentEntries = studentEntries.Except(timedOutStudents)
				.ToList();

			return studentEntries;
		}

		private List<SchoolClassEntry> FilterStudentsByLuckyNumber(SchoolClass schoolClass,
																   List<SchoolClassEntry> studentEntries)
		{
			var luckyNumber = _luckyNumberDataService.GetOrCreate(DateTime.UtcNow);

			studentEntries = studentEntries
				.Where(x => x.OrderNumber != luckyNumber.Value)
				.ToList();

			return studentEntries;
		}

		private ICollection<uint> GetAllowedOrderNumbers(SchoolClass schoolClass)
		{
			return FilterStudents(schoolClass)
				.Select(x => x.OrderNumber)
				.ToList();
		}
	}
}
