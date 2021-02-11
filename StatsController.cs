using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace mc_statstalker {
    public class StatsController : INotifyPropertyChanged {
        public string ServerUri { get; private set; } = @"https://the-wagner.de/stats/";
        public ObservableCollection<PlayerStatModel> PlayerStats { get; private set; } = new ObservableCollection<PlayerStatModel>();

        public void SetServerUri(string uri) {
            ServerUri = uri;
        }

        public async Task AddPlayerStatsAsync(string uuid) {
            var stats = new PlayerStatModel($"{ServerUri.TrimEnd('/')}/{uuid.TrimStart('/').TrimEnd('/')}.json");
            if (await stats.ReadStatsAsync()) {
                PlayerStats.Add(stats);
            }
        }

        public void RemovePlayerStats(object item) {
            PlayerStats.Remove(item as PlayerStatModel);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
