using Application.Abstractions;
using Application.Profiles.Commands;
using MediatR;

namespace Application.Profiles.Handlers
{
    public sealed class UpdateMeasurementsHandler : IRequestHandler<UpdateMeasurementsCommand>
    {
        private readonly IUserContext _ctx;
        private readonly IUserProfileRepository _repo;

        public UpdateMeasurementsHandler(IUserContext ctx, IUserProfileRepository repo)
        {
            _ctx = ctx;
            _repo = repo;
        }

        public async Task Handle(UpdateMeasurementsCommand request, CancellationToken ct)
        {
            var profile = await _repo.GetByUserIdAsync(_ctx.UserId, ct)
                ?? throw new InvalidOperationException("Profile not found");

            profile.UpdateMeasurements(request.HeightCm, request.WeightKg);
            await _repo.SaveChangesAsync(ct);
        }
    }
}
