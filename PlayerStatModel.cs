using System;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace mc_statstalker {
    public class PlayerStatModel {
        private const string MOJANG_API = @"https://api.mojang.com/user/profiles/{0}/names";

        public Uri Uri { get; private set; }
        public string Uuid { get; private set; }
        public string Name { get; private set; }

        public int DeathsCnt { get; private set; }
        public int MobKillsCnt { get; private set; }
        public int PlayerKillsCnt { get; private set; }
        public int Playtime { get; private set; }

        public PlayerStatModel(Uri json_uri) {
            Uri = json_uri;
            Uuid = json_uri.Segments[^1];
        }

        public PlayerStatModel(string json_uri) {
            Uri = new Uri(json_uri);
            Uuid = Path.GetFileNameWithoutExtension(Uri.Segments[^1]);
        }

        public async Task<bool> ReadStatsAsync() {
            var name_request = WebRequest.Create(String.Format(MOJANG_API, Uuid));
            using (WebResponse name_response = await name_request.GetResponseAsync())
            using (Stream name_stream = name_response.GetResponseStream())
            using (StreamReader name_reader = new StreamReader(name_stream)) {
                string raw_data = await name_reader.ReadToEndAsync();
                Name = JsonDocument.Parse(raw_data).RootElement[0].GetProperty("name").GetString();
            }

            if (Name == null) {
                return false;
            }

            var stat_request = WebRequest.Create(Uri);
            using (WebResponse stat_response = await stat_request.GetResponseAsync())
            using (Stream stat_stream = stat_response.GetResponseStream())
            using (StreamReader stat_reader = new StreamReader(stat_stream)) {
                string raw_data = await stat_reader.ReadToEndAsync();
                JsonElement stat_data = JsonDocument.Parse(raw_data).RootElement.GetProperty("stats").GetProperty("minecraft:custom");

                JsonElement element = new JsonElement();
                DeathsCnt = stat_data.TryGetProperty("minecraft:deaths", out element) ? element.GetInt32() : 0;
                MobKillsCnt = stat_data.TryGetProperty("minecraft:mob_kills", out element) ? element.GetInt32() : 0;
                PlayerKillsCnt = stat_data.TryGetProperty("minecraft:player_kills", out element) ? element.GetInt32() : 0;
                Playtime = stat_data.TryGetProperty("minecraft:play_one_minute", out element) ? element.GetInt32() / 20 /* ticks/s */ / 60 /* s/min */ / 60 /* min/h */ : 0;
            }

            return true;
        }
    }
}
