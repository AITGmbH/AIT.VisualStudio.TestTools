﻿using System;

using AIT.VisualStudio.TestTools.Samples.Properties;

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
            using (var timeTaker = TimeTaker.Begin(TimeSpan.FromSeconds(1)))
            {
                // Execute methods which performance should get measured
                Thread.Sleep(100);

                timeTaker.End(Resources.IntegrationTests_Method_Scenario_ExpectedResult_executing__method__took_longer_than_expected_);
            }
        }
    }
}
