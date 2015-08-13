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
    public class IntegrationTestAttribute : CustomTestAttribute
    {
        /// <summary>
        /// Creates a new instance of the <see cref="IntegrationTestAttribute" /> class.
        /// </summary>
        /// <param name="expectedExecutionTime">Expected execution time</param>
        public IntegrationTestAttribute(ExpectedExecutionTime expectedExecutionTime)
            : base(expectedExecutionTime)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="IntegrationTestAttribute" /> class.
        /// </summary>
        /// <param name="executionMode">Execution mode</param>
        public IntegrationTestAttribute(ExecutionMode executionMode)
            : base(executionMode)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="IntegrationTestAttribute" /> class.
        /// </summary>
        /// <param name="expectedExecutionTime">Expected execution time</param>
        /// <param name="executionMode">Execution mode</param>
        public IntegrationTestAttribute(ExpectedExecutionTime expectedExecutionTime, ExecutionMode executionMode)
            : base(expectedExecutionTime, executionMode)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="IntegrationTestAttribute" /> class.
        /// </summary>
        public IntegrationTestAttribute()
        {
        }

        /// <summary>
        /// Gets the test type
        /// </summary>
        public override TestType TestType
        {
            get
            {
                return TestType.Integration;
            }
        }
    }
}