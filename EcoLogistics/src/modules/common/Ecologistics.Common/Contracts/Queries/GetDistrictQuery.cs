namespace Ecologistics.Common.Contracts.Queries;

public record GetDistrictQuery(string DistrictId);

public record GetDistrictResponse(string DistrictId, string DistrictName);