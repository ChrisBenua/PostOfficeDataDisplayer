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
    /// <summary>
    /// Standard representation for Post Office
    /// </summary>
    public class PostOffice: INotifyPropertyChanged
    {
        /// <summary>
        /// The properties names.
        /// </summary>
        public static readonly string[] PropertiesNames = new string[] { "RowNum", "FullName", "ShortName", "Contacts.PostalCode",
            "Location.AdmArea", "Location.District", "Contacts.Address", "Contacts.AddressExtraInfo", "Contacts.ChiefPhone",
            "Contacts.DeliveryDepartmentPhone", "Contacts.TelegraphPhone", "Schedule.WorkingHours", "Schedule.WorkingHoursExtra",
            "ClassOPS", "TypeOPS", "MMR", "CloseFlag", "CloseExtraInfo", "UNOM", "Location.Coords.XCoordStr", "Location.Coords.YCoordStr", "GlobalID"
        };

        /// <summary>
        /// The column headers.
        /// </summary>
        public static readonly string[] ColumnHeaders = new string[]
        {
            "ROWNUM", "FullName", "ShortName", "PostalCode", "AdmArea",
            "District", "Address", "AddressExtraInfo", "ChiefPhone", "DeliveryDepartmentPhone",
            "TelegraphPhone", "WorkingHours", "WorkingHoursExtraInfo", "ClassOPS",
            "TypeOPS", "MMP", "CloseFlag", "CloseExtraInfo", "UNOM", "X_WGS84", "Y_WGS84", "GLOBALID"
        };

        /// <summary>
        /// The post office's location.
        /// </summary>
        private Location _location;

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>The location.</value>
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

        /// <summary>
        /// The row number.
        /// </summary>
        private string _rowNum;

        /// <summary>
        /// Gets or sets the row number.
        /// </summary>
        /// <value>The row number.</value>
        public string RowNum
        {
            get => _rowNum;

            set
            {
                _rowNum = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The post office's  full name.
        /// </summary>
        private string _fullName;

        /// <summary>
        /// Gets or sets the full name.
        /// </summary>
        /// <value>The full name.</value>
        public string FullName
        {
            get => _fullName;

            set
            {
                _fullName = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The post office's short name.
        /// </summary>
        private string _shortName;


        /// <summary>
        /// Gets or sets the short name.
        /// </summary>
        /// <value>The short name.</value>
        public string ShortName
        {
            get => _shortName;

            set
            {
                _shortName = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The post office's contacts.
        /// </summary>
        private OfficeContacts _contacts;

        /// <summary>
        /// Gets or sets the contacts.
        /// </summary>
        /// <value>The contacts.</value>
        public OfficeContacts Contacts
        {
            get => _contacts;

            set
            {
                _contacts = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The post office's  schedule.
        /// </summary>
        private WorkingSchedule _schedule;

        /// <summary>
        /// Gets or sets the schedule.
        /// </summary>
        /// <value>The schedule.</value>
        public WorkingSchedule Schedule
        {
            get => _schedule;

            set
            {
                _schedule = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The post office's  class ops.
        /// </summary>
        private string _classOPS;

        /// <summary>
        /// Gets or sets the class ops.
        /// </summary>
        /// <value>The class ops.</value>
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

        /// <summary>
        /// The post office's  type ops.
        /// </summary>
        private string _typeOPS;

        /// <summary>
        /// Gets or sets the type ops.
        /// </summary>
        /// <value>The type ops.</value>
        public string TypeOPS
        {
            get => _typeOPS;

            set
            {
                _typeOPS = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The post office's mmp.
        /// </summary>
        private string _mmr;

        /// <summary>
        /// Gets or sets the mmp.
        /// </summary>
        /// <value>The mmr.</value>
        public string MMR
        {
            get => _mmr;

            set
            {
                _mmr = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The post office's  close flag.
        /// </summary>
        private string _closeFlag;

        /// <summary>
        /// Gets or sets the close flag.
        /// </summary>
        /// <value>The close flag.</value>
        public string CloseFlag
        {
            get => _closeFlag;

            set
            {
                _closeFlag = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The post office's  close extra info.
        /// </summary>
        private string _closeExtraInfo;

        /// <summary>
        /// Gets or sets the close extra info.
        /// </summary>
        /// <value>The close extra info.</value>
        public string CloseExtraInfo
        {
            get => _closeExtraInfo;

            set
            {
                _closeExtraInfo = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The post office's  unom.
        /// </summary>
        private string _UNOM;


        /// <summary>
        /// Gets or sets the unom.
        /// </summary>
        /// <value>The unom.</value>
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

        /// <summary>
        /// The post office's  global identifier.
        /// </summary>
        private string _globalID;

        /// <summary>
        /// Gets or sets the global identifier.
        /// </summary>
        /// <value>The global identifier.</value>
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
        /// <summary>
        /// Initializes a new instance of the <see cref="T:PostOfficesDataDisplayer.Models.PostOffice"/> class.
        /// </summary>
        /// <param name="rowNum">Row number.</param>
        /// <param name="fullName">Full name.</param>
        /// <param name="shortName">Short name.</param>
        /// <param name="postalCode">Postal code.</param>
        /// <param name="admArea">Adm area.</param>
        /// <param name="district">District.</param>
        /// <param name="address">Address.</param>
        /// <param name="addressExtraInfo">Address extra info.</param>
        /// <param name="chiefPhone">Chief phone.</param>
        /// <param name="deliveryDepPhone">Delivery dep phone.</param>
        /// <param name="telegraphPhone">Telegraph phone.</param>
        /// <param name="workingHours">Working hours.</param>
        /// <param name="workingHoursExtra">Working hours extra.</param>
        /// <param name="classOps">Class ops.</param>
        /// <param name="typeOPS">Type ops.</param>
        /// <param name="mmr">Mmr.</param>
        /// <param name="closeFlag">Close flag.</param>
        /// <param name="closeFlagExtra">Close flag extra.</param>
        /// <param name="unom">Unom.</param>
        /// <param name="xcoord">Xcoord.</param>
        /// <param name="ycoord">Ycoord.</param>
        /// <param name="globalID">Global identifier.</param>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="T:PostOfficesDataDisplayer.Models.PostOffice"/> class.
        /// </summary>
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

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="T:PostOfficesDataDisplayer.Models.PostOffice"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="T:PostOfficesDataDisplayer.Models.PostOffice"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current
        /// <see cref="T:PostOfficesDataDisplayer.Models.PostOffice"/>; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            var other = obj as PostOffice;
            return (other?.GlobalID ?? "-1") == this.GlobalID;
        }

        /// <summary>
        /// Calls when the property changed.
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
