using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRandomizer.Models
{
	public class Student
	{
		public long Id { get; set; }
		public Guid StudentRefId { get; set; } = Guid.NewGuid();
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string FullName
		{
			get
			{
				return FirstName + " " + LastName;
			}
		}
		public SchoolClassEntry? Class { get; set; }
		public List<GroupEntry> Groups { get; set; } = new List<GroupEntry>();
		public DateTime CreationDate { get; set; } = DateTime.UtcNow;
		public DateTime? ModificationDate { get; set; }
	}
}
