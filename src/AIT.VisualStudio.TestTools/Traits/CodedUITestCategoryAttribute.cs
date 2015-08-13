namespace AIT.VisualStudio.TestTools.Traits
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using AIT.VisualStudio.TestTools.Traits.Base;

    /// <summary>
    /// Integration Test Attribute
    /// </summary>
    [SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes")]
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public class CodedUiTestCategoryAttribute : CustomTestAttribute
    {
        /// <summary>
        /// Creates a new instance of the <see cref="CodedUiTestCategoryAttribute" /> class.
        /// </summary>
        /// <param name="expectedExecutionTime">Expected execution time</param>
        public CodedUiTestCategoryAttribute(ExpectedExecutionTime expectedExecutionTime)
            : base(expectedExecutionTime)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="CodedUiTestCategoryAttribute" /> class.
        /// </summary>
        /// <param name="executionMode">Execution mode</param>
        public CodedUiTestCategoryAttribute(ExecutionMode executionMode)
            : base(executionMode)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="CodedUiTestCategoryAttribute" /> class.
        /// </summary>
        public CodedUiTestCategoryAttribute(ExpectedExecutionTime expectedExecutionTime, ExecutionMode executionMode)
            : base(expectedExecutionTime, executionMode)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CodedUiTestCategoryAttribute"/> class.
        /// </summary>
        public CodedUiTestCategoryAttribute()
            : base(ExpectedExecutionTime.Seconds)
        {
        }

        /// <summary>
        /// Gets the test type
        /// </summary>
        public override TestType TestType
        {
            get
            {
                return TestType.CodedUI;
            }
        }
    }
}