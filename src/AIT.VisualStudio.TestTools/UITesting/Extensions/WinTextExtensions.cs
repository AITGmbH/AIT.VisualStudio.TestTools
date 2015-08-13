namespace AIT.VisualStudio.TestTools.UITesting.Extensions
{
    using System;
    using System.Globalization;

    using Microsoft.VisualStudio.TestTools.UITesting.WinControls;

    /// <summary>
    /// Contains <see cref="WinText"/> extensions methods.
    /// </summary>
    public static class WinTextExtensions
    {
        /// <summary>
        /// Converts the display text to int.
        /// </summary>
        public static int ToInt(this WinText testControl)
        {
            if (testControl == null)
            {
                throw new ArgumentNullException("testControl");
            }

            return int.Parse(testControl.DisplayText, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Converts the display text to double.
        /// </summary>
        public static double ToDouble(this WinText testControl)
        {
            if (testControl == null)
            {
                throw new ArgumentNullException("testControl");
            }

            return double.Parse(testControl.DisplayText, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Returns the display text.
        /// </summary>
        public static string ToString(this WinText testControl)
        {
            if (testControl == null)
            {
                throw new ArgumentNullException("testControl");
            }

            return testControl.DisplayText;
        }
    }
}