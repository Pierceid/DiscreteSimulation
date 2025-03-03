namespace DiscreteSimulation.Randoms.Utilities {
    public static class Utility {
        public static string FormatRange(object min, object max, bool isDiscrete) {
            return $"<{min.ToString()?.Replace(',', '.')},{max.ToString()?.Replace(',', '.')}{(isDiscrete ? '>' : ')')}";
        }
    }
}
