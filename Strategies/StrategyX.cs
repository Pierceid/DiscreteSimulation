namespace DiscreteSimulation.Strategies {
    public class StrategyX : Strategy {
        private int mufflerSupply;
        private int brakeSupply;
        private int lightSupply;
        private bool isSupplier1;
        private bool isSupplier2;

        public StrategyX(int mufflerSupply, int brakeSupply, int lightSupply, bool isSupplier1, bool isSupplier2) {
            this.mufflerSupply = mufflerSupply;
            this.brakeSupply = brakeSupply;
            this.lightSupply = lightSupply;
            this.isSupplier1 = isSupplier1;
            this.isSupplier2 = isSupplier2;
        }

        public override void DetermineSupplier(int week) {
            if (isSupplier1) {
                if (week < SUPPLIER1_SWITCH) {
                    if (rng.Next() < supplier1Initial.Next()) {
                        RestockSupplies();
                    }
                } else {
                    if (rng.Next() < supplier1Adjusted.Next()) {
                        RestockSupplies();
                    }
                }
            }

            if (isSupplier2) {
                if (week < SUPPLIER2_SWITCH) {
                    if (rng.Next() < supplier2Initial.Next()) {
                        RestockSupplies();
                    }
                } else {
                    if (rng.Next() < supplier2Adjusted.Next()) {
                        RestockSupplies();
                    }
                }
            }
        }

        private void RestockSupplies() {
            mufflerStock += mufflerSupply;
            brakeStock += brakeSupply;
            lightStock += lightSupply;
        }
    }
}
