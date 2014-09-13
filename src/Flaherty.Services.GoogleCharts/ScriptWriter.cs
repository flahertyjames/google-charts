// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScriptWriter.cs" company="James Flaherty">
//   2014
// </copyright>
// <summary>
//   The script writer.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Flaherty.Services.GoogleCharts
{
    using System.Text;

    using Newtonsoft.Json;

    /// <summary>
    /// The script writer.
    /// </summary>
    public class ScriptWriter
    {
        private readonly IChart chart;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptWriter"/> class.
        /// </summary>
        /// <param name="chart">
        /// The chart.
        /// </param>
        public ScriptWriter(IChart chart)
        {
            this.chart = chart;
        }

        /// <summary>
        /// Writes the JavaScript for the chart.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string WriteScript()
        {
            var script = new StringBuilder();
            script.AppendLine("<script type=\"text/javascript\">");
            script.AppendLine("//<![CDATA[");
            script.AppendLine("\tgoogle.load(\"visualization\", \"1\", {packages:[\"corechart\"]});");
            script.AppendLine(string.Format("\tgoogle.setOnLoadCallback(DrawChart_{0});", this.chart.Target));
            script.AppendLine(string.Format("\tfunction DrawChart_{0}() {{", this.chart.Target));

            // serialize data
            var data = JsonConvert.SerializeObject(this.chart.DataSource, new Json.DataTableConverter());
            script.AppendLine(string.Format("\t\tvar data = google.visualization.arrayToDataTable({0});", data));

            // serialize options
            var options = JsonConvert.SerializeObject(this.chart.GetOptions());
            script.AppendLine(string.Format("\t\tvar options = {0};", options));

            script.AppendLine(
                string.Format(
                    "\t\tvar chart = new google.visualization.{0}(document.getElementById('{1}'));",
                    this.chart.Type,
                    this.chart.Target));
            script.AppendLine("\t\tchart.draw(data, options);");

            script.AppendLine("\t}");
            script.AppendLine("//]]>");
            script.AppendLine("</script>");
            return script.ToString();
        }
    }
}
