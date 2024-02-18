using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRandomizer.Models
{
	public class SchoolClass
	{
		public long Id { get; set; }
		public Guid SchoolClassRefId { get; set; } = Guid.NewGuid();
		public string Name { get; set; }
		public DateTime CreationDate { get; set; } = DateTime.UtcNow;
		public DateTime? ModificationDate { get; set; }
		public List<SchoolClassEntry> Students { get; set; } = new List<SchoolClassEntry>();
	}
}
