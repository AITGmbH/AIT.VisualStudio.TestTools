namespace AIT.VisualStudio.TestTools.Traits.Base
{
    using System;
    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Custom test attributes base class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public abstract class CustomTestAttribute : TestCategoryBaseAttribute
    {
        #region Fields

        private readonly ExecutionMode _executionMode;

        private readonly ExpectedExecutionTime _expectedExecutionTime;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of the <see cref="CustomTestAttribute" /> class.
        /// </summary>
        protected CustomTestAttribute()
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="CustomTestAttribute" /> class.
        /// </summary>
        /// <param name="expectedExecutionTime">Expected execution time</param>
        protected CustomTestAttribute(ExpectedExecutionTime expectedExecutionTime)
        {
            this._expectedExecutionTime = expectedExecutionTime;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="CustomTestAttribute" /> class.
        /// </summary>
        /// <param name="executionMode">Execution mode</param>
        protected CustomTestAttribute(ExecutionMode executionMode)
        {
            this._executionMode = executionMode;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="CustomTestAttribute" /> class.
        /// </summary>
        /// <param name="expectedExecutionTime">Expected execution time</param>
        /// <param name="executionMode">Execution mode</param>
        protected CustomTestAttribute(ExpectedExecutionTime expectedExecutionTime, ExecutionMode executionMode)
        {
            this._expectedExecutionTime = expectedExecutionTime;
            this._executionMode = executionMode;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the expected execution time
        /// </summary>
        public virtual ExpectedExecutionTime ExpectedExecutionTime
        {
            get
            {
                return this._expectedExecutionTime;
            }
        }

        /// <summary>
        /// Gets the execution mode
        /// </summary>
        public virtual ExecutionMode ExecutionMode
        {
            get
            {
                return this._executionMode;
            }
        }

        /// <summary>
        /// Gets the test type
        /// </summary>
        public abstract TestType TestType
        {
            get;
        }

        #endregion

        #region Protected Properties

        /// <summary>
        /// Gets a value indicating whether this <see cref="CustomTestAttribute" /> is distributed.
        /// </summary>
        protected virtual bool Distributed
        {
            get
            {
                return false;
            }
        }

        #endregion

        #region Base Overrides

        /// <summary>
        /// Gets the test categories
        /// </summary>
        public override IList<string> TestCategories
        {
            get
            {
                var testCategories = new List<string>();

                testCategories.Add("Execution." + this.ExecutionMode);
                testCategories.Add("TestType." + this.TestType);

                if (this.Distributed)
                {
                    testCategories.Add("TestType.Distributed");
                    testCategories.Add("TestType." + this.TestType + ".Distributed");
                }

                switch (this.ExpectedExecutionTime)
                {
                    case ExpectedExecutionTime.Seconds:
                    case ExpectedExecutionTime.Milliseconds:
                        testCategories.Add("Execution." + this.ExecutionMode + ".Fast");
                        break;
                    case ExpectedExecutionTime.Minutes:
                        testCategories.Add("Execution." + this.ExecutionMode + ".Slow");
                        break;
                    default:
                        testCategories.Add("Execution." + this.ExecutionMode + ".VerySlow");
                        break;
                }

                return testCategories;
            }
        }

        #endregion
    }
}