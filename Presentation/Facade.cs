using DiscreteSimulation.Simulations;
using DiscreteSimulation.Strategies;
using DiscreteSimulation.Windows;
using OxyPlot.Wpf;
using System.Windows;

namespace DiscreteSimulation.Presentation {
    public class Facade {
        private Window? mainWindow;
        private Warehouse? warehouse;
        private LineGraph? graph;
        private Thread? simulationThread;
        private bool isRunning;

        public Facade(Window? window) {
            mainWindow = window;
            warehouse = null;
            graph = null;
            simulationThread = null;
            isRunning = false;
        }

        public void StartSimulation() {
            if (warehouse == null || warehouse.Strategy == null || graph == null || isRunning) return;

            isRunning = true;
            graph.RefreshGraph();

            simulationThread = new(warehouse.RunSimulation);
            simulationThread.Start();
        }

        public void StopSimulation() {
            if (warehouse == null) return;

            if (isRunning) {
                warehouse.Stop();
                isRunning = false;

                simulationThread?.Join();
                simulationThread = null;
            }
        }

        public void PrintReplication() {
            if (warehouse == null || warehouse.Strategy == null) return;

            BarChartWindow barChartWindow = new(mainWindow, $"Costs Analysis ({warehouse.CurrentReplication})", warehouse.Strategy.DailyCosts);
            barChartWindow.Show();
        }


        public void SetStrategy(Strategy strategy) {
            if (warehouse == null || graph == null) return;

            if (isRunning) {
                StopSimulation();
            }

            warehouse.Strategy = strategy;
            graph.RefreshGraph();
        }

        public void InitGraph(PlotView plotView) {
            graph = new(
                modelTitle: "Costs Over Time",
                xAxisTitle: "Replications",
                yAxisTitle: "Costs",
                seriesTitle: "Costs",
                plotView: plotView
            );
        }

        public void InitWarehouse(int replications) {
            warehouse = new(replications) { Callback = OnReplicationCompleted };
        }

        private void OnReplicationCompleted(int replication, double cost) {
            if (graph == null) return;

            graph.UpdatePlot(replication, cost);
        }
    }
}