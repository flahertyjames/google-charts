// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Default.aspx.cs" company="James Flaherty">
//   2014
// </copyright>
// <summary>
//   The default page.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Flaherty.Services.GoogleCharts.WebControls.SampleApp
{
    using System;
    using System.Data;

    /// <summary>
    /// The default page.
    /// </summary>
    public partial class Default : System.Web.UI.Page
    {
        /// <summary>
        /// Handler that fires on page load.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The event args.
        /// </param>
        protected void Page_Load(object sender, EventArgs e)
        {
            var table = new DataTable();
            table.Columns.Add("Task");
            table.Columns.Add("Hours per Day", typeof(int));
            table.Rows.Add("Work", 11);
            table.Rows.Add("Eat", 2);
            table.Rows.Add("Commute", 2);
            table.Rows.Add("Watch TV", 2);
            table.Rows.Add("Sleep", 7);
            this.PieChart1.DataSource =
                this.PieChart2.DataSource =
                this.PieChart3.DataSource =
                this.PieChart4.DataSource =
                this.PieChart5.DataSource =
                this.PieChart6.DataSource = table;
        }
    }
}