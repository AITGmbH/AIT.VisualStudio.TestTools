namespace AIT.VisualStudio.TestTools.Traits
{
    /// <summary>
    /// Expected test execution time
    /// </summary>
    public enum ExpectedExecutionTime
    {
        /// <summary>
        /// It takes up to 50 milliseconds to execute the test.
        /// </summary>
        Milliseconds,

        /// <summary>
        /// It takes up to 30 seconds to execute the test.
        /// </summary>
        Seconds,

        /// <summary>
        /// It takes up to 20 minutes to execute the test.
        /// </summary>
        Minutes,

        /// <summary>
        /// It takes up to 2 hours to execute the test.
        /// </summary>
        Hours
    }
}