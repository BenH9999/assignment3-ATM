using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace assignment3_ATM
{
    public partial class ATMForm : Form
    {
        public enum state
        {
            choosing_account,
            entering_pin,
            options,
            getting_withdraw_amount,
            display_balance
        }

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

            //atm = new ATM(ac,this);
            atmThread = new Thread(this.mainATMLoop);
            atmThread.Start();
        }

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
            switch (this.getCurrentState())
            {
                case state.choosing_account:
                    {
                        setInstructionLabel("Enter your account number..");
                        if (isEnterButtonClicked)
                        {
                            activeAccount = atm.findAccount(getCurrentInput());
                            if (activeAccount != null)
                            {
                                isEnterButtonClicked = false;
                                this.setCurrentState(state.entering_pin);
                            }
                            else
                            {
                                setInstructionLabel("no matching account found");
                            }
                            clearInputLabel();
                        }
                        break;
                    }
                case state.entering_pin:
                    {
                        setInstructionLabel("Enter pin: ");
                        if (isEnterButtonClicked)
                        {
                            if (activeAccount.checkPin(this.promptForPin()))
                            {
                                setInstructionLabel("correct");
                                isEnterButtonClicked = false;
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
                        setInstructionLabel("Options:");
                        setInstructionLabel2("1> take out cash");
                        setInstructionLabel3("2> balance");
                        setInstructionLabel4("3> return card");
                        if (isEnterButtonClicked)
                        {
                            processOptions();
                        }
                        break;
                    }
                case state.getting_withdraw_amount:
                    {
                        setInstructionLabel("Withdraw:");
                        setInstructionLabel2("1> 10");
                        setInstructionLabel3("2> 50");
                        setInstructionLabel4("3> 500");
                        if (isEnterButtonClicked)
                        {
                            clearAllLabels();
                            withdraw();
                        }
                        break;
                    }
                case state.display_balance:
                    {
                        setExtra("Your current balance is: " + activeAccount.getBalance());
                        currentState = state.options;
                        isEnterButtonClicked = false;
                        break;
                    }
            }
        }

        private void processOptions()
        {
            int input = getCurrentInput();

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

            }
            clearInputLabel();
            isEnterButtonClicked = false;
        }

        private void withdraw()
        {
            bool decrementPossible = false;
            int input = getCurrentInput();

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

            if (decrementPossible)
            {
                setExtra("new balance " + activeAccount.getBalance());
            }
            else
            {
                setInstructionLabel("insufficient funds");
            }
            currentState = state.options;
            clearInputLabel();
            isEnterButtonClicked = false;
        }

        private int promptForPin()
        {
            int pinNumberEntered = getCurrentInput();
            return pinNumberEntered;
        }

        public void numberButtonClicked(object sender, EventArgs e)
        {
            if((Button)sender != null)
            {
                this.lblInput.Text = lblInput.Text + ((Button)sender).Text;
            }
        }

        public void enterButtonClicked(object sender, EventArgs e)
        {
            isEnterButtonClicked = true;
        }

        public void cancelButtonClicked(object sender, EventArgs e)
        {
            isCancelButtonClicked = true;
        }

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

        public void clearAllLabels()
        {
            //clearInputLabel();
            setInstructionLabel("");
            setInstructionLabel2("");
            setInstructionLabel3("");
            setInstructionLabel4("");
        }

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

        public state getCurrentState()
        {
            return this.currentState;
        }

        public void setCurrentState(state state)
        {
            this.currentState = state;
        }

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
            cancelButton.BackColor = Color.Red;
            Controls.Add(cancelButton);
        }
    }
}