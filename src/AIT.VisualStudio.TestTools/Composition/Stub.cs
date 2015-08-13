namespace AIT.VisualStudio.TestTools.Composition
{
    using System;

    /// <summary>
    /// Container for passing stubs which should replace a export to the composition container.
    /// </summary>
    public abstract class Stub
    {
        /// <summary>
        /// Gets the type which should get replaced.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
        public abstract Type Type { get; }

        /// <summary>
        /// Gets the instance which should replace the export.
        /// </summary>
        public object Instance { get; protected set; }
    }
}