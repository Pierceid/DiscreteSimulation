using DiscreteSimulation.Randoms.Structures;

namespace DiscreteSimulation.Randoms.Discrete {
    public class EmpiricD : EmpiricDistribution<int> {
        private List<EmpiricData<int>> samples;
        private List<UniformD> generators;

        public EmpiricD(List<EmpiricData<int>> samples, int seed = 0) : base(samples, seed) {
            this.samples = new(samples.Count);
            this.generators = new(samples.Count);

            double cumProb = 0.0;

            samples.ForEach(sample => {
                EmpiricData<int> data = new(sample.Range, sample.Probability + cumProb);
                this.samples.Add(data);
                this.generators.Add(new(sample.Range.First, sample.Range.Second, sample.Seed));
                cumProb += sample.Probability;
            });
        }

        public override int Next() {
            double binProb = Generator.NextDouble();

            for (int i = 0; i < this.samples.Count; i++) {
                if (binProb < this.samples[i].Probability) {
                    return this.generators[i].Next();
                }
            }

            throw new ArgumentException("Invalid probability distribution");
        }
    }
}
