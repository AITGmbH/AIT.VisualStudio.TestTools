namespace AIT.VisualStudio.TestTools.Traits
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test Attribute to comment to link to an Area
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class AreaAttribute : TestCategoryBaseAttribute
    {
        private readonly IList<string> _area;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute" />
        /// class
        /// by applying the supplied category to the test.
        /// </summary>
        /// <param name="area">The area to be applied.</param>
        public AreaAttribute(string area)
        {
            var list = new List<string>();
            if (!string.IsNullOrWhiteSpace(area))
            {
                var areas = area.Split('\\');

                for (var i = 0; i < areas.Length; i++)
                {
                    var areaName = string.Empty;

                    for (var j = 0; j <= i; j++)
                    {
                        areaName += areas[j] + ".";
                    }

                    list.Add("Area." + areaName.TrimEnd('.'));
                }
            }

            this._area = list.OrderBy(l => l.Length).ToList();
        }

        /// <summary>
        /// Gets the area to be applied.
        /// </summary>
        public string Area
        {
            get
            {
                return this._area[0];
            }
        }

        /// <summary>
        /// Gets the test categories that have been applied to the test.
        /// </summary>
        public override IList<string> TestCategories
        {
            get
            {
                return this._area;
            }
        }
    }
}