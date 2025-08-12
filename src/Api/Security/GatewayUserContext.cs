using Application.Abstractions;

namespace Api.Security
{
    public sealed class GatewayUserContext : IUserContext
    {
        private Guid _userId;
        private string? _email;
        private List<string> _roles = new();

        public Guid UserId => _userId;
        public string? Email => _email;
        public IReadOnlyList<string> Roles => _roles;

        public void Set(Guid uid, string? email, string? rolesCsv)
        {
            //_userId = uid;
            _userId =new Guid("C80D8B6C-04F1-4071-994A-09A30F640D2B");
            _email = string.IsNullOrWhiteSpace(email) ? null : email.Trim();
            _roles = string.IsNullOrWhiteSpace(rolesCsv)
                ? new List<string>()
                : rolesCsv.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList();
        }
    }
}
