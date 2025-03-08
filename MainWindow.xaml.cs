using DiscreteSimulation.Presentation;
using DiscreteSimulation.Strategies;
using System.Windows;
using System.Windows.Controls;

namespace DiscreteSimulation {
    public partial class MainWindow : Window {
        private Facade facade;

        public MainWindow() {
            InitializeComponent();

            facade = new(plotView);
        }

        private void ButtonClick(object sender, RoutedEventArgs e) {
            if (sender is Button button) {
                if (button == btnStart) {
                    facade.StartSimulation();
                } else if (button == btnStop) {
                    facade.StopSimulation();
                }
            }
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (sender is ComboBox comboBox && comboBox == cbStrategies) {
                UpdateStrategy();
                UpdateUI();
            }
        }

        private void UpdateStrategy() {
            switch (cbStrategies.SelectedIndex) {
                case 0:
                    facade.SetStrategy(new StrategyA());
                    break;
                case 1:
                    facade.SetStrategy(new StrategyB());
                    break;
                case 2:
                    facade.SetStrategy(new StrategyC());
                    break;
                case 3:
                    facade.SetStrategy(new StrategyD());
                    break;
                case 4:
                    facade.SetStrategy(new StrategyX());
                    break;
                default:
                    break;
            }
        }

        private void UpdateUI() {
            if (cbStrategies.SelectedIndex == 4) {
                groupBoxStrategyXControls.Visibility = Visibility.Visible;
            } else {
                groupBoxStrategyXControls.Visibility = Visibility.Collapsed;
            }
        }
    }
}