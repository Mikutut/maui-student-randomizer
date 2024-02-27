using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRandomizer.Models
{
    public class AttendanceRecord : INotifyPropertyChanged
    {
        private long id;
        private Guid attendanceRecordRefId = Guid.NewGuid();
        private long studentId;
        private Student? student;
        private DateTime date = DateTime.UtcNow;
        private bool isPresent;

        public long Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        public Guid AttendanceRecordRefId
        {
            get => attendanceRecordRefId;
            set
            {
                attendanceRecordRefId = value;
                OnPropertyChanged("AttendanceRecordRefId");
            }
        }

        public long StudentId
        {
            get => studentId;
            set
            {
                studentId = value;
                OnPropertyChanged("StudentId");
            }
        }

        public Student Student
        {
            get => student;
            set
            {
                student = value;
                OnPropertyChanged("Student");
            }
        }

        public DateTime Date
        {
            get => date;
            set
            {
                date = value;
                OnPropertyChanged("Date");
            }
        }

        public bool IsPresent
        {
            get => isPresent;
            set
            {
                isPresent = value;
                OnPropertyChanged("IsPresent");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
