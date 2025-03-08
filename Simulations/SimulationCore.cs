namespace DiscreteSimulation.Simulations {
    public abstract class SimulationCore {
        protected Thread? thread;
        protected int replicationStock;
        protected int currentReplication;
        protected bool isRunning;

        protected SimulationCore(int replicationStock) {
            this.thread = null;
            this.replicationStock = replicationStock;
            this.currentReplication = 0;
            this.isRunning = false;
        }

        public void Start() {
            this.isRunning = true;
        }

        public void Stop() {
            this.isRunning = false;
        }

        public void RunSimulation() {
            this.isRunning = true;
            this.thread = new(Simulate);
            this.thread.Start();
        }

        private void Simulate() {
            BeforeSimulationRun();

            for (this.currentReplication = 0; this.currentReplication < this.replicationStock && this.isRunning; this.currentReplication++) {
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
