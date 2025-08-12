using MediatR;

namespace Application.Profiles.Queries
{
    public sealed record GetMyProfileQuery : IRequest<ProfileDto>;
}
