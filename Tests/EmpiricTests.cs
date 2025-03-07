using DiscreteSimulation.Randoms.Continuous;
using DiscreteSimulation.Randoms.Discrete;
using DiscreteSimulation.Randoms.Structures;
using DiscreteSimulation.Randoms.Utilities;

namespace DiscreteSimulation.Randoms.Tests {
    public class EmpiricTests {
        public void TestDiscreteEmpiricDistribution() {
            var bins = new List<EmpiricData<int>> {
                new EmpiricData<int>(30, 60, 0.2),
                new EmpiricData<int>(60, 100, 0.4),
                new EmpiricData<int>(100, 140, 0.3),
                new EmpiricData<int>(140, 160, 0.1)
            };
            var empiric = new EmpiricD(bins);
            TestEmpiricDistribution(bins, empiric.Next, "Discrete Empiric Test");
        }

        public void TestContinuousEmpiricDistribution() {
            var bins = new List<EmpiricData<double>> {
                new EmpiricData<double>(0.05, 0.1, 0.4),
                new EmpiricData<double>(0.1, 0.5, 0.3),
                new EmpiricData<double>(0.5, 0.7, 0.2),
                new EmpiricData<double>(0.7, 0.8, 0.06),
                new EmpiricData<double>(0.8, 0.95, 0.04)
            };
            var empiric = new EmpiricC(bins);
            TestEmpiricDistribution(bins, empiric.Next, "Continuous Empiric Test");
        }

        public void TestStrategy(int index) {

        }

        private void TestEmpiricDistribution<T>(List<EmpiricData<T>> bins, Func<T> nextValue, string testName) where T : IComparable<T> {
            int totalSamples = 1_000_000;
            var result = bins.ToDictionary(bin => bin, _ => 0);
            bool isDiscrete = typeof(T) == typeof(int);

            try {
                // Generate samples and count occurrences
                for (int i = 0; i < totalSamples; i++) {
                    T value = nextValue();
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
                    Console.WriteLine($"{Utility.FormatRange(r.Key.Range.First, r.Key.Range.Second)}: {observedProbability:P}");
                }
            } catch (ArgumentException ex) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{testName} Failed: {ex.Message}");
            }
        }
    }
}
