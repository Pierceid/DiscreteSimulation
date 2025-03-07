﻿namespace DiscreteSimulation.Strategies {
    public class StrategyC : Strategy {
        public override void DetermineSupplier(int week) {
            if (week % 2 == 0) {
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
            } else {
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
}
