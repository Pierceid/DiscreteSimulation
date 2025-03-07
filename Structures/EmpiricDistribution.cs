namespace DiscreteSimulation.Randoms.Structures {
    public abstract class EmpiricDistribution<T>(List<EmpiricData<T>> values) : GeneralRandom<T> {
        public List<EmpiricData<T>> Values { get; set; } = values;
        public Random Random { get; set; } = new Random();
    }
}

