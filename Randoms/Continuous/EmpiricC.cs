using DiscreteSimulation.Randoms.Structures;

namespace DiscreteSimulation.Randoms.Continuous {
    public class EmpiricC : EmpiricDistribution<double> {
        private List<EmpiricData<double>> samples;
        private List<UniformC> generators;

        public EmpiricC(List<EmpiricData<double>> samples, int seed = 0) : base(samples, seed) {
            this.samples = new(samples.Count);
            this.generators = new(samples.Count);

            double cumProb = 0.0;

            samples.ForEach(sample => {
                EmpiricData<double> data = new(sample.Range, sample.Probability + cumProb);
                this.samples.Add(data);
                this.generators.Add(new(sample.Range.First, sample.Range.Second, sample.Seed));
                cumProb += sample.Probability;
            });
        }

        public override double Next() {
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
