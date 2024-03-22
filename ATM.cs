using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment3_ATM
{
    public class ATM
    {
        private Account[] ac = new Account[3];

        /*
         * atm constructor
         */
        public ATM()
        {
            ac[0] = new Account(300, 1111, 111111);
            ac[1] = new Account(750, 2222, 222222);
            ac[2] = new Account(3000, 3333, 333333); 
        }

        /*
         * find account function, checks through all existing
         * accounts and compares input to account number
         */
        public Account findAccount(int input)
        {

            for (int i = 0; i < ac.Length; i++)
            {
                if (ac[i].getAccountNum() == input)
                {
                    return ac[i];
                }
            }

            return null;
        }
    }
}
