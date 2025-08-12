using Application.Abstractions;

namespace Api.Security
{
    public class MockUserContext : IUserContext
    {
        public Guid UserId { get; }
        public string? Email { get; }
        public IReadOnlyList<string> Roles { get; }

        public MockUserContext(Guid userId, string? email, IReadOnlyList<string> roles)
        {
            UserId = userId;
            Email = email;
            Roles = roles;
        }
    }
}
