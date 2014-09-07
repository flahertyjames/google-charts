// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IChart.cs" company="James Flaherty">
//   2014
// </copyright>
// <summary>
//   The chart interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Flaherty.Services.GoogleCharts
{
    using System.Data;

    /// <summary>
    /// The map interface.
    /// </summary>
    public interface IChart
    {
        /// <summary>
        /// Gets the target ID.
        /// </summary>
        string Target { get; }

        /// <summary>
        /// Gets the type.
        /// </summary>
        ChartType Type { get; }

        /// <summary>
        /// Gets the data source.
        /// </summary>
        DataTable DataSource { get; }

        /// <summary>
        /// Gets the palette.
        /// </summary>
        /// <returns>
        /// The <see cref="IPalette"/>.
        /// </returns>
        IPalette GetPalette();

        /// <summary>
        /// Gets the options.
        /// </summary>
        /// <returns>
        /// The <see cref="IChartOptions"/>.
        /// </returns>
        IChartOptions GetOptions();
    }
}
