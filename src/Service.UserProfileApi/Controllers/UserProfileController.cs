using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using Service.Core.Client.Constants;
using Service.EducationProgress.Grpc;
using Service.EducationProgress.Grpc.Models;
using Service.Grpc;
using Service.TimeLogger.Grpc;
using Service.TimeLogger.Grpc.Models;
using Service.UserInfo.Crud.Grpc;
using Service.UserInfo.Crud.Grpc.Models;
using Service.UserProfileApi.Mappers;
using Service.UserProfileApi.Models;
using Service.UserProgress.Grpc;
using Service.UserProgress.Grpc.Models;
using Service.UserReward.Grpc;
using Service.UserReward.Grpc.Models;

namespace Service.UserProfileApi.Controllers
{
	[Authorize]
	[ApiController]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	[SwaggerResponse(HttpStatusCode.Unauthorized, null, Description = "Unauthorized")]
	[Route("/api/v1/userprofile")]
	public class UserProfileController : ControllerBase
	{
		private readonly IGrpcServiceProxy<IUserInfoService> _userInfoService;
		private readonly IEducationProgressService _educationProgressService;
		private readonly IUserProgressService _userProgressService;
		private readonly ITimeLoggerService _timeLoggerService;
		private readonly IUserRewardService _userRewardService;

		public UserProfileController(IGrpcServiceProxy<IUserInfoService> userInfoService,
			IEducationProgressService progressService,
			IUserProgressService userProgressService, ITimeLoggerService timeLoggerService,
			IUserRewardService userRewardService)
		{
			_userInfoService = userInfoService;
			_educationProgressService = progressService;
			_userProgressService = userProgressService;
			_timeLoggerService = timeLoggerService;
			_userRewardService = userRewardService;
		}

		[HttpPost("progress")]
		[SwaggerResponse(HttpStatusCode.OK, typeof (DataResponse<ProgressResponse>), Description = "Ok")]
		public async ValueTask<IActionResult> GetProgressAsync()
		{
			Guid? userId = await GetUserIdAsync();
			if (userId == null)
				return StatusResponse.Error(ResponseCode.UserNotFound);

			EducationProgressGrpcResponse progress = await _educationProgressService.GetProgressAsync(new GetEducationProgressGrpcRequest {UserId = userId});
			UnitedProgressGrpcResponse statsProgress = await _userProgressService.GetUnitedProgressAsync(new GetProgressGrpcRequset {UserId = userId});
			ServiceTimeResponse userTime = await _timeLoggerService.GetUserTime(new GetServiceTimeRequest {UserId = userId});
			UserAchievementsGrpcResponse achievements = await _userRewardService.GetUserAchievementsAsync(new GetUserAchievementsGrpcRequest {UserId = userId});

			return DataResponse<ProgressResponse>.Ok(new ProgressResponse
			{
				TaskScore = (progress?.Value).GetValueOrDefault(),
				TasksPassed = (progress?.TasksPassed).GetValueOrDefault(),
				TutorialsPassed = (progress?.TutorialsPassed).GetValueOrDefault(),
				Days = (userTime?.Days).GetValueOrDefault(),
				Habit = statsProgress.Habit.ToModel(),
				Skill = statsProgress.Skill.ToModel(),
				Knowledge = statsProgress.Knowledge.ToModel(),
				Achievements = achievements?.Items
			});
		}

		[HttpPost("achievements")]
		[SwaggerResponse(HttpStatusCode.OK, typeof(DataResponse<AchievementsResponse>), Description = "Ok")]
		public async ValueTask<IActionResult> GetAchievementsAsync()
		{
			Guid? userId = await GetUserIdAsync();
			if (userId == null)
				return StatusResponse.Error(ResponseCode.UserNotFound);

			UserAchievementsGrpcResponse achievements = await _userRewardService.GetUserAchievementsAsync(new GetUserAchievementsGrpcRequest { UserId = userId });

			UserAchievement[] userAchievements = achievements?.Items ?? Array.Empty<UserAchievement>();

			return DataResponse<AchievementsResponse>.Ok(new AchievementsResponse
			{
				UserAchievements = userAchievements,
				UnreceivedAchievements = Enum.GetValues<UserAchievement>().Except(userAchievements).ToArray()
			});
		}

		protected async ValueTask<Guid?> GetUserIdAsync()
		{
			UserInfoResponse userInfoResponse = await _userInfoService.Service.GetUserInfoByLoginAsync(new UserInfoAuthRequest
			{
				UserName = User.Identity?.Name
			});

			return userInfoResponse?.UserInfo?.UserId;
		}
	}
}