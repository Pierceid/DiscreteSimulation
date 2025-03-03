using DiscreteSimulation.Randoms.Structures;

namespace DiscreteSimulation.Randoms.Discrete {
    public class UniformD : GeneralRandom<int> {
        private int min;
        private int max;

        public UniformD(int min, int max, int seed = 0) {
            this.min = min;
            this.max = max;
            Seed = seed;
        }

        public override int Next() {
            return Generator.Next(this.min, this.max + 1);
        }
    }
}
