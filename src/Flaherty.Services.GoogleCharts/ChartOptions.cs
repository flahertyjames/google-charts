// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChartOptions.cs" company="James Flaherty">
//   2014
// </copyright>
// <summary>
//   Abstract options object for charts.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Flaherty.Services.GoogleCharts
{
    using Newtonsoft.Json;

    /// <summary>
    /// Abstract options object for charts.
    /// </summary>
    public abstract class ChartOptions : IChartOptions
    {
        /// <summary>
        /// Gets or sets the chart title.
        /// </summary>
        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public string Title { get; set; }

        /// <summary>
        /// Applies a supplied palette to the options object.
        /// </summary>
        /// <param name="palette">
        /// The palette.
        /// </param>
        public abstract void ApplyPalette(IPalette palette);
    }
}
