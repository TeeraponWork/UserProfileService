using MediatR;

namespace Application.Profiles.Commands
{
    public sealed record AddChronicConditionCommand(string Name, string? Notes) : IRequest<Unit>;
    public sealed record RemoveChronicConditionCommand(string Name) : IRequest<Unit>;
    public sealed record AddAllergyCommand(string Name, string? Severity) : IRequest<Unit>;
    public sealed record RemoveAllergyCommand(string Name) : IRequest<Unit>;
}
