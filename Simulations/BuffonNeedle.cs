namespace DiscreteSimulation.Simulations {
    public class BuffonNeedle(int replicationStock) : SimulationCore(replicationStock) {
        private Random random = new();
        private double needleLength = 5.0;
        private double distance = 10.0;
        private long intersectionCount = 0;

        public override void Experiment() {
            double y = this.random.NextDouble() * this.distance;
            double a = this.needleLength * Math.Sin(this.random.NextDouble() * Math.PI);

            if (y + a >= this.distance) {
                this.intersectionCount++;
            }
        }

        public override void BeforeSimulation() {
        }

        public override void AfterSimulation() {

        }

        public override void BeforeSimulationRun() {
            this.intersectionCount = 0;
        }

        public override void AfterSimulationRun() {
            double estimatedPi = (2.0 * this.needleLength * this.replicationStock) / (this.distance * this.intersectionCount);
            Console.WriteLine($"\nEstimated PI: {estimatedPi}\n");
        }
    }
}
