namespace AIT.VisualStudio.TestTools.UITesting.Extensions
{
    using System;

    using Microsoft.VisualStudio.TestTools.UITest.Extension;
    using Microsoft.VisualStudio.TestTools.UITesting;
    using Microsoft.VisualStudio.TestTools.UITesting.WpfControls;

    /// <summary>
    /// Test extensions for <see cref=" WpfTabPage" />s.
    /// </summary>
    public static class WpfTabPageExtensions
    {
        /// <summary>
        /// Determines whether the specified control is selected.
        /// </summary>
        /// <param name="tabPage">The tab page.</param>
        public static bool IsSelected(this UITestControl tabPage)
        {
            if (tabPage == null)
            {
                throw new ArgumentNullException("tabPage");
            }
            return (tabPage.State & ControlStates.Selected) == ControlStates.Selected;
        }
    }
}