using System;
using System.Globalization;

using Microsoft.VisualStudio.TestTools.UITesting.WinControls;

namespace AIT.VisualStudio.TestTools.CodedUI.Extensions
{
    /// <summary>
    /// Contains <see cref="WinText"/> extensions methods.
    /// </summary>
    public static class WinTextExtensions
    {
        /// <summary>
        /// Converts the display text to an integer.
        /// </summary>
        public static int GetDisplayTextAsNumber(this WinText testControl)
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
        public static double GetDisplayTextAsDouble(this WinText testControl)
        {
            if (testControl == null)
            {
                throw new ArgumentNullException("testControl");
            }

            return double.Parse(testControl.DisplayText, CultureInfo.CurrentCulture);
        }
    }
}