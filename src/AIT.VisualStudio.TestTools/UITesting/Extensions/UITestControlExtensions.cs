namespace AIT.VisualStudio.TestTools.UITesting.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows.Automation;

    using AIT.VisualStudio.TestTools.UITesting.Attributes;

    using Microsoft.VisualStudio.TestTools.UITest.Extension;
    using Microsoft.VisualStudio.TestTools.UITesting;
    using Microsoft.VisualStudio.TestTools.UITesting.WpfControls;

    using ControlType = Microsoft.VisualStudio.TestTools.UITesting.ControlType;

    /// <summary>
    /// Contains extension methods for the <see cref="UITestControl"/> class.
    /// </summary>
    public static class UITestControlExtensions
    {
        /// <summary>
        /// Gets the specified control.
        /// </summary>
        /// <param name="container">The container.</param>
        public static T Get<T>(this UITestControl container) where T : UITestControl, new()
        {
            var automationIdAttribute = typeof(T).GetCustomAttributes(typeof(AutomationIdAttribute), true).OfType<AutomationIdAttribute>().FirstOrDefault();

            if (automationIdAttribute == null)
            {
                throw new ArgumentOutOfRangeException("AutomationIdAttribute not set on type " + typeof(T));
            }

            return Get<T>(container, automationIdAttribute.Id);
        }

        /// <summary>
        /// Gets the specified control.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="automationId">The automation identifier.</param>
        public static T Get<T>(this UITestControl container, string automationId) where T : UITestControl, new()
        {
            var propertyExpressionCollection = new PropertyExpressionCollection { { WpfControl.PropertyNames.AutomationId, automationId }, };

            return Get<T>(container, propertyExpressionCollection);
        }

        /// <summary>
        /// Gets the specified control.
        /// </summary>
        public static T Get<T>(this UITestControl container, PropertyExpressionCollection searchProperties) where T : UITestControl, new()
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            var testControl = new T { Container = container };

            testControl.SearchProperties.AddRange(searchProperties);

            return testControl;
        }

        /// <summary>
        /// Checks whether the specified control exists.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">control</exception>
        public static bool Exists(this UITestControl control)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }

            try
            {
                return control.FindMatchingControls().Any();
            }
            catch (UITestControlNotAvailableException)
            {
                return false;
            }
            catch (ElementNotAvailableException)
            {
                return false;
            }
        }

        /// <summary>
        /// Clicks the specified control.
        /// </summary>
        public static void Click(this UITestControl control)
        {
            if (control == null) throw new ArgumentNullException("control");
            Mouse.Click(control);

            var name = control.Name;
            var wpfControl = control as WpfControl;

            if (wpfControl != null)
            {
                name = wpfControl.AutomationId;
            }

            Trace.WriteLine(control.GetType().Name + " " + name + " clicked.");
        }

        /// <summary>
        /// Determines whether the specified control is visible.
        /// </summary>
        private static bool IsVisible(this UITestControl testControl)
        {
            if (testControl == null)
            {
                throw new ArgumentNullException("testControl");
            }

            var isInvisible = (testControl.State & ControlStates.Invisible) == ControlStates.Invisible;

            if (isInvisible)
            {
                return false;
            }

            var isCollapsed = (testControl.State & ControlStates.Collapsed) == ControlStates.Collapsed;

            if (isCollapsed)
            {
                return false;
            }

            var isOffScreen = (testControl.State & ControlStates.Offscreen) == ControlStates.Offscreen;

            if (isOffScreen)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Clicks the on first visible child.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <exception cref="ArgumentNullException">control</exception>
        public static void ClickOnFirstVisibleChild(this UITestControl control)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }

            var firstVisibleControl = control.GetChildren(true).First(c => (c.ControlType == ControlType.Text) && c.IsVisible());

            firstVisibleControl.Click();
        }

        /// <summary>
        /// Gets the children including their children.
        /// </summary>
        private static IEnumerable<UITestControl> GetChildren(this UITestControl control, bool includingChildren)
        {
            foreach (var child in control.GetChildren())
            {
                yield return child;

                if (includingChildren)
                {
                    foreach (var uiTestControls in child.GetChildren(true))
                    {
                        yield return uiTestControls;
                    }
                }
            }
        }
    }
}