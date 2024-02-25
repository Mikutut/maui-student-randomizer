using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRandomizer.Models
{
	public class Group
	{
		public long Id { get; set; }
		public Guid GroupRefId { get; set; }
		public string Name { get; set; }
		public List<GroupEntry> Students { get; set; } = new List<GroupEntry>();
		public RollScope RollScope { get; set; }
		public DateTime CreationDate { get; set; } = DateTime.UtcNow;
		public DateTime? ModificationDate { get; set; }
	}
}
