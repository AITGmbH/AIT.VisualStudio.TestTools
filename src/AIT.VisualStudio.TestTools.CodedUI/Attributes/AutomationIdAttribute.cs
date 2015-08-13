using System;
using System.Linq;

namespace AIT.VisualStudio.TestTools.CodedUI.Attributes
{
    /// <summary />
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class AutomationIdAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutomationIdAttribute" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public AutomationIdAttribute(string id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        public string Id
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the automation identifier.
        /// </summary>
        /// <param name="type">The type.</param>
        public static string GetAutomationId(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            var automationIdAttribute = type.GetCustomAttributes(typeof(AutomationIdAttribute), true).OfType<AutomationIdAttribute>().FirstOrDefault();

            if (automationIdAttribute != null)
            {
                return automationIdAttribute.Id;
            }

            throw new NotImplementedException("Automation id attribute missing on type " + type);
        }
    }
}