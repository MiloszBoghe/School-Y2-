using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PayablesData
{
    public class Vendor : INotifyPropertyChanged
    {        
        public event PropertyChangedEventHandler PropertyChanged;

        private int _vendorId;
        private string _name;
        private string _address1;
        private string _address2;
        private string _city;
        private string _state;
        private string _zipCode;
        private string _phone;
        private string _contactLName;
        private string _contactFName;
        private int _defaultTermsId;
        private int _defaultAccountNo;

        public int VendorId
        {
            get => _vendorId; // same as get { return _vendorId; }
            set
            {
                _vendorId = value;
                OnPropertyChanged(); // no need to pass the name of the property thanks to [CallerMemberName] attritbute in OnPropertyChanged method
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }    

        public string Address1
        {
            get => _address1;
            set
            {
                _address1 = value;
                OnPropertyChanged();
            }
        }

        public string Address2
        {
            get => _address2;
            set
            {
                _address2 = value;
                OnPropertyChanged();
            }
        }

        public string City
        {
            get => _city;
            set
            {
                _city = value;
                OnPropertyChanged();
            }
        }

        public string State
        {
            get => _state;
            set
            {
                _state = value;
                OnPropertyChanged();
            }
        }

        public string ZipCode
        {
            get => _zipCode;
            set
            {
                _zipCode = value;
                OnPropertyChanged();
            }
        }

        public string Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                OnPropertyChanged();
            }
        }

        public string ContactLName
        {
            get => _contactLName;
            set
            {
                _contactLName = value;
                OnPropertyChanged();
            }
        }

        public string ContactFName
        {
            get => _contactFName;
            set
            {
                _contactFName = value;
                OnPropertyChanged();
            }
        }

        public int DefaultTermsId
        {
            get => _defaultTermsId;
            set
            {
                _defaultTermsId = value;
                OnPropertyChanged();
            }
        }

        public int DefaultAccountNo
        {
            get => _defaultAccountNo;
            set
            {
                _defaultAccountNo = value;
                OnPropertyChanged();
            }
        }

        private void OnPropertyChanged([CallerMemberName] string caller = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }
    }
}
