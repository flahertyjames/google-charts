// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Palette.cs" company="James Flaherty">
//   2014
// </copyright>
// <summary>
//   Object to describe a collection of colors for use in rendering a chart.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Flaherty.Services.GoogleCharts
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    
    /// <summary>
    /// Object to describe a collection of colors for use in rendering a chart.
    /// </summary>
    public class Palette : List<Color>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Palette"/> class.
        /// </summary>
        /// <param name="colors">
        /// The colors.
        /// </param>
        public Palette(params Color[] colors)
        {
            this.AddRange(colors);
        }

        /// <summary>
        /// Parses a string of colors and returns a palette.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="Palette"/>.
        /// </returns>
        public static Palette Parse(string value)
        {
            var colors = value.Split(new[] { ',' }).Select(ColorTranslator.FromHtml).ToArray();
            return new Palette(colors);
        }

        /// <summary>
        /// Converts the palette to a string.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public override string ToString()
        {
            return string.Join(",", this.Select(ColorTranslator.ToHtml));
        }
    }
}
