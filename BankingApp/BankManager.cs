using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace BankingApp
{

    class BankManager : INotifyPropertyChanged
    {
        public BankView BankV { get; set; }

        private decimal backingBankSum;
        public decimal BankSum { get { return backingBankSum; } private set { backingBankSum = value; OnPropertyChanged(); } }
        public ObservableCollection<Account> AccountList { get; set; }
        public ObservableCollection<Transfer> TransferList { get; set; }
        public decimal Watch { get { return msTimer; } set { msTimer = value; OnPropertyChanged(); }  }
        decimal msTimer;

        public string bankTotalString;
        public int LastTransfer;

        public event PropertyChangedEventHandler PropertyChanged;

        public BankManager()
        {
            AccountList = new ObservableCollection<Account>();
            TransferList = new ObservableCollection<Transfer>();
            CreateAccountList();
            BankSum = SumAccount();
            Console.WriteLine(BankSum);
        }

        public BankManager(BankView BankV)
        {
            LastTransfer = 1;
            this.BankV = BankV;
            AccountList = new ObservableCollection<Account>();
            TransferList = new ObservableCollection<Transfer>();
            CreateAccountList();
            BankSum = SumAccount();
            CreateTransactions();
        }

        public void ProccessTransfers()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            //foreach(Transfer trns in TransferList)
            {
                //trns.TransferFunds();
            }

            Parallel.ForEach(TransferList, trns =>
            {
                trns.TransferFunds();
            });

            watch.Stop();
            Watch = (decimal)watch.ElapsedMilliseconds;
            BankV.Watch = Watch;
            BankSum = SumAccount();
        }

        public void CreateAccountList()
        {
            //create 100 accounts at the start
            for(int i = 0; i < 100; i++)
            {
                Account newAct = new Account(i + 1);
                AccountList.Add(newAct);
            }
            BankV.AccountList = this.AccountList;
        }

        public decimal SumAccount()
        {
            decimal accountsSum = 0;
            foreach (Account Acnt in AccountList)
            {
                accountsSum += Acnt.AccountBalance;
            }
            bankTotalString = BankTotalToSting(accountsSum);
            BankV.BankSum = accountsSum;
            return accountsSum;
        }

        public string BankTotalToSting(decimal total)
        {
            return "Bank Total: " + total;
        }

        public void CreateTransactions()
        {
            TransferList.Clear();
            for (int i = 0; i < 9999999; i++)
            {
                Random rnd = new Random();
                int rec = rnd.Next(0, 100);
                int giv = rnd.Next(0, 100);
                decimal amt = (rnd.Next(1, 5) + (decimal)((rnd.Next(0,99)/100f)));
                Transfer newTransfer = new Transfer(AccountList[rec] ,AccountList[giv] ,amt , LastTransfer);
                TransferList.Add(newTransfer);
                LastTransfer = LastTransfer + 1; ;
            }
            
            BankV.TransferList = this.TransferList;
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
