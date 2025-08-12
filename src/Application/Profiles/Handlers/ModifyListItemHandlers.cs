using Application.Abstractions;
using Application.Profiles.Commands;
using MediatR;

namespace Application.Profiles.Handlers
{
    public sealed class AddChronicConditionHandler : IRequestHandler<AddChronicConditionCommand, Unit>
    {
        private readonly IUserContext _ctx; private readonly IUserProfileRepository _repo;
        public AddChronicConditionHandler(IUserContext c, IUserProfileRepository r) { _ctx = c; _repo = r; }

        public async Task<Unit> Handle(AddChronicConditionCommand req, CancellationToken ct)
        {
            var p = await _repo.GetByUserIdAsync(_ctx.UserId, ct) ?? throw new InvalidOperationException("Profile not found");
            p.AddChronicCondition(req.Name, req.Notes);
            await _repo.SaveChangesAsync(ct);
            return Unit.Value;
        }
    }

    public sealed class RemoveChronicConditionHandler : IRequestHandler<RemoveChronicConditionCommand, Unit>
    {
        private readonly IUserContext _ctx; private readonly IUserProfileRepository _repo;
        public RemoveChronicConditionHandler(IUserContext c, IUserProfileRepository r) { _ctx = c; _repo = r; }

        public async Task<Unit> Handle(RemoveChronicConditionCommand req, CancellationToken ct)
        {
            var p = await _repo.GetByUserIdAsync(_ctx.UserId, ct) ?? throw new InvalidOperationException("Profile not found");
            p.RemoveChronicCondition(req.Name);
            await _repo.SaveChangesAsync(ct);
            return Unit.Value;
        }
    }

    public sealed class AddAllergyHandler : IRequestHandler<AddAllergyCommand, Unit>
    {
        private readonly IUserContext _ctx; private readonly IUserProfileRepository _repo;
        public AddAllergyHandler(IUserContext c, IUserProfileRepository r) { _ctx = c; _repo = r; }

        public async Task<Unit> Handle(AddAllergyCommand req, CancellationToken ct)
        {
            var p = await _repo.GetByUserIdAsync(_ctx.UserId, ct) ?? throw new InvalidOperationException("Profile not found");
            p.AddAllergy(req.Name, req.Severity);
            await _repo.SaveChangesAsync(ct);
            return Unit.Value;
        }
    }

    public sealed class RemoveAllergyHandler : IRequestHandler<RemoveAllergyCommand, Unit>
    {
        private readonly IUserContext _ctx; private readonly IUserProfileRepository _repo;
        public RemoveAllergyHandler(IUserContext c, IUserProfileRepository r) { _ctx = c; _repo = r; }

        public async Task<Unit> Handle(RemoveAllergyCommand req, CancellationToken ct)
        {
            var p = await _repo.GetByUserIdAsync(_ctx.UserId, ct) ?? throw new InvalidOperationException("Profile not found");
            p.RemoveAllergy(req.Name);
            await _repo.SaveChangesAsync(ct);
            return Unit.Value;
        }
    }
}
