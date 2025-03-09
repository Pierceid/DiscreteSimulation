using DiscreteSimulation.Presentation;
using System.Windows;

namespace DiscreteSimulation.Windows {
    public partial class BarChartWindow : Window {
        private BarChart barChart;

        public BarChartWindow(Window mainWindow, double[] costs) {
            InitializeComponent();
            Owner = mainWindow;

            barChart = new BarChart("Cost Analysis", plotView);
            barChart.UpdateChart(costs);
        }
    }
}
