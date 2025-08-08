using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {
        public UserProfile? GetProfileByUsername(string username)
        {
            // Mock data
            if (username == "john.doe")
            {
                return new UserProfile
                {
                    Username = "john.doe",
                    Email = "john.doe@example.com",
                    DisplayName = "John Doe"
                };
            }
            return null;
        }
    }
}
