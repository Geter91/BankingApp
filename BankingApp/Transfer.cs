using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace BankingApp
{
    class Transfer : INotifyPropertyChanged
    {
        public Account RecAccount { get; set; }
        public Account GivAccount { get; set; }
        public decimal Amount { get; set; }
        public int TransferNumber { get; set; }

        private string BackingTransferStatus;
        public string TransferStatus { get { return BackingTransferStatus; } set { BackingTransferStatus = value; OnPropertyChanged(); } }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public Transfer(Account rAccount, Account gAccount, decimal amt, int tNum)
        {
            RecAccount = rAccount;
            GivAccount = gAccount;
            Amount = amt;
            TransferNumber = tNum;
            TransferStatus = "incomplete";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool TransferFunds()
        {
            if (TransferStatus.Equals("complete"))
            {
                return true;
            }
            else if (GivAccount.CheckFunds(Amount) == false)
            {
                TransferStatus = "failed";
                return false;
            }

            GivAccount.Withdraw(Amount);
            RecAccount.Deposit(Amount);

            TransferStatus = "complete";
            return true;
        }

        public override string ToString()
        {
            return TransferNumber + ", " + TransferStatus;
        }



    }
}
