using MonteCarlo.Presentation;
using System.Windows;

namespace MonteCarlo.Windows {
    public partial class BarChartWindow : Window {
        public BarChartWindow(Window? window, string title, double[] costs) {
            InitializeComponent();

            Owner = window;

            _ = new BarChart(plotView, title, costs);
        }
    }
}
