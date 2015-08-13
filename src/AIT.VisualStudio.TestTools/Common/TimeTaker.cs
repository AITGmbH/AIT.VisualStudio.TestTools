namespace AIT.VisualStudio.TestTools.Common
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.CompilerServices;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Takes the time which a test needs
    /// </summary>
    public class TimeTaker : IDisposable
    {
        #region Fields

        private TimeSpan _expectedMaximumExecutionTime = TimeSpan.MaxValue;

        private string Caller
        {
            get;
            set;
        }

        private Stopwatch StopWatch
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeTaker" /> class.
        /// </summary>
        /// <param name="caller">The caller.</param>
        [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
        public TimeTaker([CallerMemberName] string caller = "")
        {
            Caller = caller;
            StopWatch = new Stopwatch();
            StopWatch.Start();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeTaker" /> class.
        /// </summary>
        /// <param name="expectedMaximumExecutionTime">The expected maximum execution time.</param>
        /// <param name="caller">The caller.</param>
        [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
        public TimeTaker(TimeSpan expectedMaximumExecutionTime, [CallerMemberName] string caller = "")
            : this(caller)
        {
            ExpectedMaximumExecutionTime = expectedMaximumExecutionTime;
        }

        #endregion

        #region Private Properties

        private TimeSpan ExpectedMaximumExecutionTime
        {
            get
            {
                return this._expectedMaximumExecutionTime;
            }
            set
            {
                this._expectedMaximumExecutionTime = value;
            }
        }

        #endregion

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
                var elapsedTime = StopWatch.Elapsed;
                Trace.WriteLine(Caller + ": " + elapsedTime);

                StopWatch.Stop();

                Assert.IsTrue(elapsedTime < ExpectedMaximumExecutionTime, "Expected max execution time exceeded.");
            }
        }
    }
}