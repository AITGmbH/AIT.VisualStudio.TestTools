using System;

using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Microsoft.VisualStudio.TestTools.UITesting.WpfControls;

namespace AIT.VisualStudio.TestTools.CodedUI.Extensions
{
    /// <summary>
    /// Test extensions for <see cref=" WpfTabPage" />s.
    /// </summary>
    public static class WpfTabPageExtensions
    {
        /// <summary>
        /// Determines whether the specified control is selected.
        /// </summary>
        /// <param name="tabPage">The tab page.</param>

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static bool IsSelected(this WpfTabPage tabPage)
        {
            if (tabPage == null)
            {
                throw new ArgumentNullException("tabPage");
            }
            return tabPage.State.HasFlag(ControlStates.Selected);
        }
    }
}