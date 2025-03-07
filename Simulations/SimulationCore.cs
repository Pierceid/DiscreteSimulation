namespace DiscreteSimulation.Simulations {
    public abstract class SimulationCore {
        protected Thread? thread;
        protected int replicationStock;
        protected int currentReplication;

        protected SimulationCore(int replicationStock) {
            this.thread = null;
            this.replicationStock = replicationStock;
            this.currentReplication = 0;
        }

        public void RunSimulation() {
            this.thread = new(Simulate);
            this.thread.Start();
        }

        private void Simulate() {
            BeforeSimulationRun();

            for (this.currentReplication = 0; this.currentReplication < this.replicationStock; this.currentReplication++) {
                BeforeSimulation();
                Experiment();
                AfterSimulation();
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
