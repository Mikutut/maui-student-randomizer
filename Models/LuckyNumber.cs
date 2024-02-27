using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRandomizer.Models
{
    public class LuckyNumber : INotifyPropertyChanged
    {
        private long id;
        private Guid luckyNumberRefId = Guid.NewGuid();
        private uint value;
        private DateTime creationDate = DateTime.UtcNow;

        public long Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        public Guid LuckyNumberRefId
        {
            get => luckyNumberRefId;
            set
            {
                luckyNumberRefId = value;
                OnPropertyChanged("LuckyNumberRefId");
            }
        }

        public uint Value
        {
            get => value;
            set
            {
                this.value = value;
                OnPropertyChanged("Value");
            }
        }

        public DateTime CreationDate
        {
            get => creationDate;
            set
            {
                creationDate = value;
                OnPropertyChanged("CreationDate");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        
        private void OnPropertyChanged(string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
