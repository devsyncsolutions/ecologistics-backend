using Ecologistics.Common.Enums;

namespace Ecologistics.Common.Contracts.Commands;

public record CreateDeviceTokenCommand(
    string UserId,
    string Token,
    DevicePlatform DevicePlatform
);