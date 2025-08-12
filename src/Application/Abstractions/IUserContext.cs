namespace Application.Abstractions
{
    public interface IUserContext
    {
        Guid UserId { get; }         // ดึงมาจาก headers ของ gateway
        string? Email { get; }
        IReadOnlyList<string> Roles { get; }
    }
}
