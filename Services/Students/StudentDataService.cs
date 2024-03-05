using Microsoft.EntityFrameworkCore;
using StudentRandomizer.EntityFrameworkCore;
using StudentRandomizer.Enums.Sorting;
using StudentRandomizer.Enums.Students;
using StudentRandomizer.Models;
using StudentRandomizer.Services.Common;
using StudentRandomizer.Services.Students.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRandomizer.Services.Students
{
	public class StudentDataService : IStudentDataService
	{
		private readonly IRepository<Student> _studentRepository;
		private readonly IRepository<SchoolClassEntry> _schoolClassEntryRepository;
		private readonly IRepository<SchoolClass> _schoolClassRepository;
		private readonly IRepository<GroupEntry> _groupEntryRepository;
		private readonly IRepository<Group> _groupRepository;

		public StudentDataService(IRepository<Student> studentRepository,
								  IRepository<SchoolClassEntry> schoolClassEntryRepository,
								  IRepository<SchoolClass> schoolClassRepository,
								  IRepository<GroupEntry> groupEntryRepository,
								  IRepository<Group> groupRepository)
		{
			_studentRepository = studentRepository;
			_schoolClassEntryRepository = schoolClassEntryRepository;
			_schoolClassRepository = schoolClassRepository;
			_groupEntryRepository = groupEntryRepository;
			_groupRepository = groupRepository;
		}

		public Student Create(CreateStudentInput input)
		{
			var student = new Student()
			{
				FirstName = input.FirstName,
				LastName = input.LastName
			};

			student = _studentRepository.Insert(student);
			_studentRepository.SaveChanges();

			if(input.SchoolClassRefId.HasValue)
			{
				AddStudentToClass(student, input.SchoolClassRefId.Value);
			}

			return student;
		}

		public void Delete(Guid studentRefId)
		{
			var student = GetBaseStudentsQuery()
				.FirstOrDefault(x => x.StudentRefId.Equals(studentRefId));

			if(student == null)
			{
				throw new ApplicationException($"Student with RefId: '{studentRefId}' could not be found.");
			}

			_studentRepository.Delete(student);
			_studentRepository.SaveChanges();
		}

		public ICollection<Student> GetAll(GetAllStudentsInput input)
		{
			var studentsQuery = GetBaseStudentsQuery();

			studentsQuery = FilterAndSortStudents(studentsQuery, input);

			return studentsQuery.ToList();
		}

		public Student Get(Guid studentRefId)
		{
			var student = GetBaseStudentsQuery()
				.FirstOrDefault(x => x.StudentRefId.Equals(studentRefId));

			if(student == null)
			{
				throw new ApplicationException($"Student with RefId: '{studentRefId}' could not be found.");
			}

			return student;
		}

		public Student Update(UpdateStudentInput input)
		{
			var student = GetBaseStudentsQuery()
				.FirstOrDefault(x => x.StudentRefId.Equals(input.StudentRefId));

			if(student == null)
			{
				throw new ApplicationException($"Student with RefId: '{input.StudentRefId}' could not be found.");
			}

			student.FirstName = input.FirstName;
			student.LastName = input.LastName;
			student.ModificationDate = DateTime.UtcNow;
			_studentRepository.Update(student);
			_studentRepository.SaveChanges();

			var schoolClass = student.Class?.SchoolClass;

			if(input.SchoolClassRefId.HasValue)
			{
				if(schoolClass != null)
				{
					if(schoolClass.SchoolClassRefId.Equals(input.SchoolClassRefId.Value))
					{
						throw new ApplicationException($"Student with RefId: '{input.StudentRefId}' is already a part of class with RefId: '{schoolClass.SchoolClassRefId}'.");
					}
					else
					{
						RemoveStudentFromClass(student, schoolClass.SchoolClassRefId);
					}
				}

				AddStudentToClass(student, input.SchoolClassRefId.Value);
			}
			else
			{
				if(schoolClass != null)
				{
					RemoveStudentFromClass(student, schoolClass.SchoolClassRefId);
				}
			}

			_studentRepository.SaveChanges();
			return student;
		}

		private IQueryable<Student> GetBaseStudentsQuery()
		{
			return _studentRepository.GetAll()
				.Include(x => x.Class)
				.ThenInclude(y => y.SchoolClass)
				.Include(x => x.Groups)
				.ThenInclude(y => y.Group)
				.Include(x => x.Attendance);
		}

		private IQueryable<Student> FilterAndSortStudents(IQueryable<Student> query,
														  GetAllStudentsInput input)
		{
			if(input.Sorting != null)
			{
				query = input.Sorting.SortBy switch
				{
					SortStudentsBy.LastName => input.Sorting.Direction switch
					{
						SortingDirection.Descending => query.OrderByDescending(x => x.LastName),
						_ => query.OrderBy(x => x.LastName)
					},
					SortStudentsBy.ClassName => input.Sorting.Direction switch
					{
						SortingDirection.Descending => query.OrderByDescending(x => x.Class.SchoolClass.Name),
						_ => query.OrderBy(x => x.Class.SchoolClass.Name)
					},
					_ => query
				};
			}

			return query;
		}

		public void AddStudentToClass(Student student, Guid schoolClassRefId)
		{
			var schoolClass = _schoolClassRepository.GetAll()
				.Include(x => x.Students)
				.ThenInclude(y => y.Student)
				.Where(x => x.SchoolClassRefId.Equals(schoolClassRefId))
				.FirstOrDefault();

			if(schoolClass == null)
			{
				throw new ApplicationException($"School class with RefId: '{schoolClassRefId}' could not be found.");
			}

			if(schoolClass.Students
				.Where(x => x.StudentId == student.Id)
				.FirstOrDefault() != null)
			{
				throw new ApplicationException($"School class with RefId: '{schoolClassRefId}' already contains student with RefId: '{student.StudentRefId}'.");
			}

			uint lastStudentOrderNumber = schoolClass.Students
				.OrderByDescending(x => x.OrderNumber)
				.Select(x => x.OrderNumber)
				.FirstOrDefault();

			if(lastStudentOrderNumber == 0)
			{
				lastStudentOrderNumber = 1;
			}

			var newSchoolClassEntry = new SchoolClassEntry()
			{
				Student = student,
				SchoolClass = schoolClass,
				OrderNumber = lastStudentOrderNumber
			};

			schoolClass.Students.Add(newSchoolClassEntry);
			_schoolClassRepository.SaveChanges();

			ReorderStudentsInClass(schoolClass);
		}

		public void AddStudentToGroup(Student student, Guid groupRefId)
		{
			var group = _groupRepository.GetAll()
				.Include(x => x.Students)
				.ThenInclude(y => y.Student)
				.Where(x => x.GroupRefId.Equals(groupRefId))
				.FirstOrDefault();

			if(group == null)
			{
				throw new ApplicationException($"Group with RefId: '{groupRefId}' could not be found.");
			}

			if(group.Students
				.Where(x => x.StudentId == student.Id)
				.FirstOrDefault() != null)
			{
				throw new ApplicationException($"Group with RefId: '{groupRefId}' already contains student with RefId: '{student.StudentRefId}'.");
			}

			uint lastStudentOrderNumber = group.Students
				.OrderByDescending(x => x.OrderNumber)
				.Select(x => x.OrderNumber)
				.FirstOrDefault();

			if(lastStudentOrderNumber == 0)
			{
				lastStudentOrderNumber = 1;
			}

			var newGroupEntry = new GroupEntry()
			{
				Student = student,
				Group = group,
				OrderNumber = lastStudentOrderNumber
			};

			group.Students.Add(newGroupEntry);
			_groupRepository.SaveChanges();

			ReorderStudentsInGroup(group);
		}

		public void RemoveStudentFromClass(Student student, Guid schoolClassRefId)
		{
			var schoolClass = _schoolClassRepository.GetAll()
				.Include(x => x.Students)
				.ThenInclude(y => y.Student)
				.Where(x => x.SchoolClassRefId.Equals(schoolClassRefId))
				.FirstOrDefault();

			if(schoolClass == null)
			{
				throw new ApplicationException($"School class with RefId: '{schoolClassRefId}' could not be found.");
			}

			var schoolClassEntry = schoolClass.Students
				.FirstOrDefault(x => x.StudentId == student.Id);

			if(schoolClassEntry == null)
			{
				throw new ApplicationException($"School class with RefId: '{schoolClassRefId}' does not contain student with RefId: '{student.StudentRefId}'.");
			}


			schoolClass.Students.Remove(schoolClassEntry);
			_schoolClassRepository.SaveChanges();

			ReorderStudentsInClass(schoolClass);
		}

		public void RemoveStudentFromGroup(Student student, Guid groupRefId)
		{
			var group = _groupRepository.GetAll()
				.Include(x => x.Students)
				.ThenInclude(y => y.Student)
				.Where(x => x.GroupRefId.Equals(groupRefId))
				.FirstOrDefault();

			if(group == null)
			{
				throw new ApplicationException($"Group with RefId: '{groupRefId}' could not be found.");
			}

			var groupEntry = group.Students
				.FirstOrDefault(x => x.StudentId == student.Id);

			if(groupEntry == null)
			{
				throw new ApplicationException($"Group with RefId: '{groupRefId}' does not contain student with RefId: '{student.StudentRefId}'.");
			}


			group.Students.Remove(groupEntry);
			_groupRepository.SaveChanges();

			ReorderStudentsInGroup(group);
		}

		private void ReorderStudentsInClass(SchoolClass schoolClass)
		{
			var students = schoolClass.Students
				.OrderBy(x => x.Student.LastName)
				.ToList();

			var studentsCount = students.Count;

			uint index = 1;
			foreach(var student in students)
			{
				student.OrderNumber = index;
				_schoolClassEntryRepository.Update(student);
				index++;
			}
			_schoolClassEntryRepository.SaveChanges();
		}

		private void ReorderStudentsInGroup(Group group)
		{
			var students = group.Students
				.OrderBy(x => x.Student.LastName)
				.ToList();

			var studentsCount = students.Count;

			uint index = 1;
			foreach(var student in students)
			{
				student.OrderNumber = index;
				_groupEntryRepository.Update(student);
				index++;
			}

			_groupEntryRepository.SaveChanges();
		}
	}
}
