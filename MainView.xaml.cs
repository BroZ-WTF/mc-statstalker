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
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e) {
            await controller.AddPlayerStatsAsync(@"https://the-wagner.de/stats/5f3f25ee-4cad-4160-ab23-fd4087869608.json");
            await controller.AddPlayerStatsAsync(@"https://the-wagner.de/stats/9aecff5d-33cb-43f3-b279-85821fa84d18.json");
            await controller.AddPlayerStatsAsync(@"https://the-wagner.de/stats/834139bf-8770-45de-a877-3f5b664810c8.json");
            await controller.AddPlayerStatsAsync(@"https://the-wagner.de/stats/bc1b47ac-f7fb-493b-9a0c-84bf74c22c55.json");
            await controller.AddPlayerStatsAsync(@"https://the-wagner.de/stats/bdcae066-b237-4543-a90d-72f56daf4af3.json");
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e) {

        }
    }
}
