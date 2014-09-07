// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PaletteConverter.cs" company="James Flaherty">
//   2014
// </copyright>
// <summary>
//   Allows conversion between a string of comma delimited color strings and a Palette.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Flaherty.Services.GoogleCharts.Converters
{
    using System;
    using System.ComponentModel;
    using System.Globalization;

    /// <summary>
    /// Allows conversion between a string of comma delimited color strings and a Palette.
    /// </summary>
    public class PaletteConverter : TypeConverter
    {
        /// <summary>
        /// Indicates whether we can convert from the supplied type.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <param name="sourceType">
        /// The source type.
        /// </param>
        /// <returns>
        /// A <see cref="bool"/> that indicates whether we can convert from the supplied type.
        /// </returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        /// <summary>
        /// The convert from.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <param name="culture">
        /// The culture.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                return Palette.Parse((string)value);
            }

            return base.ConvertFrom(context, culture, value);
        }
    }
}