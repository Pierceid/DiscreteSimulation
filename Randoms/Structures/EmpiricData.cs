namespace DiscreteSimulation.Randoms.Structures {
    public class EmpiricData<T> {
        public Pair<T, T> Range { get; set; }
        public double Probability { get; set; }
        public int Seed { get; set; }

        public EmpiricData(T min, T max, double probability, int seed = 0) {
            Range = new(min, max);
            Probability = probability;
            Seed = seed;
        }

        public EmpiricData(Pair<T, T> range, double probability, int seed = 0) {
            Range = range;
            Probability = probability;
            Seed = seed;
        }
    }
}
