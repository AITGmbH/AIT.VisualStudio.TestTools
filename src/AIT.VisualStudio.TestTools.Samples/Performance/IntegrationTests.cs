using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AIT.VisualStudio.TestTools.Samples.Performance
{
    using System.Threading;

    using AIT.VisualStudio.TestTools.Common;

    using FluentAssertions;

    /// <summary>
    /// Performance test samples.
    /// </summary>
    [TestClass]
    public class IntegrationTests
    {
        /// <summary />
        [TestMethod]
        public void Method_Scenario_ExpectedResult()
        {
            using (var timeTaker = new TimeTaker())
            {
                // Execute methods which performance should get measured
                Thread.Sleep(100);

                timeTaker.ElapsedTime.Should().BeLessThan(TimeSpan.FromSeconds(1), "executing 'method' took longer than expected.");
            }
        }
    }
}
