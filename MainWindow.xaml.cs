using DiscreteSimulation.Presentation;
using DiscreteSimulation.Strategies;
using DiscreteSimulation.Utilities;
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
                    UpdateStrategy();
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
                    facade.SetStrategy(Utility.ParseStrategyX(
                        txtMufflers.Text,
                        txtBrakes.Text,
                        txtLights.Text,
                        chkSupplier1.IsChecked,
                        chkSupplier2.IsChecked,
                        (int)sldrSupplier1Period.Value,
                        (int)sldrSupplier2Period.Value,
                        txtSupplier1Offset.Text,
                        txtSupplier2Offset.Text
                    ));
                    break;
                default:
                    break;
            }
        }

        private void UpdateUI() {
            if (cbStrategies.SelectedIndex == 4) {
                gbStrategyXControls.Visibility = Visibility.Visible;
            } else {
                gbStrategyXControls.Visibility = Visibility.Collapsed;
            }
        }
    }
}