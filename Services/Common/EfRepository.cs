using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using StudentRandomizer.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Gaming.Input.ForceFeedback;

namespace StudentRandomizer.Services.Common
{
	public class EfRepository<T> : IRepository<T>
		where T : class
	{
		private readonly DatabaseContext _context;
		private DbSet<T> Set { get; set; }

		public EfRepository(DatabaseContext context)
		{
			_context = context;
			Set = _context.Set<T>();
		}

		public IQueryable<T> GetAll()
		{
			return Set;
		}

		public T? FirstOrDefault(Func<T, bool> predicate)
		{
			return Set.FirstOrDefault(predicate);
		}

		public T? SingleOrDefault(Func<T, bool> predicate)
		{
			return Set.SingleOrDefault(predicate);
		}

		public T Insert(T inputObject)
		{
			var entry = Set.Add(inputObject);
			return entry.Entity;
		}

		public T Update(T inputObject)
		{
			var entry = Set.Update(inputObject);
			return entry.Entity;
		}

		public void Delete(T inputObject)
		{
			Set.Remove(inputObject);
		}

		public void SaveChanges()
		{
			_context.SaveChanges();
		}
	}
}
