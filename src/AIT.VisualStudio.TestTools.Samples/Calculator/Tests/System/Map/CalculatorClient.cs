namespace AIT.VisualStudio.TestTools.Samples.Calculator.Tests.System.Map
{
    using AIT.VisualStudio.TestTools.CodedUI;

    using global::System;
    using global::System.IO;

    /// <summary>
    /// Test-Client for the Windows Calculator
    /// </summary>
    public class CalculatorClient : TestClientBase
    {
        private CalculatorShell _shell;

        private static readonly string Calculator =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "calc.exe");

        /// <summary>
        /// Initializes a new CalculatorClient instance.
        /// </summary>
        public CalculatorClient()
            : base(Calculator)
        {
        }

        /// <summary>
        /// Gets the current CalculatorShell instance.
        /// </summary>
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