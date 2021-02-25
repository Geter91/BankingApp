using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApp
{
    class Account

    {
        public int AccountID { get; private set; }
        public decimal AccountBalance { get; private set; }
        private readonly object balanceLock; 
        public Account(int AcntID)
        {
            AccountID = AcntID;
            AccountBalance = 1000;
            balanceLock = new object();
        }

        public Account(int AcntID, decimal AcntBal)
        {
            AccountID = AcntID;
            AccountBalance = AcntBal;
            balanceLock = new object();
        }

        public Account(decimal AcntBal)
        {
            AccountID = 0;
            AccountBalance = AcntBal;
            balanceLock = new object();
        }
        public Account()
        {
            AccountID = 0;
            AccountBalance = 1000;
            balanceLock = new object();
        }

        public bool CheckFunds(decimal amt)
        {
            lock (balanceLock)
            {
                if (amt > AccountBalance)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public void Withdraw(decimal amt)
        {
            lock (balanceLock)
            {
                AccountBalance = AccountBalance - amt;
            }
        }

        public void Deposit(decimal amt)
        {
            lock (balanceLock)
            {
                AccountBalance = AccountBalance + amt;
            }
        }

        public override string ToString()
        {
            return AccountID.ToString();
        }

    }
}

