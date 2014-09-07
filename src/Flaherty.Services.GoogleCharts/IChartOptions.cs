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
        /// Applies a palette to the options.
        /// </summary>
        /// <param name="palette">
        /// The palette.
        /// </param>
        void ApplyPalette(IPalette palette);
    }
}
