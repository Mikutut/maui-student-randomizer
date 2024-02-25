using Microsoft.EntityFrameworkCore;
using StudentRandomizer.Enums.SchoolClasses;
using StudentRandomizer.Enums.Sorting;
using StudentRandomizer.Models;
using StudentRandomizer.Services.Common;
using StudentRandomizer.Services.SchoolClasses.Inputs;
using StudentRandomizer.Services.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRandomizer.Services.SchoolClasses
{
	public class SchoolClassDataService : ISchoolClassDataService
	{
		private readonly IRepository<SchoolClass> _schoolClassRepository;
		private readonly IStudentDataService _studentDataService;

		public SchoolClassDataService(IRepository<SchoolClass> schoolClassRepository,
									  IStudentDataService studentDataService)
		{
			_schoolClassRepository = schoolClassRepository;
			_studentDataService = studentDataService;
		}

		public void AddStudent(Guid schoolClassRefId, Guid studentRefId)
		{
			var student = _studentDataService.Get(studentRefId);

			_studentDataService.AddStudentToClass(student, schoolClassRefId);
		}

		public SchoolClass Create(CreateSchoolClassInput input)
		{
			if (_schoolClassRepository.FirstOrDefault(x => x.Name.Equals(input.Name)) != null)
			{
				throw new ApplicationException($"School class with name: '{input.Name}' already exists.");
			}

			var schoolClass = new SchoolClass()
			{
				Name = input.Name,
				RollScope = new RollScope()
			};

			schoolClass = _schoolClassRepository.Insert(schoolClass);
			_schoolClassRepository.SaveChanges();

			return schoolClass;
		}

		public void Delete(Guid schoolClassRefId)
		{
			var schoolClass = GetBaseSchoolClassesQuery()
				.FirstOrDefault(x => x.SchoolClassRefId.Equals(schoolClassRefId));

			if(schoolClass == null)
			{
				throw new ApplicationException($"School class with RefId: '{schoolClassRefId}' could not be found.");
			}

			_schoolClassRepository.Delete(schoolClass);
			_schoolClassRepository.SaveChanges();
		}

		public SchoolClass Get(Guid schoolClassRefId)
		{
			var schoolClass = GetBaseSchoolClassesQuery()
				.FirstOrDefault(x => x.SchoolClassRefId.Equals(schoolClassRefId));

			if(schoolClass == null)
			{
				throw new ApplicationException($"School class with RefId: '{schoolClassRefId}' could not be found.");
			}

			return schoolClass;
		}

		public ICollection<SchoolClass> GetAll(GetAllSchoolClassesInput input)
		{
			var schoolClassesQuery = GetBaseSchoolClassesQuery();
			FilterAndSortSchoolClasses(schoolClassesQuery, input);

			var schoolClasses = schoolClassesQuery.ToList();
			return schoolClasses;
		}

		public void RemoveStudent(Guid schoolClassRefId, Guid studentRefId)
		{
			var student = _studentDataService.Get(studentRefId);

			_studentDataService.RemoveStudentFromClass(student, schoolClassRefId);
		}

		public SchoolClass Update(UpdateSchoolClassInput input)
		{
			var schoolClass = GetBaseSchoolClassesQuery()
				.FirstOrDefault(x => x.SchoolClassRefId.Equals(input.SchoolClassRefId));

			if(schoolClass == null)
			{
				throw new ApplicationException($"School class with RefId: '{input.SchoolClassRefId}' could not be found.");
			}

			if (_schoolClassRepository.FirstOrDefault(x => x.Name.Equals(input.Name)) != null)
			{
				throw new ApplicationException($"School class with name: '{input.Name}' already exists.");
			}

			schoolClass.Name = input.Name;
			schoolClass = _schoolClassRepository.Update(schoolClass);
			_schoolClassRepository.SaveChanges();

			return schoolClass;
		}

		private IQueryable<SchoolClass> GetBaseSchoolClassesQuery()
		{
			return _schoolClassRepository.GetAll()
				.Include(x => x.Students)
				.ThenInclude(y => y.Student)
				.Include(x => x.RollScope)
				.ThenInclude(y => y.Rolls);
		}

		private void FilterAndSortSchoolClasses(IQueryable<SchoolClass> query,
											    GetAllSchoolClassesInput input)
		{
			if(input.Sorting != null)
			{
				query = input.Sorting.SortBy switch
				{
					SortSchoolClassesBy.Name => input.Sorting.Direction switch
					{
						SortingDirection.Descending => query.OrderByDescending(x => x.Name),
						_ => query.OrderBy(x => x.Name)
					},
					_ => query
				};
			}
		}
	}
}
