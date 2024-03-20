using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment3_ATM
{
    internal class Account
    {
        private int balance;
        private int pin;
        private int accountNum;

        public Account(int balance, int pin, int accountNum)
        {
            this.balance = balance;
            this.pin = pin;
            this.accountNum = accountNum;
        }

        public int getBalance()
        {
            return balance;
        }
        public void setBalance(int newBalance)
        {
            this.balance = newBalance;
        }

        public Boolean decrementBalance(int amount)
        {
            if(this.balance >amount)
            {
                balance -= amount;
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean checkPin(int pinEntered)
        {
            if(pinEntered == pin)
            {
                return true;
            }
            else 
            { 
                return false;
            }
        }

        public int getAccountNum()
        {
            return accountNum;
        }
    }
}
