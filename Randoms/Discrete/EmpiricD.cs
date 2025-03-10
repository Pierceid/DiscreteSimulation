using DiscreteSimulation.Structures;

namespace DiscreteSimulation.Randoms.Discrete {
    public class EmpiricD : GeneralRandom<int> {
        private List<EmpiricData<int>> samples;
        private List<UniformD> generators;

        public EmpiricD(List<EmpiricData<int>> samples) {
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
            double rng = Generator.NextDouble();

            for (int i = 0; i < this.samples.Count; i++) {
                if (rng < this.samples[i].Probability) {
                    return this.generators[i].Next();
                }
            }

            throw new ArgumentException("Invalid probability distribution");
        }
    }
}
