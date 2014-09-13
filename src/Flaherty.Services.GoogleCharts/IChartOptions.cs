// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IChartOptions.cs" company="James Flaherty">
//   2014
// </copyright>
// <summary>
//   Interface to describe a set of options for a chart.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Flaherty.Services.GoogleCharts
{
    /// <summary>
    /// Interface to describe a set of options for a chart.
    /// </summary>
    public interface IChartOptions
    {
        /// <summary>
        /// Gets the default colors for the chart.
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        Palette Colors { get; }

        /// <summary>
        /// Gets the chart title.
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        string Title { get; }
    }
}
