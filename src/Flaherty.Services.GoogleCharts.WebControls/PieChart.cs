// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PieChart.cs" company="James Flaherty">
//   2014
// </copyright>
// <summary>
//   Control for rendering a Google Charts pie chart in an ASP.net web application.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Flaherty.Services.GoogleCharts.WebControls
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using Flaherty.Services.GoogleCharts;

    /// <summary>
    /// Control for rendering a Google Charts pie chart in an ASP.net web application.
    /// </summary>
    [ParseChildren(true)]
    public sealed class PieChart : Panel, IChart
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PieChart"/> class.
        /// </summary>
        public PieChart()
        {
            this.Type = ChartType.PieChart;
            this.Options = new PieChartOptions();
            this.Init += this.Chart_Init;
        }

        /// <summary>
        /// Gets the target.
        /// </summary>
        public string Target
        {
            get
            {
                return this.ClientID;
            }
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        public ChartType Type { get; private set; }

        /// <summary>
        /// Gets or sets the data source.
        /// </summary>
        public DataTable DataSource { get; set; }

        /// <summary>
        /// Gets or sets the options.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PieChartOptions Options { get; set; }

        /// <summary>
        /// Adds slice options to the pie chart.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:PropertySummaryDocumentationMustMatchAccessors", Justification = "Reviewed. Suppression is OK here.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public PieChartSlice Slice
        {
            set
            {
                var slice = value.ToOptions();
                if (this.Options.Slices.ContainsKey(value.Index))
                {
                    this.Options.Slices[value.Index] = slice;
                }
                else
                {
                    this.Options.Slices.Add(value.Index, slice);
                }
            }
        }

        /// <summary>
        /// Gets the options as an interface.
        /// </summary>
        /// <returns>
        /// The <see cref="IChartOptions"/>.
        /// </returns>
        public IChartOptions GetOptions()
        {
            return this.Options;
        }

        private void RegisterJavascript()
        {
            this.Page.ClientScript.RegisterClientScriptInclude(this.GetType().Name, "https://www.google.com/jsapi");
            var writer = new ScriptWriter(this);
            Page.ClientScript.RegisterClientScriptBlock(
                this.GetType(),
                string.Format("{0}.{1}", this.GetType().FullName, this.ClientID),
                writer.WriteScript());
        }

        // ReSharper disable once InconsistentNaming
        private void Chart_Init(object sender, EventArgs e)
        {
            this.Page.Load += this.Page_Load;
            this.Page.PreRender += this.Page_PreRender;
        }

        // ReSharper disable once InconsistentNaming
        private void Page_Load(object sender, EventArgs e)
        {
            this.RegisterJavascript();
        }

        // ReSharper disable once InconsistentNaming
        private void Page_PreRender(object sender, EventArgs e)
        {
        }
    }
}
