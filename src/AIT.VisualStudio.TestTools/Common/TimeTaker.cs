using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AIT.VisualStudio.TestTools.Common
{
    /// <summary>
    /// Takes the time which a test needs
    /// </summary>
    public class TimeTaker : IDisposable
    {
        #region Fields

        private bool _isStopped = false;

        private string Caller
        {
            get;
        }

        private Stopwatch StopWatch
        {
            get;
        }

        private TimeSpan ExpectedMaximumExecutionTime
        {
            get;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeTaker" /> class.
        /// </summary>
        /// <param name="expectedMaximumExecutionTime">The expected maximum execution time.</param>
        /// <param name="caller">The caller.</param>
        private TimeTaker(TimeSpan expectedMaximumExecutionTime, string caller)
        {
            ExpectedMaximumExecutionTime = expectedMaximumExecutionTime;
            Caller = caller;
            StopWatch = new Stopwatch();
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeTaker" /> class and starts tracking the time.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
        public static TimeTaker Begin(TimeSpan expectedMaximumExecutionTime, [CallerMemberName] string caller = "")
        {
            TimeTaker result;
            TimeTaker timeTaker = null;
            try
            {
                timeTaker = new TimeTaker(expectedMaximumExecutionTime, caller);
                timeTaker.StopWatch.Start();
                result = timeTaker;
                timeTaker = null;
            }
            finally
            {
                if (timeTaker != null)
                {
                    timeTaker.Dispose();
                }
            }
            return result;
        }

        /// <summary>
        /// Gets the elapsed time.
        /// </summary>
        public TimeSpan ElapsedTime
        {
            get
            {
                return StopWatch.Elapsed;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed", Justification = "This is a private method.")]
        private void End(bool throwExn, string message = "Maximum execution time was exceeded!")
        {
            if (_isStopped)
            {
                return;
            }

            _isStopped = true;
            StopWatch.Stop();
            var elapsedTime = StopWatch.Elapsed;
            Trace.WriteLine(Caller + ": " + elapsedTime);

            var msg = string.Format(CultureInfo.InvariantCulture, "{0}: {1}\nMax execution time: {2}, Actual: {3}.", Caller,
                message, ExpectedMaximumExecutionTime, elapsedTime);
            if (throwExn)
            {
                Assert.IsTrue(elapsedTime < ExpectedMaximumExecutionTime, msg);
            }
            else
            {
                Trace.WriteLine(msg);
            }
        }

        /// <summary>
        /// Ends the current execution time tracking and throws an exception if applicable
        /// </summary>
        /// <param name="message">the message for the exception or the trace message.</param>
        [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed", Justification = "Used from C# only.")]
        public void End(string message = "Maximum execution time was exceeded!")
        {
            End(true, message);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                End(false);
            }
        }
    }
}