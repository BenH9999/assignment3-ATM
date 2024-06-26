namespace assignment3_ATM
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            //Application.Run();
            makeForms();
        }

        private static void makeForms()
        {
            //atm object
            ATM atm= new ATM();

            /*
             * creating both threads for both forms to run simultaneously
             */
            Thread atmForm1 = new Thread(() => Application.Run(new ATMForm(atm)));
            Thread atmForm2 = new Thread(() => Application.Run(new ATMForm(atm)));
            atmForm1.Start();
            atmForm2.Start();
        }
    }
}