namespace AIT.VisualStudio.TestTools.Samples.Calculator.Tests.System.Map
{
    using AIT.VisualStudio.TestTools.CodedUI.Extensions;

    using global::System.Globalization;

    using Microsoft.VisualStudio.TestTools.UITesting;
    using Microsoft.VisualStudio.TestTools.UITesting.WinControls;

    /// <summary>
    /// The Win32 calculator window.
    /// </summary>
    public class CalculatorShell : WinWindow
    {
        /// <summary>
        /// Create a new CalculatorShell instance
        /// </summary>
        /// <param name="parent">the parent control</param>
        public CalculatorShell(UITestControl parent)
            : base(parent)
        {
            this.SearchProperties[WinControl.PropertyNames.ControlId] = "131";
            this.WindowTitles.Add("Calculator");
        }

        /// <summary>
        /// The 1 button.
        /// </summary>
        public WinButton Button1
        {
            get
            {
                return
                    this.Find<WinButton>(
                        new PropertyExpressionCollection
                            {
                                new PropertyExpression(UITestControl.PropertyNames.Name, "1")
                            });
            }
        }

        /// <summary>
        /// The 2 button.
        /// </summary>
        public WinButton Button2
        {
            get
            {
                return
                    this.Find<WinButton>(
                        new PropertyExpressionCollection
                            {
                                new PropertyExpression(UITestControl.PropertyNames.Name, "2")
                            });
            }
        }

        /// <summary>
        /// The 3 button.
        /// </summary>
        public WinButton Button3
        {
            get
            {
                return
                    this.Find<WinButton>(
                        new PropertyExpressionCollection
                            {
                                new PropertyExpression(UITestControl.PropertyNames.Name, "3")
                            });
            }
        }

        /// <summary>
        /// The 4 button.
        /// </summary>
        public WinButton Button4
        {
            get
            {
                return
                    this.Find<WinButton>(
                        new PropertyExpressionCollection
                            {
                                new PropertyExpression(UITestControl.PropertyNames.Name, "4")
                            });
            }
        }

        /// <summary>
        /// The 5 button.
        /// </summary>
        public WinButton Button5
        {
            get
            {
                return
                    this.Find<WinButton>(
                        new PropertyExpressionCollection
                            {
                                new PropertyExpression(UITestControl.PropertyNames.Name, "5")
                            });
            }
        }

        /// <summary>
        /// The 6 button.
        /// </summary>
        public WinButton Button6
        {
            get
            {
                return
                    this.Find<WinButton>(
                        new PropertyExpressionCollection
                            {
                                new PropertyExpression(UITestControl.PropertyNames.Name, "6")
                            });
            }
        }

        /// <summary>
        /// The 7 button.
        /// </summary>
        public WinButton Button7
        {
            get
            {
                return
                    this.Find<WinButton>(
                        new PropertyExpressionCollection
                            {
                                new PropertyExpression(UITestControl.PropertyNames.Name, "7")
                            });
            }
        }

        /// <summary>
        /// The 8 button.
        /// </summary>
        public WinButton Button8
        {
            get
            {
                return
                    this.Find<WinButton>(
                        new PropertyExpressionCollection
                            {
                                new PropertyExpression(UITestControl.PropertyNames.Name, "8")
                            });
            }
        }

        /// <summary>
        /// The 9 button.
        /// </summary>
        public WinButton Button9
        {
            get
            {
                return
                    this.Find<WinButton>(
                        new PropertyExpressionCollection
                            {
                                new PropertyExpression(UITestControl.PropertyNames.Name, "9")
                            });
            }
        }

        /// <summary>
        /// The 0 button.
        /// </summary>
        public WinButton Button0
        {
            get
            {
                return
                    this.Find<WinButton>(
                        new PropertyExpressionCollection
                            {
                                new PropertyExpression(UITestControl.PropertyNames.Name, "0")
                            });
            }
        }

        /// <summary>
        /// The result button.
        /// </summary>
        public WinText Result
        {
            get
            {
                var container =
                    this.Find<WinWindow>(
                        new PropertyExpressionCollection { new PropertyExpression(WinControl.PropertyNames.ControlId, "150") });

                container.WindowTitles.Add("Calculator");

                return
                    container.Find<WinText>(
                        new PropertyExpressionCollection
                            {
                                new PropertyExpression(
                                    UITestControl.PropertyNames.Name,
                                    "Result")
                            });
            }
        }

        /// <summary>
        /// The equals button.
        /// </summary>
        public WinButton ButtonEquals
        {
            get
            {
                return
                    this.Find<WinButton>(
                        new PropertyExpressionCollection
                            {
                                new PropertyExpression(
                                    UITestControl.PropertyNames.Name,
                                    "Equals")
                            });
            }
        }

        /// <summary>
        /// The add button.
        /// </summary>
        public WinButton ButtonAdd
        {
            get
            {
                return
                    this.Find<WinButton>(
                        new PropertyExpressionCollection
                            {
                                new PropertyExpression(
                                    UITestControl.PropertyNames.Name,
                                    "Add")
                            });
            }
        }

        /// <summary>
        /// Enter the given number.
        /// </summary>
        /// <param name="number">the number to enter into the calculator.</param>
        public void Enter(int number)
        {
            var digits = number.ToString(CultureInfo.InvariantCulture).ToCharArray();

            foreach (var digit in digits)
            {
                var button =
                    this.Find<WinButton>(
                        new PropertyExpressionCollection
                            {
                                new PropertyExpression(
                                    UITestControl.PropertyNames.Name,
                                    digit.ToString(CultureInfo.InvariantCulture))
                            });

                button.Click();
            }
        }
    }
}