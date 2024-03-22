using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace assignment3_ATM
{
    public partial class ATMForm : Form
    {
        /*
         * enum for tracking which state the program is currently in
         */
        public enum state
        {
            choosing_account,
            entering_pin,
            options,
            getting_withdraw_amount,
            display_balance
        }


        /*
         * initialising variables
         */
        private state currentState;

        private Account activeAccount = null;

        private ATM atm;
        private Thread atmThread;

        public bool isEnterButtonClicked;
        public bool isCancelButtonClicked;

        const int cell_size = 80;
        Button[,] numberButtons = new Button[3, 3];
        Button zeroButton = new Button();
        Button enterButton = new Button();
        Button cancelButton = new Button();
        int currentButtonNo;
        
        /*
         * form constructor that passes in
         * a single atm object
         */
        public ATMForm(ATM atm)
        {
            InitializeComponent();
            addControls();

            currentState = state.choosing_account;
            this.atm = atm;

            this.lblExtra.Text = "";
            this.lblInput.Text = "";
            this.lblInstruction.Text = "";
            this.lblInstruction2.Text = "";
            this.lblInstruction3.Text = "";
            this.lblInstruction4.Text = "";

            isEnterButtonClicked = false;
            isCancelButtonClicked = false;

            /*
             * thread for running form at same time as loop
             */
            atmThread = new Thread(this.mainATMLoop);
            atmThread.Start();
        }

        /*
         * main loop
         */
        public void mainATMLoop()
        {
            while (true)
            {
                runATM();
                Thread.Sleep(200);
            }
        }

        public void runATM()
        {
            /*
             * switch to efficiently cycle through program states
             */
            switch (this.getCurrentState())
            {
                case state.choosing_account:
                    {
                        if (lblExtra.Text != "") clearAllLabels();
                        setInstructionLabel("Enter your account number..");
                        if (isEnterButtonClicked)
                        {
                            isEnterButtonClicked = false;
                            activeAccount = atm.findAccount(getCurrentInput());
                            if (activeAccount != null)
                            {
                                this.setCurrentState(state.entering_pin);
                            }
                            else
                            {
                                setExtra("no matching account found");
                            }
                            clearInputLabel();
                        }
                        break;
                    }
                case state.entering_pin:
                    {
                        /*
                         * checking for cancel button being pressed and then
                         * going back to first state
                         */
                        if (isCancelButtonClicked)
                        {
                            isCancelButtonClicked = false;
                            setExtra("Returning card..");
                            Thread.Sleep(800);
                            clearAllLabels();
                            currentState = state.choosing_account;
                        }
                        if (lblExtra.Text != "") clearAllLabels();
                        setInstructionLabel("Enter pin: ");
                        /*
                         * check if enter button is clicked and carry out
                         * operations for the current state
                         */
                        if (isEnterButtonClicked)
                        {
                            isEnterButtonClicked = false;
                            if (activeAccount.checkPin(this.promptForPin()))
                            {
                                setInstructionLabel("correct");
                                this.setCurrentState(state.options);
                            }
                            else
                            {
                                setInstructionLabel("incorrect pin");
                            }
                            clearInputLabel();
                        }
                        break;
                    }
                case state.options:
                    {
                        /*
                         * checking for cancel button being pressed and then
                         * going back to first state
                         */
                        if (isCancelButtonClicked)
                        {
                            isCancelButtonClicked = false;
                            setExtra("Returning card..");
                            Thread.Sleep(800);
                            clearAllLabels();
                            currentState = state.choosing_account;
                        }
                        if (lblExtra.Text != "") clearAllLabels();
                        setInstructionLabel("Options:");
                        setInstructionLabel2("1> take out cash");
                        setInstructionLabel3("2> balance");
                        setInstructionLabel4("3> return card");
                        /*
                         * check if enter button is clicked and carry out
                         * operations for the current state
                         */
                        if (isEnterButtonClicked)
                        {
                            processOptions();
                        }
                        break;
                    }
                case state.getting_withdraw_amount:
                    {
                        /*
                         * checking for cancel button being pressed and then
                         * going back to first state
                         */
                        if (isCancelButtonClicked)
                        {
                            isCancelButtonClicked = false;
                            setExtra("Returning card..");
                            Thread.Sleep(800);
                            clearAllLabels();
                            currentState = state.choosing_account;
                        }
                        setInstructionLabel("Withdraw:");
                        setInstructionLabel2("1> 10");
                        setInstructionLabel3("2> 50");
                        setInstructionLabel4("3> 500");
                        /*
                         * check if enter button is clicked and carry out
                         * operations for the current state
                         */
                        if (isEnterButtonClicked)
                        {
                            clearAllLabels();
                            withdraw();
                        }
                        break;
                    }
                case state.display_balance:
                    {
                        /*
                         * just print balance on the lblExtra for 800ms then return
                         * to options state
                         */
                        setExtra("Your current balance is: " + activeAccount.getBalance());
                        Thread.Sleep(800);
                        currentState = state.options;
                        isEnterButtonClicked = false;
                        break;
                    }
            }
        }

        private void processOptions()
        {
            //get current input from lblInput.Text
            int input = getCurrentInput();

            /*
             * check input and if invalid print error for 800ms
             */
            if (input == 1)
            {
                currentState = state.getting_withdraw_amount;
            }
            else if (input == 2)
            {
                currentState = state.display_balance;
            }
            else if (input == 3)
            {
                clearAllLabels();
                currentState = state.choosing_account;
            }
            else
            {
                setExtra("Invalid input");
                Thread.Sleep(800);
            }
            clearInputLabel();
            isEnterButtonClicked = false;
        }

        /*
         * withdraw function
         */
        private void withdraw()
        {
            bool decrementPossible = false;
            int input = getCurrentInput();

            /*
             * check input and decrement balance accordingly
             */
            if (input > 0 && input < 4)
            {
                if (input == 1)
                {
                    if (activeAccount.decrementBalance(10))
                    {
                        decrementPossible = true;
                    }
                }
                else if (input == 2)
                {
                    if (activeAccount.decrementBalance(50))
                    {
                        decrementPossible = true;
                    }
                }
                else if (input == 3)
                {
                    if (activeAccount.decrementBalance(500))
                    {
                        decrementPossible = true;
                    }
                }
            }
            else
            {
                setExtra("Invalid input");
                Thread.Sleep(800);
            }

            /*
             * print resulting messages
             */
            if (decrementPossible)
            {
                setExtra("new balance " + activeAccount.getBalance());
                Thread.Sleep(800);
            }
            else
            {
                setInstructionLabel("insufficient funds");
            }
            currentState = state.options;
            clearInputLabel();
            isEnterButtonClicked = false;
        }

        /*
         * get pin
         */
        private int promptForPin()
        {
            int pinNumberEntered = getCurrentInput();
            return pinNumberEntered;
        }

        /*
         * event handler for all number button presses,
         * gets number on the button.text and outputs it to lblInput.Text
         */
        public void numberButtonClicked(object sender, EventArgs e)
        {
            if((Button)sender != null)
            {
                this.lblInput.Text = lblInput.Text + ((Button)sender).Text;
            }
        }

        /*
         * setting enter button bool to true if clicked
         */
        public void enterButtonClicked(object sender, EventArgs e)
        {
            isEnterButtonClicked = true;
        }

        /*
         * setting cancel button bool to true if clicked
         */
        public void cancelButtonClicked(object sender, EventArgs e)
        {
            isCancelButtonClicked = true;
        }

        /*
         * clear all labels function for cleaner code
         */
        public void clearAllLabels()
        {
            setExtra("");
            setInstructionLabel("");
            setInstructionLabel2("");
            setInstructionLabel3("");
            setInstructionLabel4("");
        }

        /*
         * get the lblInput.Text and output as int, 
         * if empty return 0
         */
        public int getCurrentInput()
        {
            string inputString = lblInput.Text == "" ? "0" : lblInput.Text;
            return Int32.Parse(inputString);
        }

        public void setExtra(string text)
        {
            if (!this.IsHandleCreated || this.IsDisposed) return;
            if (!lblExtra.IsHandleCreated || lblExtra.IsDisposed) return;

            this.lblExtra.Invoke((MethodInvoker)delegate
            {
                lblExtra.Text = text;
            });
        }

        /*
         * changing label on threds led to issues
         * 
         * solved with solution from:
         * https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.methodinvoker?view=windowsdesktop-8.0
         */
        public void setInstructionLabel(string text)
        {
            if (!this.IsHandleCreated || this.IsDisposed) return;
            if (!lblInstruction.IsHandleCreated || lblInstruction.IsDisposed) return;

            this.lblInstruction.Invoke((MethodInvoker)delegate
            {
                lblInstruction.Text = text;
            });
        }

        public void clearInputLabel()
        {
            if (!this.IsHandleCreated || this.IsDisposed) return;
            if (!lblInput.IsHandleCreated || lblInput.IsDisposed) return;

            this.lblInput.Invoke((MethodInvoker)delegate
            {
                lblInput.Text = "";
            });
        }

        public void setInstructionLabel2(string text)
        {
            if (!this.IsHandleCreated || this.IsDisposed) return;
            if (!lblInstruction2.IsHandleCreated || lblInstruction2.IsDisposed) return;

            this.lblInstruction2.Invoke((MethodInvoker)delegate
            {
                lblInstruction2.Text = text;
            });
        }

        public void setInstructionLabel3(string text)
        {
            if (!this.IsHandleCreated || this.IsDisposed) return;
            if (!lblInstruction3.IsHandleCreated || lblInstruction3.IsDisposed) return;

            this.lblInstruction3.Invoke((MethodInvoker)delegate
            {
                lblInstruction3.Text = text;
            });
        }

        public void setInstructionLabel4(string text)
        {
            if (!this.IsHandleCreated || this.IsDisposed) return;
            if (!lblInstruction4.IsHandleCreated || lblInstruction4.IsDisposed) return;

            this.lblInstruction4.Invoke((MethodInvoker)delegate
            {
                lblInstruction4.Text = text;
            });
        }

        /*
         * getter and setter for state
         */
        public state getCurrentState()
        {
            return this.currentState;
        }

        public void setCurrentState(state state)
        {
            this.currentState = state;
        }

        /*
         * function to add buttons to program
         */
        private void addControls()
        {
            currentButtonNo = 9;
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    numberButtons[x, y] = new Button();
                    numberButtons[x, y].SetBounds((x + 1) * cell_size, (y + 1) * cell_size, cell_size, cell_size);
                    numberButtons[x, y].Text = currentButtonNo.ToString();
                    numberButtons[x, y].Click += new EventHandler(this.numberButtonClicked);
                    Controls.Add(numberButtons[x, y]);
                    currentButtonNo--;
                }
            }
            zeroButton = new Button();
            zeroButton.SetBounds(1 * cell_size, 4 * cell_size, cell_size, cell_size);
            zeroButton.Text = "0";
            zeroButton.Click += new EventHandler(this.numberButtonClicked);
            Controls.Add(zeroButton);

            enterButton = new Button();
            enterButton.SetBounds(2 * cell_size, 4 * cell_size, cell_size, cell_size);
            enterButton.Text = "Enter";
            enterButton.Click += new EventHandler(this.enterButtonClicked);
            enterButton.BackColor = Color.Green;
            Controls.Add(enterButton);

            cancelButton = new Button();
            cancelButton.SetBounds(3 * cell_size, 4 * cell_size, cell_size, cell_size);
            cancelButton.Text = "Cancel";
            cancelButton.Click += new EventHandler(this.cancelButtonClicked);
            cancelButton.BackColor = Color.Red;
            Controls.Add(cancelButton);
        }
    }
}