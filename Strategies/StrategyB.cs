namespace DiscreteSimulation.Strategies {
    public class StrategyB : Strategy {
        public override void DetermineSupplier(int week) {
            if (week < supplier2Switch) {
                if (rng.Next() < supplier2Initial.Next()) {
                    mufflerStock += mufflerSupply;
                    brakeStock += brakeSupply;
                    lightStock += lightSupply;
                }
            } else {
                if (rng.Next() < supplier2Adjusted.Next()) {
                    mufflerStock += mufflerSupply;
                    brakeStock += brakeSupply;
                    lightStock += lightSupply;
                }
            }
        }
    }
}
