using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Wpf;

namespace DiscreteSimulation.Presentation {
    public class BarChart {
        private PlotModel model;
        private BarSeries barSeries;
        private PlotView plotView;

        public BarChart(string title, PlotView plotView) {
            this.plotView = plotView;
            model = new PlotModel { Title = title };

            var categoryAxis = new CategoryAxis { Position = AxisPosition.Bottom, Title = "Day" };
            model.Axes.Add(categoryAxis);

            var valueAxis = new LinearAxis { Position = AxisPosition.Left, Title = "Cost", Minimum = 0 };
            model.Axes.Add(valueAxis);

            barSeries = new BarSeries { Title = "Daily Costs" };
            model.Series.Add(barSeries);

            this.plotView.Model = model;
        }

        public void UpdateChart(double[] costs) {
            barSeries.Items.Clear();
            for (int i = 0; i < costs.Length; i++) {
                barSeries.Items.Add(new BarItem(costs[i]));
                ((CategoryAxis)model.Axes[0]).Labels.Add($"D{i + 1}");
            }
            model.InvalidatePlot(true);
        }
    }
}
