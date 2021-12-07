using MyJetWallet.Sdk.Service;
using MyYamlParser;

namespace Service.UserProfile.Settings
{
    public class SettingsModel
    {
        [YamlProperty("UserProfile.SeqServiceUrl")]
        public string SeqServiceUrl { get; set; }

        [YamlProperty("UserProfile.ZipkinUrl")]
        public string ZipkinUrl { get; set; }

        [YamlProperty("UserProfile.ElkLogs")]
        public LogElkSettings ElkLogs { get; set; }

        [YamlProperty("UserProfile.PostgresConnectionString")]
        public string PostgresConnectionString { get; set; }
    }
}
