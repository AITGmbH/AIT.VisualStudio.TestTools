namespace AIT.VisualSTudio.TestTools.Samples.Calculator.Tests.System.Map
{
    using AIT.VisualStudio.TestTools.UITesting;

    using global::System;
    using global::System.IO;

    public class CalculatorClient : TestClientBase
    {
        private CalculatorShell _shell;

        private static readonly string Calculator =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "system32", "calc.exe");

        public CalculatorClient()
            : base(Calculator)
        {
        }

        public CalculatorShell Shell
        {
            get
            {
                if (_shell == null)
                {
                    _shell = new CalculatorShell(ApplicationUnderTest);
                    _shell.WaitForControlReady(10000);
                }

                return _shell;
            }
        }
    }
}