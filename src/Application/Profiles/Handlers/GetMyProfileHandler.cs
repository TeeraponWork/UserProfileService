using Application.Abstractions;
using Application.Profiles.Queries;
using MediatR;

namespace Application.Profiles.Handlers
{
    public sealed class GetMyProfileHandler : IRequestHandler<GetMyProfileQuery, ProfileDto>
    {
        private readonly IUserContext _ctx;
        private readonly IUserProfileRepository _repo;
        public GetMyProfileHandler(IUserContext ctx, IUserProfileRepository repo)
        { _ctx = ctx; _repo = repo; }

        public async Task<ProfileDto> Handle(GetMyProfileQuery request, CancellationToken ct)
        {
            var p = await _repo.GetByUserIdAsync(_ctx.UserId, ct)
                ?? throw new InvalidOperationException("Profile not found");
            return new ProfileDto(
                p.Id, p.UserId, p.DisplayName, p.Sex, p.DateOfBirth, p.HeightCm, p.WeightKg, p.BloodType,
                p.ChronicConditions.Select(c => c.Name).ToList(),
                p.Allergies.Select(a => a.Name).ToList(),
                p.CreatedAtUtc, p.UpdatedAtUtc
            );
        }
    }
}
