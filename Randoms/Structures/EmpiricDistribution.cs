namespace DiscreteSimulation.Randoms.Structures {
    public abstract class EmpiricDistribution<T> : GeneralRandom<T> {
        public List<EmpiricData<T>> Values { get; set; }
        public Random Random { get; set; }

        protected EmpiricDistribution(List<EmpiricData<T>> values, int seed = 0) {
            Values = values;
            Random = new Random(seed);

            double sum = Values.Sum(x => x.Probability);

            // tolerating the rounding up mistake
            if (Math.Abs(sum - 1.0) > double.Epsilon) {
                throw new ArgumentException("Sum of probabilities is not equal to 1");
            }
        }
    }
}

