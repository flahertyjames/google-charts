// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PieChartSliceOptions.cs" company="James Flaherty">
//   2014
// </copyright>
// <summary>
//   Options object for pie chart slices.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Flaherty.Services.GoogleCharts
{
    using System.Drawing;

    using Newtonsoft.Json;

    /// <summary>
    /// Options object for pie chart slices.
    /// </summary>
    public class PieChartSliceOptions
    {
        /// <summary>
        /// Gets or sets the slice color.
        /// </summary>
        [JsonProperty("color", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Json.ColorConverter))]
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public Color? Color { get; set; }

        /// <summary>
        /// Gets or sets the slice offset.
        /// </summary>
        [JsonProperty("offset", NullValueHandling = NullValueHandling.Ignore)]
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public double? Offset { get; set; }
    }
}
