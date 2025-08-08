using Application.DTOs;
using Domain.Interfaces;

namespace Application.Services
{
    public class ProfileService
    {
        private readonly IUserProfileRepository _repository;

        public ProfileService(IUserProfileRepository repository)
        {
            _repository = repository;
        }

        public UserProfileDto? GetProfile(string username)
        {
            var entity = _repository.GetProfileByUsername(username);
            if (entity == null) return null;

            return new UserProfileDto
            {
                Username = entity.Username,
                Email = entity.Email,
                DisplayName = entity.DisplayName
            };
        }
    }
}
