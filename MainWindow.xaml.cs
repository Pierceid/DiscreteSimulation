using DiscreteSimulation.Simulations;
using DiscreteSimulation.Strategies;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Wpf;
using OxyPlot;
using System.Windows.Controls;
using System.Windows;

namespace DiscreteSimulation {
    public partial class MainWindow : Window {
        private Warehouse warehouse;
        private PlotModel model;
        private LineSeries series;
        private LinearAxis xAxis;
        private LinearAxis yAxis;
        private bool isRunning;
        private Thread? simulationThread;

        private Strategy[] strategies = [
            new StrategyA(),
            new StrategyB(),
            new StrategyC(),
            new StrategyD(),
            new StrategyX()
        ];

        public MainWindow() {
            InitializeComponent();

            warehouse = new Warehouse(1_000_000);
            warehouse.SetReplicationCallback(OnReplicationCompleted);

            model = new PlotModel { Title = "Total Cost Over Time" };

            xAxis = new LinearAxis { Position = AxisPosition.Bottom, Title = "Replications", Minimum = 0, Maximum = 1000 };
            model.Axes.Add(xAxis);

            yAxis = new LinearAxis { Position = AxisPosition.Left, Title = "Average Cost", Minimum = 0, Maximum = 1000 };
            model.Axes.Add(yAxis);

            series = new LineSeries { Title = "Average Cost", MarkerType = MarkerType.None };
            model.Series.Add(series);

            plotView.Model = model;

            isRunning = false;

            cbStrategies.SelectedIndex = 0;
        }

        private void OnReplicationCompleted(int replication, double cost) {
            Dispatcher.Invoke(() => UpdatePlot(replication, cost));
        }

        private void UpdatePlot(int replication, double cost) {
            series.Points.Add(new DataPoint(replication, cost));

            xAxis.Maximum = Math.Max(replication, xAxis.Maximum);

            yAxis.Minimum = cost * 0.99;
            yAxis.Maximum = cost * 1.01;

            model.InvalidatePlot(true);
        }

        private void StartSimulation() {
            if (isRunning || warehouse.Strategy == null) return;

            isRunning = true;

            RefreshGraph();

            simulationThread = new Thread(() => warehouse.RunSimulation());

            simulationThread.Start();
        }

        private void StopSimulation() {
            isRunning = false;
            warehouse.Stop();
            warehouse = new Warehouse(1_000_000);
            warehouse.SetReplicationCallback(OnReplicationCompleted);
            simulationThread?.Join();
            simulationThread = null;
        }

        private void ButtonClick(object sender, RoutedEventArgs e) {
            if (sender is Button button) {
                if (button == btnStart) {
                    StartSimulation();
                } else if (button == btnStop) {
                    StopSimulation();
                }
            }
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (sender is ComboBox comboBox && comboBox == cbStrategies) {
                if (isRunning) {
                    StopSimulation();
                }

                warehouse.Strategy = strategies[comboBox.SelectedIndex];

                RefreshGraph();
                UpdateUI();
            }
        }

        private void RefreshGraph() {
            series.Points.Clear();
            xAxis.Minimum = 0;
            xAxis.Maximum = 1000;
            yAxis.Minimum = 0;
            yAxis.Maximum = 1000;
            model.InvalidatePlot(true);
        }

        private void UpdateUI() {
            if (warehouse.Strategy is StrategyX) {
                groupBoxStrategyXControls.Visibility = Visibility.Visible;
            } else {
                groupBoxStrategyXControls.Visibility = Visibility.Collapsed;
            }
        }
    }
}