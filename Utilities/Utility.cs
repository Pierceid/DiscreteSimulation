using DiscreteSimulation.Strategies;

namespace DiscreteSimulation.Utilities {
    public static class Utility {
        public static string FormatRange(object min, object max) {
            return $"<{min.ToString()?.Replace(',', '.')},{max.ToString()?.Replace(',', '.')})";
        }

        public static StrategyX ParseStrategyX(string? m, string? b, string? l, bool? s1, bool? s2) {
            if (!int.TryParse(m, out int mufflerSupply)) mufflerSupply = 0;

            if (!int.TryParse(b, out int brakeSupply)) brakeSupply = 0;

            if (!int.TryParse(l, out int lightSupply)) lightSupply = 0;

            bool isSupplier1 = s1 ?? false;

            bool isSupplier2 = s2 ?? false;

            return new StrategyX(mufflerSupply, brakeSupply, lightSupply, isSupplier1, isSupplier2);
        }
    }
}
