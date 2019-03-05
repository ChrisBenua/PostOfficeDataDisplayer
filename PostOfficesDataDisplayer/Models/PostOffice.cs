using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PostOfficesDataDisplayer.Utils;

namespace PostOfficesDataDisplayer.Models
{
    public class PostOffice: INotifyPropertyChanged
    {

        public static readonly string[] PropertiesNames = new string[] { "RowNum", "FullName", "ShortName", "Contacts.PostalCode",
            "Location.AdmArea", "Location.District", "Contacts.Address", "Contacts.AddressExtraInfo", "Contacts.ChiefPhone",
            "Contacts.DeliveryDepartmentPhone", "Contacts.TelegraphPhone", "Schedule.WorkingHours", "Schedule.WorkingHoursExtra",
            "ClassOPS", "TypeOPS", "MMR", "CloseFlag", "CloseExtraInfo", "UNOM", "Location.Coords.XCoordStr", "Location.Coords.YCoordStr", "GlobalID"
        };

        public static readonly string[] ColumnHeaders = new string[]
        {
            "ROWNUM", "FullName", "ShortName", "PostalCode", "AdmArea",
            "District", "Address", "AddressExtraInfo", "ChiefPhone", "DeliveryDepartmentPhone",
            "TelegraphPhone", "WorkingHours", "WorkingHoursExtraInfo", "ClassOPS",
            "TypeOPS", "MMP", "CloseFlag", "CloseExtraInfo", "UNOM", "X_WGS84", "Y_WGS84", "GLOBALID"
        };

        private Location _location;
        
        public Location Location
        {
            get
            {
                return _location;
            }

            set
            {
                _location = value;
                OnPropertyChanged();
            }
        }

        private string _rowNum;

        public string RowNum
        {
            get => _rowNum;

            set
            {
                _rowNum = value;
                OnPropertyChanged();
            }
        }

        private string _fullName;

        public string FullName
        {
            get => _fullName;

            set
            {
                _fullName = value;
                OnPropertyChanged();
            }
        }

        private string _shortName;

        public string ShortName
        {
            get => _shortName;

            set
            {
                _shortName = value;
                OnPropertyChanged();
            }
        }

        private OfficeContacts _contacts;

        public OfficeContacts Contacts
        {
            get => _contacts;

            set
            {
                _contacts = value;
                OnPropertyChanged();
            }
        }

        private WorkingSchedule _schedule;

        public WorkingSchedule Schedule
        {
            get => _schedule;

            set
            {
                _schedule = value;
                OnPropertyChanged();
            }
        }

        private string _classOPS;

        public string ClassOPS
        {
            get => _classOPS;

            set
            {
                var res = Validator.ValidateInt(value, a => a > 0 && a <= 1e9);
                if (res.Item1)
                {
                    _classOPS = value;
                }
                else
                {
                    MessageBox.Show("Invalid ClassOPS value, expected integer", "Wrong format");
                }
                OnPropertyChanged();

            }
        }

        private string _typeOPS;

        public string TypeOPS
        {
            get => _typeOPS;

            set
            {
                _typeOPS = value;
                OnPropertyChanged();
            }
        }

        private string _mmr;

        public string MMR
        {
            get => _mmr;

            set
            {
                _mmr = value;
                OnPropertyChanged();
            }
        }

        private string _closeFlag;

        public string CloseFlag
        {
            get => _closeFlag;

            set
            {
                _closeFlag = value;
                OnPropertyChanged();
            }
        }

        private string _closeExtraInfo;

        public string CloseExtraInfo
        {
            get => _closeExtraInfo;

            set
            {
                _closeExtraInfo = value;
                OnPropertyChanged();
            }
        }

        private string _UNOM;

        public string UNOM
        {
            get => _UNOM;

            set
            {
                var res = Validator.ValidateInt(value, arg => arg >= 0 && arg <= 1e9);
                if (res.Item1)
                {
                    _UNOM = value;
                }

                OnPropertyChanged();
            }
        }

        private string _globalID;

        public string GlobalID
        {
            get => _globalID;

            set
            {
                var res = Validator.ValidateInt(value, arg => arg >= 0 && arg <= 1e9);
                if (res.Item1)
                {
                    _globalID = value;
                }
                OnPropertyChanged();
            }
        }

        public PostOffice(string rowNum, string fullName, string shortName, string postalCode, string admArea,
                          string district, string address, string addressExtraInfo, string chiefPhone, 
                          string deliveryDepPhone, string telegraphPhone, string workingHours, string workingHoursExtra,
                          string classOps, string typeOPS, string mmr, string closeFlag, string closeFlagExtra, string unom,
                          string xcoord, string ycoord, string globalID)
        {
            this.Contacts = new OfficeContacts(postalCode, address, addressExtraInfo, chiefPhone, deliveryDepPhone, telegraphPhone);
            this.Location = new Location(xcoord.Replace(",", "."), ycoord.Replace(",", "."), district, admArea);
            this.Schedule = new WorkingSchedule(workingHours, workingHoursExtra);
            this.ClassOPS = classOps;
            this.TypeOPS = typeOPS;
            this.MMR = mmr;
            this.CloseFlag = closeFlag;
            this.CloseExtraInfo = closeFlagExtra;
            this.UNOM = unom;
            this.GlobalID = globalID;
            this.RowNum = rowNum;
            this.FullName = fullName;
            this.ShortName = shortName;
        }

        public PostOffice()
        {
            this.Contacts = new OfficeContacts();
            this.Location = new Location();
            this.Schedule = new WorkingSchedule("", "");
            this.ClassOPS = "";
            this.TypeOPS = "";
            this.MMR = MMR;
            this.CloseFlag = "";
            this.CloseExtraInfo = "";
            this.UNOM = "";
            this.GlobalID = "";
        }

        public override bool Equals(object obj)
        {
            var other = obj as PostOffice;
            return (other?.GlobalID ?? "-1") == this.GlobalID;
        }

        public void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
