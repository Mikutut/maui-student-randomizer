using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRandomizer.Models
{
	public class Roll
	{
		public long Id { get; set; }
		public Guid RollRefId { get; set; }
		public long RollScopeId { get; set; }
		public RollScope Scope { get; set; }
		public long IndexNumber { get; set; }
		public long Value { get; set; }
		public DateTime CreationDate { get; set; } = DateTime.UtcNow;
	}
}
