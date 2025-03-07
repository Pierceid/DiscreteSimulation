using DiscreteSimulation.Simulations;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.Windows;
using System.Windows.Controls;

namespace DiscreteSimulation {
    public partial class MainWindow : Window {
        private BuffonNeedle buffon;
        private PlotModel model;
        private LineSeries series;
        private LinearAxis xAxis;
        private LinearAxis yAxis;
        private bool isRunning;
        private int updateInterval = 1000; // Update the plot every 100 replications

        public MainWindow() {
            InitializeComponent();

            buffon = new BuffonNeedle(100_000); // Large replication count

            // Initialize OxyPlot model
            model = new PlotModel { Title = "Buffon's Needle Experiment" };

            // Dynamic X-Axis (Replications)
            xAxis = new LinearAxis { Position = AxisPosition.Bottom, Title = "Replications", Minimum = 0, Maximum = 1000 };
            model.Axes.Add(xAxis);

            // Dynamic Y-Axis (Pi estimate)
            yAxis = new LinearAxis { Position = AxisPosition.Left, Title = "Estimated π", Minimum = 2.5, Maximum = 3.5 };
            model.Axes.Add(yAxis);

            // Line series for the plot
            series = new LineSeries { Title = "Estimated π", MarkerType = MarkerType.None };
            model.Series.Add(series);

            plotView.Model = model;
            isRunning = false;
        }

        private void StartSimulation() {
            if (isRunning) return;

            isRunning = true;

            // Start the simulation with a callback to update the plot
            //buffon.RunSimulation((replication, estimatedPi) => {
            //    Dispatcher.Invoke(() => {
            //        UpdatePlot(replication, estimatedPi);
            //    });
            //});
        }

        private void StopSimulation() {
            // Ideally, you would cancel the simulation here
            isRunning = false;
        }

        private void UpdatePlot(long replications, double estimatedPi) {
            // Only update the plot after every `updateInterval` replications
            if (replications % updateInterval != 0) return;

            // Add a new point to the series for the current replication
            series.Points.Add(new DataPoint(replications, estimatedPi));

            // Dynamically adjust axes based on new data points
            xAxis.Maximum = Math.Max(replications, xAxis.Maximum);
            yAxis.Minimum = series.Points.Min(p => p.Y) - 0.1;
            yAxis.Maximum = series.Points.Max(p => p.Y) + 0.1;

            // Refresh the plot view
            model.InvalidatePlot(true);
        }

        private void ButtonClick(object sender, RoutedEventArgs e) {
            if (sender is Button button) {
                if (button == btn1 && !isRunning) {  // Start simulation
                    StartSimulation();
                } else if (button == btn2) { // Stop simulation
                    StopSimulation();
                }
            }
        }
    }
}
