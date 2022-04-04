using MyJetWallet.Sdk.Service;
using MyYamlParser;

namespace Service.WalletApi.UserProfileApi.Settings
{
	public class SettingsModel
	{
		[YamlProperty("UserProfileApi.SeqServiceUrl")]
		public string SeqServiceUrl { get; set; }

		[YamlProperty("UserProfileApi.ZipkinUrl")]
		public string ZipkinUrl { get; set; }

		[YamlProperty("UserProfileApi.ElkLogs")]
		public LogElkSettings ElkLogs { get; set; }

		[YamlProperty("UserProfileApi.EnableApiTrace")]
		public bool EnableApiTrace { get; set; }

		[YamlProperty("UserProfileApi.MyNoSqlReaderHostPort")]
		public string MyNoSqlReaderHostPort { get; set; }

		[YamlProperty("UserProfileApi.AuthMyNoSqlReaderHostPort")]
		public string AuthMyNoSqlReaderHostPort { get; set; }

		[YamlProperty("UserProfileApi.SessionEncryptionKeyId")]
		public string SessionEncryptionKeyId { get; set; }

		[YamlProperty("UserProfileApi.MyNoSqlWriterUrl")]
		public string MyNoSqlWriterUrl { get; set; }

		[YamlProperty("UserProfileApi.TimeLoggerServiceUrl")]
		public string TimeLoggerServiceUrl { get; set; }

		[YamlProperty("UserProfileApi.UserProgressServiceUrl")]
		public string UserProgressServiceUrl { get; set; }

		[YamlProperty("UserProfileApi.EducationProgressServiceUrl")]
		public string EducationProgressServiceUrl { get; set; }

		[YamlProperty("UserProfileApi.UserRewardServiceUrl")]
		public string UserRewardServiceUrl { get; set; }
	}
}