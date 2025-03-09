using DiscreteSimulation.Presentation;
using DiscreteSimulation.Strategies;
using DiscreteSimulation.Utilities;
using System.Windows;
using System.Windows.Controls;

namespace DiscreteSimulation.Windows {
    public partial class MainWindow : Window {
        private Facade facade;

        public MainWindow() {
            InitializeComponent();

            facade = new(this);
            facade.InitGraph(plotView);
        }

        private void ButtonClick(object sender, RoutedEventArgs e) {
            if (sender is Button button) {
                if (button == btnStart) {
                    UpdateStrategy();
                    facade.StartSimulation();
                } else if (button == btnStop) {
                    facade.StopSimulation();
                } else if (button == btnPrint) {
                    facade.PrintReplication();
                }
            }
        }

        private void ComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (sender is ComboBox comboBox) {
                if (comboBox == cbStrategies) {
                    UpdateUI();
                }
            }
        }

        private void TextBoxLostFocus(object sender, RoutedEventArgs e) {
            if (sender is TextBox textBox) {
                if (textBox == txtReplications) {
                    UpdateWarehouse();
                }
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

        private void UpdateWarehouse() {
            if (!int.TryParse(txtReplications.Text, out int replications)) replications = 0;

            facade.InitWarehouse(replications);
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