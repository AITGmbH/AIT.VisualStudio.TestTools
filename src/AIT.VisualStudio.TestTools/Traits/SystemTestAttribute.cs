﻿namespace AIT.VisualStudio.TestTools.Traits
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using AIT.VisualStudio.TestTools.Traits.Base;

    /// <summary>
    /// Integration Test Attribute
    /// </summary>
    [SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes")]
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public class SystemTestAttribute : CustomTestAttribute
    {
        /// <summary>
        /// Creates a new instance of the <see cref="SystemTestAttribute" /> class.
        /// </summary>
        /// <param name="expectedExecutionTime">Expected execution time</param>
        public SystemTestAttribute(ExpectedExecutionTime expectedExecutionTime)
            : base(expectedExecutionTime)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="SystemTestAttribute" /> class.
        /// </summary>
        /// <param name="executionMode">Execution mode</param>
        public SystemTestAttribute(ExecutionMode executionMode)
            : base(executionMode)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="SystemTestAttribute" /> class.
        /// </summary>
        /// <param name="expectedExecutionTime">Expected execution time</param>
        /// <param name="executionMode">Execution mode</param>
        public SystemTestAttribute(ExpectedExecutionTime expectedExecutionTime, ExecutionMode executionMode)
            : base(expectedExecutionTime, executionMode)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="SystemTestAttribute" /> class.
        /// </summary>
        public SystemTestAttribute()
        {
        }

        /// <summary>
        /// Gets the test type
        /// </summary>
        public override TestType TestType
        {
            get
            {
                return TestType.System;
            }
        }
    }
}