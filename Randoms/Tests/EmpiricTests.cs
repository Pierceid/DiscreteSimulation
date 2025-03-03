using DiscreteSimulation.Randoms.Continuous;
using DiscreteSimulation.Randoms.Discrete;
using DiscreteSimulation.Randoms.Structures;

namespace DiscreteSimulation.Randoms.Tests {
    public class EmpiricTests {
        public void TestDiscreteEmpiricDistribution() {
            var bins = new List<EmpiricData<int>>
            {
                new(1, 2, 0.5),
                new(3, 4, 0.2),
                new(5, 6, 0.3)
            };
            var empiric = new EmpiricD(bins);

            TestEmpiricDistribution(bins, empiric.Next, "Discrete Empiric Test");
        }

        public void TestContinuousEmpiricDistribution() {
            var bins = new List<EmpiricData<double>>
            {
                new(1.2, 3.6, 0.5),
                new(3.6, 4.8, 0.2),
                new(4.8, 6.0, 0.3)
            };
            var empiric = new EmpiricC(bins);

            TestEmpiricDistribution(bins, empiric.Next, "Continuous Empiric Test");
        }

        private void TestEmpiricDistribution<T>(List<EmpiricData<T>> bins, Func<T> nextValueGenerator, string testName) where T : IComparable<T> {
            int totalSamples = 10_000;
            var result = bins.ToDictionary(bin => bin, _ => 0);

            try {
                // Generate samples and count occurrences
                for (int i = 0; i < totalSamples; i++) {
                    T value = nextValueGenerator();
                    foreach (var bin in bins) {
                        if (value.CompareTo(bin.Range.Second) < 0) {
                            result[bin]++;
                            break;
                        }
                    }
                }

                // Display results
                Console.WriteLine($"{testName} - Observed Frequencies:");
                foreach (var r in result) {
                    double observedProbability = r.Value / (double)totalSamples;
                    Console.WriteLine($"<{r.Key.Range.First},{r.Key.Range.Second}): {observedProbability:P}");
                }
            } catch (ArgumentException ex) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{testName} Failed: {ex.Message}");
            }
        }
    }
}
