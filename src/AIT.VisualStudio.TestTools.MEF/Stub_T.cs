using System;

namespace AIT.VisualStudio.TestTools.MEF
{
    /// <summary>
    /// Container for passing stubs which should replace a export to the composition container.
    /// </summary>
    public class Stub<T> : Stub
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Stub{T}" /> class.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public Stub(T instance)
        {
            this.Instance = instance;
        }

        /// <summary>
        /// Gets the type which should get replaced.
        /// </summary>
        public override Type Type
        {
            get
            {
                return typeof (T);
            }
        }
    }
}