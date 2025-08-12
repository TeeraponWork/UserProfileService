using Domain.Enums;

namespace Application.Profiles
{
    public sealed record ProfileDto(
        Guid Id,
        Guid UserId,
        string? DisplayName,
        Sex Sex,
        DateOnly? DateOfBirth,
        int? HeightCm,
        decimal? WeightKg,
        string? BloodType,
        IReadOnlyList<string> ChronicConditions,
        IReadOnlyList<string> Allergies,
        DateTime CreatedAtUtc,
        DateTime UpdatedAtUtc
    );
}
