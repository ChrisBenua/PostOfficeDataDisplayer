using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PostOfficesDataDisplayer.Models
{
    public class WorkingSchedule: INotifyPropertyChanged
    {
        private string _workingHours;

        public string WorkingHours
        {
            get => _workingHours;

            set
            {
                _workingHours = value;
                OnPropertyChanged();
            }
        }

        private string _workingHoursExtra;

        public string WorkingHoursExtra
        {
            get => _workingHours;

            set
            {
                _workingHours = value;
                OnPropertyChanged();
            }
        }

        public WorkingSchedule(string workHours, string extra)
        {
            WorkingHours = workHours;
            WorkingHoursExtra = extra;
        }

        public void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
