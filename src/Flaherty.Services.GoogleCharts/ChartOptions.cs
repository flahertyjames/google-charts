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
    using System.ComponentModel;

    using Flaherty.Services.GoogleCharts.Converters;
    using Flaherty.Services.GoogleCharts.Json;

    using Newtonsoft.Json;

    /// <summary>
    /// Abstract options object for charts.
    /// </summary>
    public abstract class ChartOptions : IChartOptions
    {
        /// <summary>
        /// Gets or sets the default colors for the chart.
        /// </summary>
        [TypeConverter(typeof(PaletteConverter))]
        [JsonProperty("colors", ItemConverterType = typeof(ColorConverter), NullValueHandling = NullValueHandling.Ignore)]
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public Palette Colors { get; set; }

        /// <summary>
        /// Gets or sets the chart title.
        /// </summary>
        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public string Title { get; set; }
    }
}
