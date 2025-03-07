using DiscreteSimulation.Structures;

namespace DiscreteSimulation.Randoms.Discrete {
    public class EmpiricD : EmpiricDistribution<int> {
        private List<EmpiricData<int>> samples;
        private List<UniformD> generators;

        public EmpiricD(List<EmpiricData<int>> samples) : base(samples) {
            this.samples = new(samples.Count);
            this.generators = new(samples.Count);

            double cumProb = 0.0;

            samples.ForEach(s => {
                EmpiricData<int> data = new(s.Range.First, s.Range.Second, s.Probability + cumProb);
                this.samples.Add(data);
                this.generators.Add(new(s.Range.First, s.Range.Second));
                cumProb += s.Probability;
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
