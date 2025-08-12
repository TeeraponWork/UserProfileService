using Domain.Enums;
using MediatR;

namespace Application.Profiles.Commands
{
    public sealed record UpsertProfileCommand(
        string? DisplayName,
        Sex Sex,
        DateOnly? DateOfBirth,
        string? BloodType
    ) : IRequest<Guid>; // return ProfileId
}
