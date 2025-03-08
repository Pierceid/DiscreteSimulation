using DiscreteSimulation.Strategies;

namespace DiscreteSimulation.Simulations {
    public class Warehouse(int replicationStock) : SimulationCore(replicationStock) {
        public Strategy? Strategy { get; set; } = null;
        private Action<int, double>? callback;

        public void SetReplicationCallback(Action<int, double> callback) {
            this.callback = callback;
        }

        public override void AfterSimulation() {

        }

        public override void AfterSimulationRun() {

        }

        public override void BeforeSimulation() {

        }

        public override void BeforeSimulationRun() {

        }

        public override void Experiment() {
            if (Strategy != null) {
                Strategy.RunStrategy();

                if (currentReplication < replicationStock * 0.01) return;

                if (currentReplication % 1000 == 0) {
                    double averageCost = Strategy.TotalCost / (currentReplication + 1);
                    callback?.Invoke(currentReplication, averageCost);
                }
            }
        }
    }
}