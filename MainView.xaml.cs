using System.Windows;

namespace mc_statstalker {
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window {
        private StatsController controller = new StatsController();

        public MainView() {
            DataContext = controller;

            InitializeComponent();

            ServerField.Text = controller.ServerUri;
        }

        private void SetButton_Click(object sender, RoutedEventArgs e) {
            controller.SetServerUri(ServerField.Text);
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e) {
            await controller.AddPlayerStatsAsync(UuidField.Text);
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e) {
            controller.RemovePlayerStats(StatsGrid.SelectedItem);
        }
    }
}
