using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

using BLL;

namespace UI
{
    public partial class InformeMontoRecaudado : Form
    {
        private InformeMontoRecaudadoBLL _informeMontoRecaudadoBLL;

        public InformeMontoRecaudado()
        {
            InitializeComponent();

            this._informeMontoRecaudadoBLL = new InformeMontoRecaudadoBLL();

            CargarChart();
        }

        private void CargarChart()
        {

            var (dispositivos, montos) = _informeMontoRecaudadoBLL.ObtenerMontosRecaudados();
            chart1.Series[0].Points.DataBindXY(dispositivos, montos);
            chart1.Series[0].ChartType = SeriesChartType.Column;
            chart1.ChartAreas[0].Area3DStyle.Enable3D = true;
        }
    }
}
