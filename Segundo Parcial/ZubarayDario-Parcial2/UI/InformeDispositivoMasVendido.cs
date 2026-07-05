using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

using BE;
using BLL;

namespace UI
{
    public partial class InformeDispositivoMasVendido : Form
    {
        private InformeDispositivoMasVendidoBLL _InformeDispositivoMasVendidoBLL;

        public InformeDispositivoMasVendido()
        {
            InitializeComponent();

            this._InformeDispositivoMasVendidoBLL = new InformeDispositivoMasVendidoBLL();
            CargarChart();
        }

        private void CargarChart()
        {
            var (dispositivos, valores) = _InformeDispositivoMasVendidoBLL.ObtenerDispositivosMasVendidos();
            chart1.Series[0].Points.DataBindXY(dispositivos, valores);
            chart1.Series[0].ChartType = SeriesChartType.Column;
            chart1.ChartAreas[0].Area3DStyle.Enable3D = true;
        }
    }
}
