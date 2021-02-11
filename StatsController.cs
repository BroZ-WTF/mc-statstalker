using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace mc_statstalker {
    public class StatsController : INotifyPropertyChanged {
        public ObservableCollection<PlayerStatModel> PlayerStats { get; private set; } = new ObservableCollection<PlayerStatModel>();

        public async Task AddPlayerStatsAsync(string uri) {
            var stats = new PlayerStatModel(uri);
            if (await stats.ReadStatsAsync()) {
                PlayerStats.Add(stats);
            }
        }

        public void RemovePlayerStats(string uri) {
            PlayerStats.Remove(PlayerStats.Where(stat => stat.Uri.AbsoluteUri == uri).Single());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
