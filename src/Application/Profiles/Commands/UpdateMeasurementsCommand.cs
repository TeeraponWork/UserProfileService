using MediatR;

namespace Application.Profiles.Commands
{
    public sealed record UpdateMeasurementsCommand(int? HeightCm, decimal? WeightKg) : IRequest;
}
