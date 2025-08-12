using Application.Abstractions;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public sealed class UserProfileRepository : IUserProfileRepository
    {
        private readonly UserProfileDbContext _db;
        public UserProfileRepository(UserProfileDbContext db) => _db = db;

        public Task<UserProfileAggregate?> GetByUserIdAsync(Guid userId, CancellationToken ct)
            => _db.Profiles
                .Include(p => p.ChronicConditions)
                .Include(p => p.Allergies)
                .FirstOrDefaultAsync(p => p.UserId == userId, ct);

        public async Task AddAsync(UserProfileAggregate profile, CancellationToken ct)
            => await _db.Profiles.AddAsync(profile, ct);

        public Task SaveChangesAsync(CancellationToken ct) => _db.SaveChangesAsync(ct);
    }
}
