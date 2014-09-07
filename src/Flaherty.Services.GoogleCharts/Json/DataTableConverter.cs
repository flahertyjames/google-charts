// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataTableConverter.cs" company="James Flaherty">
//   2014
// </copyright>
// <summary>
//   Converts a data table to JSON.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Flaherty.Services.GoogleCharts.Json
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    using Newtonsoft.Json;

    /// <summary>
    /// Converts a data table to JSON.
    /// </summary>
    public class DataTableConverter : JsonConverter
    {
        /// <summary>
        /// Writes a data table to JSON.
        /// </summary>
        /// <param name="writer">
        /// The writer.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="serializer">
        /// The serializer.
        /// </param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var dataTable = value as DataTable;
            if (dataTable != null)
            {
                var dataMatrix = new List<IEnumerable<object>>();
                dataMatrix.Add(dataTable.Columns.Cast<DataColumn>().Select(x => x.ColumnName));
                var data = dataTable.Rows.Cast<DataRow>().Select(x => x.ItemArray);
                dataMatrix.AddRange(data);
                serializer.Serialize(writer, dataMatrix);
            }
        }

        /// <summary>
        /// Reads a data table from JSON.
        /// </summary>
        /// <param name="reader">
        /// The reader.
        /// </param>
        /// <param name="objectType">
        /// The object type.
        /// </param>
        /// <param name="existingValue">
        /// The existing value.
        /// </param>
        /// <param name="serializer">
        /// The serializer.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// This method is not implemented
        /// </exception>
        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Determines whether the supplied object can be converted.
        /// </summary>
        /// <param name="objectType">
        /// The object type.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DataTable);
        }
    }
}