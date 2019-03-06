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
    /// post office's  Office contacts.
    /// </summary>
    public class OfficeContacts: INotifyPropertyChanged
    {
        /// <summary>
        /// The post office's  postal code.
        /// </summary>
        private string _postalCode;

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        /// <value>The postal code.</value>
        public string PostalCode
        {
            get => _postalCode;

            set
            {
                _postalCode = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The post office's  address.
        /// </summary>
        private string _address;

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>The address.</value>
        public string Address
        {
            get => _address;

            set
            {
                _address = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The post office's  address extra info.
        /// </summary>
        private string _addressExtraInfo;

        /// <summary>
        /// Gets or sets the address extra info.
        /// </summary>
        /// <value>The address extra info.</value>
        public string AddressExtraInfo
        {
            get => _addressExtraInfo;

            set
            {
                _addressExtraInfo = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The post office's  chief phone.
        /// </summary>
        private string _chiefPhone;

        /// <summary>
        /// Gets or sets the chief phone.
        /// </summary>
        /// <value>The chief phone.</value>
        public string ChiefPhone
        {
            get => _chiefPhone;

            set
            {
                _chiefPhone = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The post office's  delivery department phone.
        /// </summary>
        private string _deliveryDepartmentPhone;

        /// <summary>
        /// Gets or sets the delivery department phone.
        /// </summary>
        /// <value>The delivery department phone.</value>
        public string DeliveryDepartmentPhone
        {
            get => _deliveryDepartmentPhone;

            set
            {
                _deliveryDepartmentPhone = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The post office's  telegraph phone.
        /// </summary>
        private string _telegraphPhone;

        /// <summary>
        /// Gets or sets the telegraph phone.
        /// </summary>
        /// <value>The telegraph phone.</value>
        public string TelegraphPhone
        {
            get => _telegraphPhone;

            set
            {
                _telegraphPhone = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:PostOfficesDataDisplayer.Models.OfficeContacts"/> class.
        /// </summary>
        /// <param name="postalCode">Postal code.</param>
        /// <param name="address">Address.</param>
        /// <param name="addressExtra">Address extra.</param>
        /// <param name="chiefPhone">Chief phone.</param>
        /// <param name="deliveryPhone">Delivery phone.</param>
        /// <param name="telegraphPhone">Telegraph phone.</param>
        public OfficeContacts(string postalCode, string address, string addressExtra, string chiefPhone, string deliveryPhone, string telegraphPhone)
        {
            this.PostalCode = postalCode;
            this.Address = address;
            this.AddressExtraInfo = addressExtra;
            this.ChiefPhone = chiefPhone;
            this.DeliveryDepartmentPhone = deliveryPhone;
            this.TelegraphPhone = telegraphPhone;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:PostOfficesDataDisplayer.Models.OfficeContacts"/> class.
        /// </summary>
        public OfficeContacts()
        {
            this.PostalCode = "";
            this.Address = "";
            this.AddressExtraInfo = "";
            this.ChiefPhone = "";
            this.DeliveryDepartmentPhone = "";
            this.TelegraphPhone = "";
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
