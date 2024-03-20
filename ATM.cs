using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment3_ATM
{
    internal class ATM
    {
        readonly ATMForm form;

        public enum state
        {
            choosing_account,
            entering_pin,
            options,
            getting_withdraw_amount,
            display_balance
        }

        private state currentState;

        private Account[] ac;
        private Account activeAccount = null;

        public ATM(Account[] ac, ATMForm form)
        {
            this.form = form;
            currentState = state.choosing_account;
            this.ac = ac;  
        }

        public void runATM()
        {
            switch (this.getCurrentState())
            {
                case state.choosing_account:
                    {
                        form.setInstructionLabel("Enter your account number..");
                        if (form.isEnterButtonClicked)
                        {
                            activeAccount = this.findAccount();
                            if (activeAccount != null)
                            {
                                form.isEnterButtonClicked = false;
                                this.setCurrentState(state.entering_pin);
                            }
                            else
                            {
                                form.setInstructionLabel("no matching account found");
                            }
                            form.clearInputLabel();
                        }
                        break;
                    }
                case state.entering_pin:
                    {
                        form.setInstructionLabel("Enter pin: ");
                        if (form.isEnterButtonClicked)
                        {
                            if (activeAccount.checkPin(this.promptForPin()))
                            {
                                form.setInstructionLabel("correct");
                                form.isEnterButtonClicked = false;
                                this.setCurrentState(state.options);
                            }
                            else
                            {
                                form.setInstructionLabel("incorrect pin");
                            }
                            form.clearInputLabel();
                        }
                        break;
                    }
                case state.options:
                    {
                        form.setInstructionLabel("Options:");
                        form.setInstructionLabel2("1> take out cash");
                        form.setInstructionLabel3("2> balance");
                        form.setInstructionLabel4("3> return card");
                        if (form.isEnterButtonClicked)
                        {
                            processOptions();
                        }
                        break;
                    }
                case state.getting_withdraw_amount:
                    {
                        form.setInstructionLabel("Withdraw:");
                        form.setInstructionLabel2("1> 10");
                        form.setInstructionLabel3("2> 50");
                        form.setInstructionLabel4("3> 500");
                        if (form.isEnterButtonClicked)
                        {
                            form.clearAllLabels();
                            withdraw();
                        }
                        break;
                    }
                case state.display_balance:
                    {
                        form.setExtra("Your current balance is: " + activeAccount.getBalance());
                        currentState = state.options;
                        form.isEnterButtonClicked = false;
                        break;
                    }
            }
        }

        private void processOptions()
        {
            int input = form.getCurrentInput();

            if (input == 1) 
            {
                currentState = state.getting_withdraw_amount;
            }
            else if(input == 2)
            {
                currentState = state.display_balance;
            }
            else if(input == 3)
            {
                form.clearAllLabels();
                currentState = state.choosing_account;
            }
            else
            {

            }
            form.clearInputLabel();
            form.isEnterButtonClicked = false;
        }

        private void withdraw()
        {
            bool decrementPossible = false;
            int input = form.getCurrentInput();

            if(input > 0 && input < 4)
            {
                if(input== 1)
                {
                    if (activeAccount.decrementBalance(10))
                    {
                        decrementPossible = true;
                    }
                }
                else if(input == 2)
                {
                    if (activeAccount.decrementBalance(50))
                    {
                        decrementPossible = true;
                    }
                }
                else if(input == 3)
                {
                    if (activeAccount.decrementBalance(500))
                    {
                        decrementPossible= true;
                    }
                }
            }

            if (decrementPossible)
            {
                form.setExtra("new balance " + activeAccount.getBalance());
            }
            else
            {
                form.setInstructionLabel("insufficient funds");
            }
            currentState = state.options;
            form.clearInputLabel();
            form.isEnterButtonClicked = false;
        }

        private Account findAccount()
        {
            int accountNumberInput = form.getCurrentInput();

            for (int i = 0; i < ac.Length; i++)
            {
                if (ac[i].getAccountNum() == accountNumberInput)
                {
                    return ac[i];
                }
            }

            return null;
        }

        private void dispBalance()
        {
            if (this.activeAccount != null)
            {
                
            }
        }

        private int promptForPin()
        {
            int pinNumberEntered = form.getCurrentInput();
            return pinNumberEntered;
        }

        public state getCurrentState()
        {
            return this.currentState;
        }

        public void setCurrentState(state state)
        {
            this.currentState = state;
        }
    }
}
