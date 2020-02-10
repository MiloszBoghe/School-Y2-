using Bank.DomainClasses.Enums;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Bank.DomainClasses
{
    public class Account : INotifyPropertyChanged
    {
        private decimal _balance;
        // DONE: vul deze klasse aan
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance
        {
            get => _balance; set
            {
                _balance = value;
                OnPropertyChanged(nameof(Balance));
            }
        }
        public AccountType AccountType { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }

    }
}
