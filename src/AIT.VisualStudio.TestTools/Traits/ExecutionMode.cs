namespace AIT.VisualStudio.TestTools.Traits
{
    /// <summary>
    /// Test Execution Mode
    /// </summary>
    public enum ExecutionMode
    {
        /// <summary>
        /// Tests runs automatically
        /// </summary>
        Automated,

        /// <summary>
        /// Tests must get executed manually.
        /// </summary>
        Manual,

        /// <summary>
        /// Test must get executed using a test runner.
        /// </summary>
        CodedUI
    }
}