using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRandomizer.Services.Common
{
	public interface IRepository<T>
		where T: class
	{
		IQueryable<T> GetAll();
		T? FirstOrDefault(Func<T, bool> predicate);
		T? SingleOrDefault(Func<T, bool> predicate);
		T Insert(T inputObject);
		T Update(T inputObject);
		void Delete(T inputObject);
		void SaveChanges();
	}
}
