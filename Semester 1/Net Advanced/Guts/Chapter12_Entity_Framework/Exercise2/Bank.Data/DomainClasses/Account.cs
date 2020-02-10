using Bank.Data.DomainClasses.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Bank.Data.DomainClasses
{
    public class Account : INotifyPropertyChanged
    {
        private decimal balance;
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public AccountType AccountType { get; set; }
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public decimal Balance
        {
            get => balance;
            set
            {
                balance = value;
                OnPropertyChanged(nameof(Balance));
            }
        }

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
