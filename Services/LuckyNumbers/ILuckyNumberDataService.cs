using StudentRandomizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRandomizer.Services.LuckyNumbers
{
    public interface ILuckyNumberDataService
    {
        LuckyNumber GetOrCreate(DateTime luckyNumberDate);
        ICollection<LuckyNumber> GetAll();
    }
}
