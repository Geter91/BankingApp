using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BankingApp 
{
     partial class  BankView : INotifyPropertyChanged
    {

        public BankManager BankM { get; set; }

        public string Title { get; set; }

        private decimal backingBankSum;
        public decimal BankSum { get { return backingBankSum; } set { backingBankSum = value; OnPropertyChanged(); } }
        public ObservableCollection<Account> AccountList { get; set; }
        public ObservableCollection<Transfer> TransferList { get; set; }

        public decimal Watch { get { return msTimer; } set { msTimer = value; OnPropertyChanged(); } }
        public decimal msTimer;
        public BankView()
        {
            BankSum = 0;
            Title = "Demo Bank App";
            AccountList = new ObservableCollection<Account>();
            TransferList = new ObservableCollection<Transfer>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}