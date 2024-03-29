﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRandomizer.Models
{
	public class RollScope
	{
		public long Id { get; set; }
		public Guid RollScopeRefId { get; set; } = Guid.NewGuid();
		public DateTime CreationDate { get; set; } = DateTime.UtcNow;
		public long? GroupId { get; set; }
		public Group? Group { get; set; }
		public long? SchoolClassId { get; set; }
		public SchoolClass? SchoolClass { get; set; }
		public List<Roll> Rolls { get; set; } = new List<Roll>();
	}
}
