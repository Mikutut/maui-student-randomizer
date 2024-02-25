using Microsoft.EntityFrameworkCore;
using StudentRandomizer.Enums.SchoolClasses;
using StudentRandomizer.Enums.Sorting;
using StudentRandomizer.Models;
using StudentRandomizer.Services.Common;
using StudentRandomizer.Services.Groups;
using StudentRandomizer.Services.Groups.Inputs;
using StudentRandomizer.Services.SchoolClasses.Inputs;
using StudentRandomizer.Services.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRandomizer.Services.SchoolClasses
{
	public class GroupDataService : IGroupDataService
	{
		private readonly IRepository<Group> _groupRepository;
		private readonly IStudentDataService _studentDataService;

		public GroupDataService(IRepository<Group> groupRepository,
								IStudentDataService studentDataService)
		{
			_groupRepository = groupRepository;
			_studentDataService = studentDataService;
		}

		public void AddStudent(Guid groupRefId, Guid studentRefId)
		{
			var student = _studentDataService.Get(studentRefId);

			_studentDataService.AddStudentToGroup(student, groupRefId);
		}

		public Group Create(CreateGroupInput input)
		{
			if (_groupRepository.FirstOrDefault(x => x.Name.Equals(input.Name)) != null)
			{
				throw new ApplicationException($"Group with name: '{input.Name}' already exists.");
			}

			var group = new Group()
			{
				Name = input.Name,
				RollScope = new RollScope()
			};

			group = _groupRepository.Insert(group);
			_groupRepository.SaveChanges();

			return group;
		}

		public void Delete(Guid groupRefId)
		{
			var group = GetBaseGroupsQuery()
				.FirstOrDefault(x => x.GroupRefId.Equals(groupRefId));

			if(group == null)
			{
				throw new ApplicationException($"Group with RefId: '{groupRefId}' could not be found.");
			}

			_groupRepository.Delete(group);
			_groupRepository.SaveChanges();
		}

		public Group Get(Guid groupRefId)
		{
			var group = GetBaseGroupsQuery()
				.FirstOrDefault(x => x.GroupRefId.Equals(groupRefId));

			if(group == null)
			{
				throw new ApplicationException($"Group with RefId: '{groupRefId}' could not be found.");
			}

			return group;
		}

		public ICollection<Group> GetAll(GetAllGroupsInput input)
		{
			var groupsQuery = GetBaseGroupsQuery();
			FilterAndSortGroups(groupsQuery, input);

			var groups = groupsQuery.ToList();
			return groups;
		}

		public void RemoveStudent(Guid groupRefId, Guid studentRefId)
		{
			var student = _studentDataService.Get(studentRefId);

			_studentDataService.RemoveStudentFromGroup(student, groupRefId);
		}

		public Group Update(UpdateGroupInput input)
		{
			var group = GetBaseGroupsQuery()
				.FirstOrDefault(x => x.GroupRefId.Equals(input.GroupRefId));

			if(group == null)
			{
				throw new ApplicationException($"Group with RefId: '{input.GroupRefId}' could not be found.");
			}

			if (_groupRepository.FirstOrDefault(x => x.Name.Equals(input.Name)) != null)
			{
				throw new ApplicationException($"Group with name: '{input.Name}' already exists.");
			}

			group.Name = input.Name;
			group = _groupRepository.Update(group);
			_groupRepository.SaveChanges();

			return group;
		}

		private IQueryable<Group> GetBaseGroupsQuery()
		{
			return _groupRepository.GetAll()
				.Include(x => x.Students)
				.ThenInclude(y => y.Student)
				.Include(x => x.RollScope)
				.ThenInclude(y => y.Rolls);
		}

		private void FilterAndSortGroups(IQueryable<Group> query,
										 GetAllGroupsInput input)
		{
			if(input.Sorting != null)
			{
				query = input.Sorting.SortBy switch
				{
					SortGroupsBy.Name => input.Sorting.Direction switch
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
