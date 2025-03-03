using System.Diagnostics;

namespace DiscreteSimulation.Simulations {
    public abstract class SimulationCore {
        protected Thread? thread;
        protected long replicationsCount;
        protected long currentReplication;

        protected SimulationCore(long replicationsCount) {
            this.thread = null;
            this.replicationsCount = replicationsCount;
            this.currentReplication = 0;
        }

        public void RunSimulation() {
            this.thread = new(Simulate);
            this.thread.Start();
        }

        private void Simulate() {
            BeforeSimulationRun();

            Stopwatch stopwatch = Stopwatch.StartNew();

            for (int current = 0; current < this.replicationsCount; current++) {
                BeforeSimulation();
                Experiment();
                AfterSimulation();

                if (current % 1000 == 0) {
                    Console.WriteLine($"Replication {current}. Finished in {stopwatch.ElapsedMilliseconds} ms");
                    stopwatch.Restart();
                }
            }

            AfterSimulationRun();
        }

        public abstract void Experiment();
        public abstract void BeforeSimulation();
        public abstract void AfterSimulation();
        public abstract void BeforeSimulationRun();
        public abstract void AfterSimulationRun();
    }
}
