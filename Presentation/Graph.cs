using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot;
using OxyPlot.Wpf;

namespace DiscreteSimulation.Presentation {
    public class Graph {
        private PlotModel model;
        private LineSeries series;
        private LinearAxis xAxis;
        private LinearAxis yAxis;
        private PlotView plotView;

        public Graph(string modelTitle, string xAxisTitle, string yAxisTitle, string seriesTitle, PlotView plotView) {
            model = new PlotModel { Title = modelTitle };

            xAxis = new LinearAxis { Position = AxisPosition.Bottom, Title = xAxisTitle, Minimum = 0, Maximum = 1000 };
            model.Axes.Add(xAxis);

            yAxis = new LinearAxis { Position = AxisPosition.Left, Title = yAxisTitle, Minimum = 0, Maximum = 1000 };
            model.Axes.Add(yAxis);

            series = new LineSeries { Title = seriesTitle, MarkerType = MarkerType.None };
            model.Series.Add(series);

            this.plotView = plotView;
            this.plotView.Model = model;
        }

        public void UpdatePlot(double xValue, double yValue) {
            series.Points.Add(new DataPoint(xValue, yValue));
            xAxis.Maximum = xValue;
            yAxis.Minimum = yValue * 0.995;
            yAxis.Maximum = yValue * 1.005;
            model.InvalidatePlot(true);
        }

        public void RefreshGraph() {
            series.Points.Clear();
            xAxis.Minimum = 0;
            xAxis.Maximum = 1000;
            yAxis.Minimum = 0;
            yAxis.Maximum = 1000;
            model.InvalidatePlot(true);
        }

        public void InvalidatePlot() {
            model.InvalidatePlot(true);
        }
    }
}