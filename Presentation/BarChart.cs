﻿using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Wpf;

namespace DiscreteSimulation.Presentation {
    public class BarChart {
        public BarChart(PlotView plotView, string title, double[] costs) {
            var model = new PlotModel { Title = title };

            var columnSeries = new LinearBarSeries {
                FillColor = OxyColors.SkyBlue,
                StrokeColor = OxyColors.Black,
                StrokeThickness = 1,
                BarWidth = 10
            };

            for (int i = 0; i < costs.Length; i++) {
                columnSeries.Points.Add(new DataPoint(i, costs[i]));
            }

            model.Series.Add(columnSeries);

            model.Axes.Add(new CategoryAxis {
                Position = AxisPosition.Bottom,
                Title = "Days",
                MajorStep = 7,
                MinimumPadding = 1,
                AbsoluteMinimum = 0
            });

            model.Axes.Add(new LinearAxis {
                Position = AxisPosition.Left,
                Title = "Costs",
                MajorStep = Math.Round(costs[^1] * 0.2),
                MinimumPadding = 1,
                AbsoluteMinimum = 0
            });

            plotView.Model = model;
        }
    }
}
