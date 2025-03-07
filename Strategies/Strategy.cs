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
        public double TotalCost { get; private set; } = 0.0;

        // Constants
        protected const int MUFFLER_SUPPLY = 100;
        protected const int BRAKE_SUPPLY = 200;
        protected const int LIGHT_SUPPLY = 150;
        protected const int SUPPLIER1_SWITCH = 10;
        protected const int SUPPLIER2_SWITCH = 15;
        protected const int TOTAL_WEEKS = 30;
        protected const double MUFFLER_UNIT_COST = 0.2;
        protected const double BRAKE_UNIT_COST = 0.3;
        protected const double LIGHT_UNIT_COST = 0.25;
        protected const double SHORTAGE_PENALTY = 0.3;

        // Static Data for Empirical Distributions
        private static readonly List<EmpiricData<int>> LightDemandSamples =
        [
            new(30, 60, 0.2),
            new(60, 100, 0.4),
            new(100, 140, 0.3),
            new(140, 160, 0.1)
        ];

        private static readonly List<EmpiricData<double>> Supplier2InitialSamples =
        [
            new(0.05, 0.1, 0.4),
            new(0.1, 0.5, 0.3),
            new(0.5, 0.7, 0.2),
            new(0.7, 0.8, 0.06),
            new(0.8, 0.95, 0.04)
        ];

        private static readonly List<EmpiricData<double>> Supplier2AdjustedSamples =
        [
            new(0.05, 0.1, 0.2),
            new(0.1, 0.5, 0.4),
            new(0.5, 0.7, 0.3),
            new(0.7, 0.8, 0.06),
            new(0.8, 0.95, 0.04)
        ];

        protected Strategy() {
            mufflerDemand = new UniformD(50, 101);
            brakeDemand = new UniformD(60, 251);
            lightDemand = new EmpiricD(LightDemandSamples);
            supplier1Initial = new UniformC(0.1, 0.7);
            supplier1Adjusted = new UniformC(0.3, 0.95);
            supplier2Initial = new EmpiricC(Supplier2InitialSamples);
            supplier2Adjusted = new EmpiricC(Supplier2AdjustedSamples);
            rng = new UniformC(0, 1);
        }

        protected void ResetInventory() {
            mufflerStock = 0;
            brakeStock = 0;
            lightStock = 0;
        }

        protected void RestockComponents() {
            mufflerStock += MUFFLER_SUPPLY;
            brakeStock += BRAKE_SUPPLY;
            lightStock += LIGHT_SUPPLY;
        }

        private void ComputeCosts() {
            TotalCost += 4 * (mufflerStock * MUFFLER_UNIT_COST + brakeStock * BRAKE_UNIT_COST + lightStock * LIGHT_UNIT_COST);

            mufflerStock -= mufflerDemand.Next();
            brakeStock -= brakeDemand.Next();
            lightStock -= lightDemand.Next();

            ApplyShortagePenalty(ref mufflerStock);
            ApplyShortagePenalty(ref brakeStock);
            ApplyShortagePenalty(ref lightStock);

            TotalCost += 3 * (mufflerStock * MUFFLER_UNIT_COST + brakeStock * BRAKE_UNIT_COST + lightStock * LIGHT_UNIT_COST);
        }

        private void ApplyShortagePenalty(ref int stock) {
            if (stock < 0) {
                TotalCost += Math.Abs(stock) * SHORTAGE_PENALTY;
                stock = 0;
            }
        }

        public void RunStrategy() {
            ResetInventory();

            for (int week = 0; week < TOTAL_WEEKS; week++) {
                DetermineSupplier(week);
                ComputeCosts();
            }
        }

        public abstract void DetermineSupplier(int week);
    }
}
