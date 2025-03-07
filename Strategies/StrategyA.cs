namespace DiscreteSimulation.Strategies {
    public class StrategyA : Strategy {
        public override void DetermineSupplier(int week) {
            if (week < supplier1Switch) {
                if (rng.Next() < supplier1Initial.Next()) {
                    mufflerStock += mufflerSupply;
                    brakeStock += brakeSupply;
                    lightStock += lightSupply;
                }
            } else {
                if (rng.Next() < supplier1Adjusted.Next()) {
                    mufflerStock += mufflerSupply;
                    brakeStock += brakeSupply;
                    lightStock += lightSupply;
                }
            }
        }
    }
}
