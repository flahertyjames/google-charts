// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataTableConverterTest.cs" company="James Flaherty">
//   2014
// </copyright>
// <summary>
//   Test to check <see cref="GoogleCharts.Json.DataTableConverter" /> logic.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Flaherty.Services.GoogleCharts.Tests.Json
{
    using System.Data;

    using Newtonsoft.Json;

    using NUnit.Framework;

    /// <summary>
    /// Test to check <see cref="GoogleCharts.Json.DataTableConverter"/> logic.
    /// </summary>
    [TestFixture]
    public class DataTableConverterTest
    {
        /// <summary>
        /// Test to confirm that serialization of a data table generates appropriate JSON data.
        /// </summary>
        [Test]
        // ReSharper disable once InconsistentNaming
        public void DataTable_Serialize_GeneratesJsonData()
        {
            const string Expected = "[[\"Id\",\"FirstName\",\"LastName\"],[1,\"John\",\"Smith\"],[2,\"Jane\",\"Doe\"]]";
            var table = new DataTable();
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("FirstName");
            table.Columns.Add("LastName");
            table.Rows.Add(1, "John", "Smith");
            table.Rows.Add(2, "Jane", "Doe");
            var json = JsonConvert.SerializeObject(table, new GoogleCharts.Json.DataTableConverter());
            Assert.AreEqual(Expected, json);
        }
    }
}
