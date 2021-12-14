﻿using Service.Core.Domain.Models;
using Service.UserProfile.Domain.Models;
using Service.UserProfile.Grpc.Models;

namespace Service.UserProfile.Mappers
{
	public static class AccountMapper
	{
		public static AccountEntity ToEntity(this SaveAccountGrpcRequest request, IEncoderDecoder encoderDecoder) => new AccountEntity
		{
			UserId = request.UserId,
			FirstName = encoderDecoder.Encode(request.FirstName),
			LastName = encoderDecoder.Encode(request.LastName),
			Gender = encoderDecoder.Encode(request.Gender),
			Phone = encoderDecoder.Encode(request.Phone),
			Country = encoderDecoder.Encode(request.Country)
		};

		public static AccountDataGrpcModel ToGrpcModel(this AccountEntity entity, IEncoderDecoder encoderDecoder) => new AccountDataGrpcModel
		{
			FirstName = encoderDecoder.Decode(entity.FirstName),
			LastName = encoderDecoder.Decode(entity.LastName),
			Gender = encoderDecoder.Decode(entity.Gender),
			Phone = encoderDecoder.Decode(entity.Phone),
			Country = encoderDecoder.Decode(entity.Country)
		};
	}
}