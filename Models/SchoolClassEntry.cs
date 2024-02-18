using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRandomizer.Models
{
	public class SchoolClassEntry
	{
		public long Id { get; set; }
		public long SchoolClassId { get; set; }
		public SchoolClass? SchoolClass { get; set; } = null!;
		public long StudentId { get; set; }
		public Student? Student { get; set; } = null!;
		public uint OrderNumber { get; set; }
		public DateTime CreationDate { get; set; } = DateTime.UtcNow;
	}
}
