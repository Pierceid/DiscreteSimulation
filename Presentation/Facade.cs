using DiscreteSimulation.Simulations;
using DiscreteSimulation.Strategies;
using OxyPlot.Wpf;

namespace DiscreteSimulation.Presentation {
    public class Facade {
        private Warehouse warehouse;
        private Graph graph;
        private Thread? simulationThread;
        private bool isRunning;
        private const int REPLICATION_STOCK = 1_000_000;

        public Facade(PlotView plotView) {
            warehouse = new(REPLICATION_STOCK) { Callback = OnReplicationCompleted };

            graph = new(
                modelTitle: "Total Cost Over Time",
                xAxisTitle: "Replications",
                yAxisTitle: "Average Cost",
                seriesTitle: "Average Cost",
                plotView: plotView
            );

            isRunning = false;
        }

        public void StartSimulation() {
            if (warehouse.Strategy == null || isRunning) return;

            isRunning = true;
            graph.RefreshGraph();

            simulationThread = new(warehouse.RunSimulation);
            simulationThread.Start();
        }

        public void StopSimulation() {
            if (isRunning) {
                warehouse.Stop();
                isRunning = false;

                simulationThread?.Join();
                simulationThread = null;
            }
        }

        public void SetStrategy(Strategy strategy) {
            if (isRunning) {
                StopSimulation();
            }

            warehouse.Strategy = strategy;
            graph.RefreshGraph();
        }

        private void OnReplicationCompleted(int replication, double cost) {
            graph.UpdatePlot(replication, cost);
        }
    }
}