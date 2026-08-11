namespace Ecologistics.PickupManagement.UseCases.GetAllDistricts;

public record GetAllDistrictsQuery(
    string? Region,
    string? SearchKey,
    int Page,
    int PageSize);

public record GetAllDistrictDto(string Id, string Name, string Region);
