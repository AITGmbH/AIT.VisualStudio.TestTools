namespace AIT.VisualStudio.TestTools.Samples.Calculator.Tests.System
{
    using AIT.VisualStudio.TestTools.Samples.Calculator.Tests.System.Map;
    using AIT.VisualStudio.TestTools.Traits;
    using AIT.VisualStudio.TestTools.CodedUI.Extensions;

    using Microsoft.VisualStudio.TestTools.UITesting;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Summary description for CalculatorTests
    /// </summary>
    [CodedUITest]
    public class CalculatorTests
    {
        /// <summary />
        [TestMethod]
        [CodedUiTestCategory]
        [Area("Sample\\Calculator")]
        public void Calculator_Add1And2_ResultIs3()
        {
            using (var client = new CalculatorClient())
            {
                client.LaunchApplicationUnderTest();
                client.Shell.Button1.Click();
                client.Shell.ButtonAdd.Click();
                client.Shell.Button2.Click();

                client.Shell.ButtonEquals.Click();

                Assert.AreEqual("3", client.Shell.Result.DisplayText);
            }
        }

        /// <summary />
        [TestMethod]
        [CodedUiTestCategory]
        [Area("Sample\\Calculator")]
        public void Calculator_Add122And110_ResultIs232()
        {
            using (var client = new CalculatorClient())
            {
                client.LaunchApplicationUnderTest();
                client.Shell.Enter(122);
                client.Shell.ButtonAdd.Click();
                client.Shell.Enter(110);

                client.Shell.ButtonEquals.Click();

                Assert.AreEqual("232", client.Shell.Result.DisplayText);
            }
        }

        /// <summary />
        [TestMethod]
        [CodedUiTestCategory]
        [Area("Sample\\Calculator")]
        public void Calculator_AdditionOfTwoPositiveNumbers_ResultIsCorrect()
        {
            using (var client = new CalculatorClient())
            {
                client.LaunchApplicationUnderTest();
                client.Shell.Enter(1);
                client.Shell.ButtonAdd.Click();
                client.Shell.Enter(299);

                client.Shell.ButtonEquals.Click();

                Assert.AreEqual("300", client.Shell.Result.DisplayText);
            }
        }

    }
}