using DiscreteSimulation.Randoms.Strategies;
using DiscreteSimulation.Strategies;

namespace DiscreteSimulation.Simulations {
    class Warehouse(int replicationStock) : SimulationCore(replicationStock) {
        private Strategy strategy = new StrategyA();

        public override void AfterSimulation() {

        }

        public override void AfterSimulationRun() {
            Console.WriteLine("\n" + Math.Round(strategy.TotalCost / replicationStock, 2));
        }

        public override void BeforeSimulation() {

        }

        public override void BeforeSimulationRun() {

        }

        public override void Experiment() {
            strategy.RunStrategy();
            Console.WriteLine(Math.Round(strategy.TotalCost / currentReplication, 2));
        }
    }
}
