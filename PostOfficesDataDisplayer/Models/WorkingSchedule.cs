using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PostOfficesDataDisplayer.Models
{
    /// <summary>
    /// post office's  Working schedule.
    /// </summary>
    public class WorkingSchedule: INotifyPropertyChanged
    {
        /// <summary>
        /// The post office's  working hours.
        /// </summary>
        private string _workingHours;

        /// <summary>
        /// Gets or sets the working hours.
        /// </summary>
        /// <value>The working hours.</value>
        public string WorkingHours
        {
            get => _workingHours;

            set
            {
                _workingHours = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The post office's  working hours extra.
        /// </summary>
        private string _workingHoursExtra;

        /// <summary>
        /// Gets or sets the working hours extra.
        /// </summary>
        /// <value>The working hours extra.</value>
        public string WorkingHoursExtra
        {
            get => _workingHoursExtra;

            set
            {
                _workingHoursExtra = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:PostOfficesDataDisplayer.Models.WorkingSchedule"/> class.
        /// </summary>
        /// <param name="workHours">Work hours.</param>
        /// <param name="extra">Extra.</param>
        public WorkingSchedule(string workHours, string extra)
        {
            WorkingHours = workHours;
            WorkingHoursExtra = extra;
        }

        /// <summary>
        /// Ons the property changed.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        public void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Occurs when property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
