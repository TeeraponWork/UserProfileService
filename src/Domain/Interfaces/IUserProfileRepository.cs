using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserProfileRepository
    {
        UserProfile? GetProfileByUsername(string username);
    }
}
