using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRandomizer.Services.ExternalData
{
	public interface IExternalDataService
	{
		string ExportStudents();
		string ExportSchoolClasses();
		string ExportGroups();
		string ExportRolls();
		string ExportLuckyNumbers();
		string ExportAttendance();
		string ExportAll();
		void ImportStudents(string block);
		void ImportSchoolClasses(string block);
		void ImportSchoolClassStudents(string block);
		void ImportGroups(string block);
		void ImportGroupStudents(string block);
		void ImportRolls(string block);
		void ImportLuckyNumbers(string block);
		void ImportAttendance(string block);
		void ImportAll(string fileContents);
	}
}
