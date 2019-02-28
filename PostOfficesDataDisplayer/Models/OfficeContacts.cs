using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PostOfficesDataDisplayer.Models
{
    public class OfficeContacts: INotifyPropertyChanged
    {

        private string _postalCode;

        public string PostalCode
        {
            get => _postalCode;

            set
            {
                _postalCode = value;
                OnPropertyChanged();
            }
        }

        private string _address;

        public string Address
        {
            get => _address;

            set
            {
                _address = value;
                OnPropertyChanged();
            }
        }

        private string _addressExtraInfo;

        public string AddressExtraInfo
        {
            get => _addressExtraInfo;

            set
            {
                _addressExtraInfo = value;
                OnPropertyChanged();
            }
        }

        private string _chiefPhone;

        public string ChiefPhone
        {
            get => _chiefPhone;

            set
            {
                _chiefPhone = value;
                OnPropertyChanged();
            }
        }

        private string _deliveryDepartmentPhone;
        
        public string DeliveryDepartmentPhone
        {
            get => _deliveryDepartmentPhone;

            set
            {
                _deliveryDepartmentPhone = value;
                OnPropertyChanged();
            }
        }

        private string _telegraphPhone;

        public string TelegraphPhone
        {
            get => _telegraphPhone;

            set
            {
                _telegraphPhone = value;
                OnPropertyChanged();
            }
        }

        public OfficeContacts(string postalCode, string address, string addressExtra, string chiefPhone, string deliveryPhone, string telegraphPhone)
        {
            this.PostalCode = postalCode;
            this.Address = address;
            this.AddressExtraInfo = addressExtra;
            this.ChiefPhone = chiefPhone;
            this.DeliveryDepartmentPhone = deliveryPhone;
            this.TelegraphPhone = telegraphPhone;
        }


        public void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
