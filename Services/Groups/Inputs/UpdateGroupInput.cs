﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRandomizer.Services.Groups.Inputs
{
	public class UpdateGroupInput
	{
		public Guid GroupRefId { get; set; }
		public string Name { get; set; }
	}
}
