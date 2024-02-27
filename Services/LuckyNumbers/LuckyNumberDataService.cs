using StudentRandomizer.Models;
using StudentRandomizer.Services.Common;
using StudentRandomizer.Services.Groups;
using StudentRandomizer.Services.SchoolClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRandomizer.Services.LuckyNumbers
{
    public class LuckyNumberDataService : ILuckyNumberDataService
    {
        private readonly IRepository<LuckyNumber> _luckyNumberRepository;
		private readonly IGroupDataService _groupDataService;
		private readonly ISchoolClassDataService _schoolClassDataService;

		public LuckyNumberDataService(IRepository<LuckyNumber> luckyNumberRepository,
									  IGroupDataService groupDataService,
									  ISchoolClassDataService schoolClassDataService)
		{
			_luckyNumberRepository = luckyNumberRepository;
			_groupDataService = groupDataService;
			_schoolClassDataService = schoolClassDataService;
		}

		public LuckyNumber GetOrCreate(DateTime luckyNumberDate)
		{
			var luckyNumber = _luckyNumberRepository.FirstOrDefault(x => x.CreationDate.Date.Equals(luckyNumberDate.Date));

			if(luckyNumber == null)
			{
				var highestSchoolClassOrderNumber = _schoolClassDataService
					.GetAll(new SchoolClasses.Inputs.GetAllSchoolClassesInput())
					.SelectMany(x => x.Students)
					.OrderByDescending(x => x.OrderNumber)
					.Select(x => x.OrderNumber)
					.FirstOrDefault();

				var highestGroupOrderNumber = _groupDataService
					.GetAll(new Groups.Inputs.GetAllGroupsInput())
					.SelectMany(x => x.Students)
					.OrderByDescending(x => x.OrderNumber)
					.Select(x => x.OrderNumber)
					.FirstOrDefault();

				var highestOrderNumber = (highestGroupOrderNumber > highestSchoolClassOrderNumber)
						? highestGroupOrderNumber
						: highestSchoolClassOrderNumber;

				if(highestOrderNumber == 0)
				{
					highestOrderNumber = 1;
				}

				Random random = new Random();

				luckyNumber = new LuckyNumber()
				{
					Value = (uint)random.Next(1, (int)highestOrderNumber + 1)
				};

				luckyNumber = _luckyNumberRepository.Insert(luckyNumber);
				_luckyNumberRepository.SaveChanges();
			}

			return luckyNumber;
		}
	}
}
