using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment3_ATM
{
    public class Account
    {
        private int balance;
        private int pin;
        private int accountNum;

        /*
         * account constructor that passes in current balance,
         * pin and account number for initialisation
         */
        public Account(int balance, int pin, int accountNum)
        {
            this.balance = balance;
            this.pin = pin;
            this.accountNum = accountNum;
        }

        /*
         * balance getter and setter
         */
        public int getBalance()
        {
            return balance;
        }
        public void setBalance(int newBalance)
        {
            this.balance = newBalance;
        }

        /*
         * checks if program can decrement, returns false if cannot
         * and returns true along with carrying out the operation
         * if true
         */
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

        /*
         * check pin, returns true/false depending if valid
         */
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

        /*
         * account number getter
         */
        public int getAccountNum()
        {
            return accountNum;
        }
    }
}
