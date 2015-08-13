using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIT.VisualStudio.TestTools.Traits
{
    using AIT.VisualStudio.TestTools.Traits.Base;

    /// <summary>
    /// Performance test attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public sealed class PerformanceTestAttribute : CustomTestAttribute
    {
        /// <summary>
        /// Creates a new instance of the <see cref="PerformanceTestAttribute" /> class.
        /// </summary>
        /// <param name="executionMode">Execution mode</param>
        public PerformanceTestAttribute(ExecutionMode executionMode) : base(executionMode) { }

        /// <summary>
        /// Creates a new instance of the <see cref="PerformanceTestAttribute" /> class.
        /// </summary>
        public PerformanceTestAttribute() { }

        /// <summary>
        /// Gets the expected execution time
        /// </summary>
        public override ExpectedExecutionTime ExpectedExecutionTime
        {
            get
            {
                return ExpectedExecutionTime.Hours;
            }
        }

        /// <summary>
        /// Gets the test type
        /// </summary>
        public override TestType TestType
        {
            get
            {
                return TestType.Performance;
            }
        }
    }
}
