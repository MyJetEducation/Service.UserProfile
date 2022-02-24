using MyJetWallet.Sdk.Service;
using MyYamlParser;

namespace Service.UserProfileApi.Settings
{
	public class SettingsModel
	{
		[YamlProperty("UserProfileApi.SeqServiceUrl")]
		public string SeqServiceUrl { get; set; }

		[YamlProperty("UserProfileApi.ZipkinUrl")]
		public string ZipkinUrl { get; set; }

		[YamlProperty("UserProfileApi.ElkLogs")]
		public LogElkSettings ElkLogs { get; set; }

		[YamlProperty("UserProfileApi.JwtAudience")]
		public string JwtAudience { get; set; }

		[YamlProperty("UserProfileApi.TimeLoggerServiceUrl")]
		public string TimeLoggerServiceUrl { get; set; }

		[YamlProperty("UserProfileApi.UserProgressServiceUrl")]
		public string UserProgressServiceUrl { get; set; }

		[YamlProperty("UserProfileApi.UserInfoCrudServiceUrl")]
		public string UserInfoCrudServiceUrl { get; set; }

		[YamlProperty("UserProfileApi.EducationProgressServiceUrl")]
		public string EducationProgressServiceUrl { get; set; }

		[YamlProperty("UserProfileApi.UserRewardServiceUrl")]
		public string UserRewardServiceUrl { get; set; }
	}
}