using Application.Abstractions;
using Application.Profiles.Commands;
using Domain.Entities;
using MediatR;

namespace Application.Profiles.Handlers
{
    public sealed class UpsertProfileHandler : IRequestHandler<UpsertProfileCommand, Guid>
    {
        private readonly IUserContext _ctx;
        private readonly IUserProfileRepository _repo;

        public UpsertProfileHandler(IUserContext ctx, IUserProfileRepository repo)
        {
            _ctx = ctx; _repo = repo;
        }

        public async Task<Guid> Handle(UpsertProfileCommand request, CancellationToken ct)
        {
            var existing = await _repo.GetByUserIdAsync(_ctx.UserId, ct);
            if (existing is null)
            {
                var profile = UserProfileAggregate.Create(_ctx.UserId, request.DisplayName, request.Sex, request.DateOfBirth);
                profile.UpdateBasics(request.DisplayName, request.Sex, request.DateOfBirth, request.BloodType);
                await _repo.AddAsync(profile, ct);
                await _repo.SaveChangesAsync(ct);
                return profile.Id;
            }
            else
            {
                existing.UpdateBasics(request.DisplayName, request.Sex, request.DateOfBirth, request.BloodType);
                await _repo.SaveChangesAsync(ct);
                return existing.Id;
            }
        }
    }
}
