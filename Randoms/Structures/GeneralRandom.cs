namespace DiscreteSimulation.Randoms.Structures {
    public abstract class GeneralRandom<T> {
        public int Seed { get; set; }
        public Random Generator { get; set; }
        private static readonly Random random = new(0);

        public GeneralRandom() {
            Seed = GetNextSeed();
            Generator = new Random(Seed);
        }

        public abstract T Next();

        public static int GetNextSeed() => random.Next(int.MaxValue);
    }
}
