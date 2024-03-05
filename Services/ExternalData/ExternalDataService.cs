using StudentRandomizer.EntityFrameworkCore;
using StudentRandomizer.Enums.ExternalData;
using StudentRandomizer.Models;
using StudentRandomizer.Services.Common;
using StudentRandomizer.Services.Groups;
using StudentRandomizer.Services.Groups.Inputs;
using StudentRandomizer.Services.LuckyNumbers;
using StudentRandomizer.Services.SchoolClasses;
using StudentRandomizer.Services.SchoolClasses.Inputs;
using StudentRandomizer.Services.Students;
using StudentRandomizer.Services.Students.Inputs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StudentRandomizer.Services.ExternalData
{
	public class ExternalDataService : IExternalDataService
	{
		private readonly IStudentDataService _studentDataService;
		private readonly ISchoolClassDataService _schoolClassDataService;
		private readonly IGroupDataService _groupDataService;
		private readonly ILuckyNumberDataService _luckyNumberDataService;
		private readonly IRollManagementService<SchoolClass> _schoolClassRollManagementService;
		private readonly IRollManagementService<Models.Group> _groupRollManagementService;

		private readonly IRepository<Student> _studentRepository;
		private readonly IRepository<Models.Group> _groupRepository;
		private readonly IRepository<SchoolClass> _schoolClassRepository;
		private readonly IRepository<LuckyNumber> _luckyNumberRepository;
		private readonly IRepository<RollScope> _rollScopeRepository;

		private readonly DatabaseContext _context;

		private const string HEADER_PATTERN = @"^=([^=]+)=$";
		private const string FOOTER_PATTERN = @"^=END=$";

		private readonly GetAllStudentsInput getAllStudentsInput = new GetAllStudentsInput()
		{
			Sorting = new SortStudentsInput()
			{
				SortBy = Enums.Students.SortStudentsBy.LastName,
				Direction = Enums.Sorting.SortingDirection.Ascending
			}
		};

		private readonly GetAllSchoolClassesInput getAllSchoolClassesInput = new GetAllSchoolClassesInput()
		{
			Sorting = new SortSchoolClassesInput()
			{
				SortBy = Enums.SchoolClasses.SortSchoolClassesBy.Name,
				Direction = Enums.Sorting.SortingDirection.Ascending
			}
		};

		private readonly GetAllGroupsInput getAllGroupsInput = new GetAllGroupsInput()
		{
			Sorting = new SortGroupsInput()
			{
				SortBy = Enums.Groups.SortGroupsBy.Name,
				Direction = Enums.Sorting.SortingDirection.Ascending
			}
		};

		public ExternalDataService(IStudentDataService studentDataService,
								   ISchoolClassDataService schoolClassDataService,
								   IGroupDataService groupDataService,
								   ILuckyNumberDataService luckyNumberDataService,
								   IRollManagementService<SchoolClass> schoolClassRollManagementService,
								   IRollManagementService<Models.Group> groupRollManagementService,
								   IRepository<Student> studentRepository,							   
								   IRepository<Models.Group> groupRepository,
								   IRepository<SchoolClass> schoolClassRepository,
								   IRepository<RollScope> rollScopeRepository,
								   IRepository<LuckyNumber> luckyNumberRepository,
								   DatabaseContext context)
		{
			_studentDataService = studentDataService;
			_schoolClassDataService = schoolClassDataService;
			_groupDataService = groupDataService;
			_luckyNumberDataService = luckyNumberDataService;
			_schoolClassRollManagementService = schoolClassRollManagementService;
			_groupRollManagementService = groupRollManagementService;
			_context = context;

			_studentRepository = studentRepository;
			_groupRepository = groupRepository;
			_schoolClassRepository = schoolClassRepository;
			_luckyNumberRepository = luckyNumberRepository;
			_rollScopeRepository = rollScopeRepository;
		}

		public void ImportAttendance(string block)
		{
			var lines = block.Split(Environment.NewLine);

			foreach(var line in lines)
			{
				var values = line.Split(';');
				var student = _studentDataService.Get(Guid.Parse(values[1]));

				student.Attendance.Add(new AttendanceRecord()
				{
					AttendanceRecordRefId = Guid.Parse(values[0]),
					Date = DateTime.Parse(values[2]),
					IsPresent = bool.Parse(values[3])
				});

				_studentRepository.Update(student);
			}

			_studentRepository.SaveChanges();
		}

		public string ExportAttendance()
		{
			StringBuilder sb = new StringBuilder();

			var attendanceRecords = _studentDataService.GetAll(getAllStudentsInput)
				.SelectMany(x => x.Attendance)
				.ToList();

			sb.AppendLine("=Attendance=");
			foreach(AttendanceRecord attendance in attendanceRecords)
			{
				sb.AppendLine($"{attendance.AttendanceRecordRefId};{attendance.Student.StudentRefId};{attendance.Date.ToString("o")};{attendance.IsPresent};");
			}
			sb.AppendLine("=END=");

			return sb.ToString();
		}

		public void ImportGroups(string block)
		{
			var lines = block.Split(Environment.NewLine);

			foreach(var line in lines)
			{
				var values = line.Split(';');

				Models.Group group = new Models.Group()
				{
					GroupRefId = Guid.Parse(values[0]),
					Name = values[1]
				};

				group = _groupRepository.Insert(group);
				_groupRepository.SaveChanges();

				group.RollScope = new RollScope()
				{
					RollScopeRefId = Guid.Parse(values[2]),
					GroupId = group.Id,
					Group = group
				};

				group = _groupRepository.Update(group);
				_groupRepository.SaveChanges();
			}
		}

		public void ImportGroupStudents(string block)
		{
			var lines = block.Split(Environment.NewLine);

			foreach(var line in lines)
			{
				var values = line.Split(';');

				var group = _groupDataService.Get(Guid.Parse(values[0]));
				var student = _studentDataService.Get(Guid.Parse(values[1]));

				group.Students.Add(new GroupEntry()
				{
					Student = student,
					StudentId = student.Id,
					Group = group,
					GroupId = group.Id,
					OrderNumber = uint.Parse(values[2])
				});

				group = _groupRepository.Update(group);
				_groupRepository.SaveChanges();
			}
		}

		public string ExportGroups()
		{
			var sb = new StringBuilder();
			var groups = _groupDataService.GetAll(getAllGroupsInput);

			sb.AppendLine("=Groups=");
			foreach(var group in groups)
			{
				sb.AppendLine($"{group.GroupRefId};{group.Name};{group.RollScope.RollScopeRefId};");
			}
			sb.AppendLine("=END=");

			sb.AppendLine("=GroupStudents=");
			foreach(var group in groups)
			{
				foreach(var student in group.Students)
				{
					sb.AppendLine($"{group.GroupRefId};{student.Student.StudentRefId};{student.OrderNumber};");
				}
			}
			sb.AppendLine("=END=");

			return sb.ToString();
		}

		public void ImportLuckyNumbers(string block)
		{
			var lines = block.Split(Environment.NewLine);

			foreach(var line in lines)
			{
				var values = line.Split(';');

				var luckyNumber = _luckyNumberRepository.Insert(new LuckyNumber()
				{
					LuckyNumberRefId = Guid.Parse(values[0]),
					CreationDate = DateTime.Parse(values[1]),
					Value = uint.Parse(values[2])
				});

				_luckyNumberRepository.SaveChanges();
			}
		}

		public string ExportLuckyNumbers()
		{
			var sb = new StringBuilder();
			var luckyNumbers = _luckyNumberDataService.GetAll();

			sb.AppendLine("=LuckyNumbers=");
			foreach(var luckyNumber in luckyNumbers)
			{
				sb.AppendLine($"{luckyNumber.LuckyNumberRefId};{luckyNumber.CreationDate.ToString("o")};{luckyNumber.Value};");
			}
			sb.AppendLine("=END=");

			return sb.ToString();
		}

		public void ImportRolls(string block)
		{
			var lines = block.Split(Environment.NewLine);

			foreach(var line in lines)
			{
				var values = line.Split(';');
				var rollScope = _rollScopeRepository
					.FirstOrDefault(x => x.RollScopeRefId.Equals(Guid.Parse(values[1])));

				rollScope?.Rolls.Add(new Roll()
				{
					RollRefId = Guid.Parse(values[0]),
					CreationDate = DateTime.Parse(values[2]),
					Value = long.Parse(values[3])
				});

				rollScope = _rollScopeRepository.Update(rollScope);
				_rollScopeRepository.SaveChanges();
			}
		}

		public string ExportRolls()
		{
			var sb = new StringBuilder();

			var groups = _groupDataService.GetAll(getAllGroupsInput);
			var schoolClasses = _schoolClassDataService.GetAll(getAllSchoolClassesInput);

			sb.AppendLine("=Rolls=");
			foreach(var group in groups)
			{
				var rollScope = group.RollScope;
				foreach(var roll in rollScope.Rolls)
				{
					sb.AppendLine($"{roll.RollRefId};{rollScope.RollScopeRefId};{roll.CreationDate.ToString("o")};{roll.Value};");
				}
			}
			foreach(var schoolClass in schoolClasses)
			{
				var rollScope = schoolClass.RollScope;
				foreach(var roll in rollScope.Rolls)
				{
					sb.AppendLine($"{roll.RollRefId};{rollScope.RollScopeRefId};{roll.CreationDate.ToString("o")};{roll.Value};");
				}
			}
			sb.AppendLine("=END=");

			return sb.ToString();
		}

		public void ImportSchoolClasses(string block)
		{
			var lines = block.Split(Environment.NewLine);

			foreach(var line in lines)
			{
				var values = line.Split(';');

				Models.SchoolClass schoolClass = new Models.SchoolClass()
				{
					SchoolClassRefId = Guid.Parse(values[0]),
					Name = values[1]
				};

				schoolClass = _schoolClassRepository.Insert(schoolClass);
				_schoolClassRepository.SaveChanges();

				schoolClass.RollScope = new RollScope()
				{
					RollScopeRefId = Guid.Parse(values[2]),
					SchoolClassId = schoolClass.Id,
					SchoolClass = schoolClass
				};

				schoolClass = _schoolClassRepository.Update(schoolClass);
				_schoolClassRepository.SaveChanges();
			}
		}

		public void ImportSchoolClassStudents(string block)
		{
			var lines = block.Split(Environment.NewLine);

			foreach(var line in lines)
			{
				var values = line.Split(';');

				var schoolClass = _schoolClassDataService.Get(Guid.Parse(values[0]));
				var student = _studentDataService.Get(Guid.Parse(values[1]));

				schoolClass.Students.Add(new SchoolClassEntry()
				{
					Student = student,
					StudentId = student.Id,
					SchoolClass = schoolClass,
					SchoolClassId = schoolClass.Id,
					OrderNumber = uint.Parse(values[2])
				});

				schoolClass = _schoolClassRepository.Update(schoolClass);
				_schoolClassRepository.SaveChanges();
			}
		}

		public string ExportSchoolClasses()
		{
			var sb = new StringBuilder();
			var schoolClasses = _schoolClassDataService.GetAll(getAllSchoolClassesInput);

			sb.AppendLine("=SchoolClasses=");
			foreach(var schoolClass in schoolClasses)
			{
				sb.AppendLine($"{schoolClass.SchoolClassRefId};{schoolClass.Name};{schoolClass.RollScope.RollScopeRefId};");
			}
			sb.AppendLine("=END=");

			sb.AppendLine("=SchoolClassStudents=");
			foreach(var schoolClass in schoolClasses)
			{
				foreach(var student in schoolClass.Students)
				{
					sb.AppendLine($"{schoolClass.SchoolClassRefId};{student.Student.StudentRefId};{student.OrderNumber};");
				}
			}
			sb.AppendLine("=END=");

			return sb.ToString();
		}

		public void ImportStudents(string block)
		{
			var lines = block.Split(Environment.NewLine);

			foreach(var line in lines)
			{
				var values = line.Split(';');

				var student = _studentRepository.Insert(new Student()
				{
					StudentRefId = Guid.Parse(values[0]),
					FirstName = values[1],
					LastName = values[2]
				});

				student = _studentRepository.Insert(student);
				_studentRepository.SaveChanges();
			}
		}

		public string ExportStudents()
		{
			var sb = new StringBuilder();

			var students = _studentDataService.GetAll(getAllStudentsInput);

			sb.AppendLine("=Students=");
			foreach(var student in students)
			{
				sb.AppendLine($"{student.StudentRefId};{student.FirstName};{student.LastName};");
			}
			sb.AppendLine("=END=");

			return sb.ToString();
		}

		public void ImportAll(string fileContents)
		{
			string[] lines = fileContents.Split(Environment.NewLine);

			Enums.ExternalData.ImportScope? currentImportScope = null;
			StringBuilder buffer = new StringBuilder();

			var transaction = _context.Database.BeginTransaction();
			try
			{
				foreach(string line in lines)
				{
					var headerMatch = Regex.Match(line, HEADER_PATTERN);
					var footerMatch = Regex.Match(line, FOOTER_PATTERN);

					if(footerMatch.Success)
					{
						switch(currentImportScope)
						{
							case Enums.ExternalData.ImportScope.Attendance:
								ImportAttendance(buffer.ToString().TrimEnd());
								break;
							case Enums.ExternalData.ImportScope.Groups:
								ImportGroups(buffer.ToString().TrimEnd());
								break;
							case Enums.ExternalData.ImportScope.GroupStudents:
								ImportGroupStudents(buffer.ToString().TrimEnd());
								break;
							case Enums.ExternalData.ImportScope.LuckyNumbers:
								ImportLuckyNumbers(buffer.ToString().TrimEnd());
								break;
							case Enums.ExternalData.ImportScope.SchoolClasses:
								ImportSchoolClasses(buffer.ToString().TrimEnd());
								break;
							case Enums.ExternalData.ImportScope.SchoolClassStudents:
								ImportSchoolClassStudents(buffer.ToString().TrimEnd());
								break;
							case Enums.ExternalData.ImportScope.Students:
								ImportStudents(buffer.ToString().TrimEnd());
								break;
							case Enums.ExternalData.ImportScope.Rolls:
								ImportRolls(buffer.ToString().TrimEnd());
								break;
							default:
								break;
						}

						buffer.Clear();
						currentImportScope = null;
					}
					else if(headerMatch.Success)
					{
						currentImportScope = headerMatch.Groups[1].Value switch
						{
							"Attendance" => Enums.ExternalData.ImportScope.Attendance,
							"Groups" => Enums.ExternalData.ImportScope.Groups,
							"GroupStudents" => Enums.ExternalData.ImportScope.GroupStudents,
							"LuckyNumbers" => Enums.ExternalData.ImportScope.LuckyNumbers,
							"SchoolClasses" => Enums.ExternalData.ImportScope.SchoolClasses,
							"SchoolClassStudents" => Enums.ExternalData.ImportScope.SchoolClassStudents,
							"Students" => Enums.ExternalData.ImportScope.Students,
							"Rolls" => Enums.ExternalData.ImportScope.Rolls,
							_ => null
						};
					} 
					else
					{
						buffer.AppendLine(line);
					}
				}

				transaction.Commit();
			}
			catch(Exception)
			{
				transaction.Rollback();
				throw;
			}
		}

		public string ExportAll()
		{
			var sb = new StringBuilder();

			sb.Append(ExportStudents());
			sb.Append(ExportAttendance());
			sb.Append(ExportSchoolClasses());
			sb.Append(ExportGroups());
			sb.Append(ExportRolls());
			sb.Append(ExportLuckyNumbers());

			return sb.ToString();
		}
	}
}
