using DiscreteSimulation.Randoms.Structures;

namespace DiscreteSimulation.Randoms.Continuous {
    public class EmpiricC : EmpiricDistribution<double> {
        private List<EmpiricData<double>> samples;
        private List<UniformC> generators;

        public EmpiricC(List<EmpiricData<double>> samples) : base(samples) {
            this.samples = new(samples.Count);
            this.generators = new(samples.Count);

            double cumProb = 0.0;

            samples.ForEach(s => {
                EmpiricData<double> data = new(s.Range.First, s.Range.Second, s.Probability + cumProb);
                this.samples.Add(data);
                this.generators.Add(new(s.Range.First, s.Range.Second));
                cumProb += s.Probability;
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
