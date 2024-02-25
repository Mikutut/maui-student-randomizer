using StudentRandomizer.Models;
using StudentRandomizer.Services.Students.Inputs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRandomizer.Services.Students
{
    public interface IStudentDataService
    {
        ICollection<Student> GetAll(GetAllStudentsInput input);
        Student Get(Guid studentRefId);
        Student Create(CreateStudentInput input);
        Student Update(UpdateStudentInput input);
        void AddStudentToClass(Student student, Guid schoolClassRefId);
        void AddStudentToGroup(Student student, Guid groupRefId);
        void RemoveStudentFromClass(Student student, Guid schoolClassRefId);
        void RemoveStudentFromGroup(Student student, Guid groupRefId);
        void Delete(Guid studentRefId);
    }
}
