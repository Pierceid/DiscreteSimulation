namespace DiscreteSimulation.Simulations {
    public class BuffonNeedle(long replicationsCount) : SimulationCore(replicationsCount) {
        private Random randomPosition = new();
        private Random randomAlpha = new();
        private double needleLength = 5.0;
        private double distance = 10.0;
        private long intersectionCount = 0;

        public override void Experiment() {
            double y = randomPosition.NextDouble() * distance;
            double a = needleLength * Math.Sin(randomAlpha.NextDouble() * Math.PI);

            if (y + a >= distance) intersectionCount++;
        }

        public override void BeforeSimulation() {

        }

        public override void AfterSimulation() {

        }

        public override void BeforeSimulationRun() {

        }

        public override void AfterSimulationRun() {

        }
    }
}
