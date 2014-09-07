// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PieChartOptions.cs" company="James Flaherty">
//   2014
// </copyright>
// <summary>
//   Options object for pie charts.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Flaherty.Services.GoogleCharts
{
    using System.Collections.Generic;
    using System.Drawing;

    using Newtonsoft.Json;

    /// <summary>
    /// Options object for pie charts.
    /// </summary>
    public class PieChartOptions : ChartOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PieChartOptions"/> class.
        /// </summary>
        public PieChartOptions()
        {
            this.Slices = new Dictionary<int, PieChartSliceOptions>();
        }

        /// <summary>
        /// Gets or sets whether the chart should render in 3D. A null value is equivalent to false.
        /// </summary>
        [JsonProperty("is3D", NullValueHandling = NullValueHandling.Ignore)]
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public bool? Is3D { get; set; }

        /// <summary>
        /// Gets or sets a number between 0 and 1, corresponding to the ratio of radii between the hole and the chart. 
        /// Numbers between 0.4 and 0.6 will look best on most charts. Values equal to or greater than 1 will be ignored, 
        /// and a value of 0 will completely shut your pie hole.
        /// </summary>
        [JsonProperty("pieHole", NullValueHandling = NullValueHandling.Ignore)]
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public double? PieHole { get; set; }

        /// <summary>
        /// Gets or sets a value with which to rotate the pie chart clockwise.
        /// </summary>
        [JsonProperty("pieStartAngle", NullValueHandling = NullValueHandling.Ignore)]
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public double? PieStartAngle { get; set; }

        /// <summary>
        /// Gets the collection of slice options.
        /// </summary>
        [JsonProperty("slices")]
        public Dictionary<int, PieChartSliceOptions> Slices { get; private set; }

        /// <summary>
        /// Applies a supplied palette to the options object.
        /// </summary>
        /// <param name="palette">
        /// The palette.
        /// </param>
        public override void ApplyPalette(IPalette palette)
        {
            var i = 0;
            foreach (Color color in palette.Colors)
            {
                if (!this.Slices.ContainsKey(i))
                {
                    this.Slices.Add(i, new PieChartSliceOptions());
                }

                var slice = this.Slices[i];
                if (!slice.Color.HasValue)
                {
                    slice.Color = color;
                }

                i++;
            }
        }
    }
}
