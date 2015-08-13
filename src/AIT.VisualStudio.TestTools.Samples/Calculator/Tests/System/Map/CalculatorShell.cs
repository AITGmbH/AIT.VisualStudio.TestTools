namespace AIT.VisualSTudio.TestTools.Samples.Calculator.Tests.System.Map
{
    using AIT.VisualStudio.TestTools.UITesting.Extensions;

    using global::System.Globalization;

    using Microsoft.VisualStudio.TestTools.UITesting;
    using Microsoft.VisualStudio.TestTools.UITesting.WinControls;

    public class CalculatorShell : WinWindow
    {
        public CalculatorShell(UITestControl parent)
            : base(parent)
        {
            #region Search Criteria

            this.SearchProperties[WinControl.PropertyNames.ControlId] = "131";
            this.WindowTitles.Add("Calculator");

            #endregion
        }

        public WinButton Button1
        {
            get
            {
                return
                    this.Get<WinButton>(
                        new PropertyExpressionCollection
                            {
                                new PropertyExpression(UITestControl.PropertyNames.Name, "1")
                            });
            }
        }

        public WinButton Button2
        {
            get
            {
                return
                    this.Get<WinButton>(
                        new PropertyExpressionCollection
                            {
                                new PropertyExpression(UITestControl.PropertyNames.Name, "2")
                            });
            }
        }

        public WinButton Button3
        {
            get
            {
                return
                    this.Get<WinButton>(
                        new PropertyExpressionCollection
                            {
                                new PropertyExpression(UITestControl.PropertyNames.Name, "3")
                            });
            }
        }

        public WinButton Button4
        {
            get
            {
                return
                    this.Get<WinButton>(
                        new PropertyExpressionCollection
                            {
                                new PropertyExpression(UITestControl.PropertyNames.Name, "4")
                            });
            }
        }

        public WinButton Button5
        {
            get
            {
                return
                    this.Get<WinButton>(
                        new PropertyExpressionCollection
                            {
                                new PropertyExpression(UITestControl.PropertyNames.Name, "5")
                            });
            }
        }

        public WinButton Button6
        {
            get
            {
                return
                    this.Get<WinButton>(
                        new PropertyExpressionCollection
                            {
                                new PropertyExpression(UITestControl.PropertyNames.Name, "6")
                            });
            }
        }

        public WinButton Button7
        {
            get
            {
                return
                    this.Get<WinButton>(
                        new PropertyExpressionCollection
                            {
                                new PropertyExpression(UITestControl.PropertyNames.Name, "7")
                            });
            }
        }

        public WinButton Button8
        {
            get
            {
                return
                    this.Get<WinButton>(
                        new PropertyExpressionCollection
                            {
                                new PropertyExpression(UITestControl.PropertyNames.Name, "8")
                            });
            }
        }

        public WinButton Button9
        {
            get
            {
                return
                    this.Get<WinButton>(
                        new PropertyExpressionCollection
                            {
                                new PropertyExpression(UITestControl.PropertyNames.Name, "9")
                            });
            }
        }

        public WinButton Button0
        {
            get
            {
                return
                    this.Get<WinButton>(
                        new PropertyExpressionCollection
                            {
                                new PropertyExpression(UITestControl.PropertyNames.Name, "0")
                            });
            }
        }

        public WinText Result
        {
            get
            {
                var container =
                    this.Get<WinWindow>(
                        new PropertyExpressionCollection { new PropertyExpression(PropertyNames.ControlId, "150") });

                container.WindowTitles.Add("Calculator");

                return
                    container.Get<WinText>(
                        new PropertyExpressionCollection
                            {
                                new PropertyExpression(
                                    UITestControl.PropertyNames.Name,
                                    "Result")
                            });
            }
        }

        public WinButton ButtonEquals
        {
            get
            {
                return
                    this.Get<WinButton>(
                        new PropertyExpressionCollection
                            {
                                new PropertyExpression(
                                    UITestControl.PropertyNames.Name,
                                    "Equals")
                            });
            }
        }

        public WinButton ButtonAdd
        {
            get
            {
                return
                    this.Get<WinButton>(
                        new PropertyExpressionCollection
                            {
                                new PropertyExpression(
                                    UITestControl.PropertyNames.Name,
                                    "Add")
                            });
            }
        }

        public void Enter(int number)
        {
            var digits = number.ToString(CultureInfo.InvariantCulture).ToCharArray();

            foreach (var digit in digits)
            {
                var button =
                    this.Get<WinButton>(
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