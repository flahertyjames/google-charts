// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PieChartSlice.cs" company="James Flaherty">
//   2014
// </copyright>
// <summary>
//   Control to specify details for a pie chart slice.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Flaherty.Services.GoogleCharts.WebControls
{
    using System.Drawing;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Control to specify details for a pie chart slice.
    /// </summary>
    [ParseChildren(true)]
    public class PieChartSlice : WebControl
    {
        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        public Color? Color { get; set; }

        /// <summary>
        /// Gets or sets the offset.
        /// </summary>
        public double? Offset { get; set; }

        /// <summary>
        /// Converts the slice to a <see cref="PieChartSliceOptions"/> object.
        /// </summary>
        /// <returns>
        /// The <see cref="PieChartSliceOptions"/>.
        /// </returns>
        public PieChartSliceOptions ToOptions()
        {
            return new PieChartSliceOptions { Color = this.Color, Offset = this.Offset };
        }
    }
}
