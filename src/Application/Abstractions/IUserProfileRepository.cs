using Domain.Entities;

namespace Application.Abstractions
{
    public interface IUserProfileRepository
    {
        Task<UserProfileAggregate?> GetByUserIdAsync(Guid userId, CancellationToken ct);
        Task AddAsync(UserProfileAggregate profile, CancellationToken ct);
        Task SaveChangesAsync(CancellationToken ct);
    }
}
