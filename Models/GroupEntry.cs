using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRandomizer.Models
{
	public class GroupEntry
	{
		public long Id { get; set; }
		public long GroupId { get; set; }
		public Group? Group { get; set; } = null!;
		public long StudentId { get; set; }
		public Student? Student { get; set; } = null!;
		public uint OrderNumber { get; set; }
		public DateTime CreationDate { get; set; } = DateTime.UtcNow;
	}
}
