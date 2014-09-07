// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPalette.cs" company="James Flaherty">
//   2014
// </copyright>
// <summary>
//   The palette interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Flaherty.Services.GoogleCharts
{
    using System.Collections.Generic;
    using System.Drawing;

    /// <summary>
    /// The palette interface.
    /// </summary>
    public interface IPalette
    {
        /// <summary>
        /// Gets the colors associated with the palette.
        /// </summary>
        IEnumerable<Color> Colors { get; }
    }
}
