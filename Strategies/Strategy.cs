using DiscreteSimulation.Randoms.Continuous;
using DiscreteSimulation.Randoms.Discrete;
using DiscreteSimulation.Structures;

namespace DiscreteSimulation.Strategies {
    public abstract class Strategy {
        // Demand Distributions
        protected UniformD mufflerDemand;
        protected UniformD brakeDemand;
        protected EmpiricD lightDemand;

        // Supplier Lead-Time Distributions
        protected UniformC supplier1Initial;
        protected UniformC supplier1Adjusted;
        protected EmpiricC supplier2Initial;
        protected EmpiricC supplier2Adjusted;

        // Random Number Generator
        protected UniformC rng;

        // Component Inventory
        protected int mufflerStock = 0;
        protected int brakeStock = 0;
        protected int lightStock = 0;

        // Total Cost
        public double TotalCost { get; set; } = 0.0;

        // Constants
        protected const int mufflerSupply = 100;
        protected const int brakeSupply = 200;
        protected const int lightSupply = 150;
        protected const int supplier1Switch = 10;
        protected const int supplier2Switch = 15;
        protected const int totalWeeks = 30;
        protected const double mufflerUnitCost = 0.2;
        protected const double brakeUnitCost = 0.3;
        protected const double lightUnitCost = 0.25;
        protected const double shortagePenalty = 0.3;

        // Static Data for Empirical Distributions
        private static readonly List<EmpiricData<int>> lightDemandSamples =
        [
            new EmpiricData<int>(30, 60, 0.2),
            new EmpiricData<int>(60, 100, 0.4),
            new EmpiricData<int>(100, 140, 0.3),
            new EmpiricData<int>(140, 160, 0.1)
        ];

        private static readonly List<EmpiricData<double>> supplier2InitialSamples =
        [
            new EmpiricData<double>(0.05, 0.1, 0.4),
            new EmpiricData<double>(0.1, 0.5, 0.3),
            new EmpiricData<double>(0.5, 0.7, 0.2),
            new EmpiricData<double>(0.7, 0.8, 0.06),
            new EmpiricData<double>(0.8, 0.95, 0.04)
        ];

        private static readonly List<EmpiricData<double>> supplier2AdjustedSamples =
        [
            new EmpiricData<double>(0.05, 0.1, 0.2),
            new EmpiricData<double>(0.1, 0.5, 0.4),
            new EmpiricData<double>(0.5, 0.7, 0.3),
            new EmpiricData<double>(0.7, 0.8, 0.06),
            new EmpiricData<double>(0.8, 0.95, 0.04)
        ];

        protected Strategy() {
            mufflerDemand = new UniformD(50, 101);
            brakeDemand = new UniformD(60, 251);
            lightDemand = new EmpiricD(lightDemandSamples);
            supplier1Initial = new UniformC(0.1, 0.7);
            supplier1Adjusted = new UniformC(0.3, 0.95);
            supplier2Initial = new EmpiricC(supplier2InitialSamples);
            supplier2Adjusted = new EmpiricC(supplier2AdjustedSamples);
            rng = new UniformC(0, 1);
        }

        protected void ResetInventory() {
            mufflerStock = brakeStock = lightStock = 0;
        }

        protected void ComputeCosts() {
            TotalCost += 4 * (mufflerStock * mufflerUnitCost + brakeStock * brakeUnitCost + lightStock * lightUnitCost);

            mufflerStock -= mufflerDemand.Next();
            brakeStock -= brakeDemand.Next();
            lightStock -= lightDemand.Next();

            if (mufflerStock < 0) {
                TotalCost += Math.Abs(mufflerStock) * shortagePenalty;
                mufflerStock = 0;
            }

            if (brakeStock < 0) {
                TotalCost += Math.Abs(brakeStock) * shortagePenalty;
                brakeStock = 0;
            }

            if (lightStock < 0) {
                TotalCost += Math.Abs(lightStock) * shortagePenalty;
                lightStock = 0;
            }

            TotalCost += 3 * (mufflerStock * mufflerUnitCost + brakeStock * brakeUnitCost + lightStock * lightUnitCost);
        }

        public void RunStrategy() {
            ResetInventory();

            for (int week = 0; week < totalWeeks; week++) {
                DetermineSupplier(week);
                ComputeCosts();
            }
        }

        public abstract void DetermineSupplier(int week);
    }
}
