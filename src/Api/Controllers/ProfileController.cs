using Application.Profiles;
using Application.Profiles.Commands;
using Application.Profiles.Queries;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/profiles")]
public sealed class ProfilesController : ControllerBase
{
    private readonly IMediator _mediator;
    public ProfilesController(IMediator mediator) => _mediator = mediator;

    // GET api/profiles/me
    [HttpGet("me")]
    public async Task<ActionResult<ProfileDto>> GetMe(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetMyProfileQuery(), ct);
        return Ok(result);
    }

    // POST api/profiles/upsert
    public sealed record UpsertProfileRequest(string? DisplayName, Sex Sex, DateOnly? DateOfBirth, string? BloodType);
    [HttpPost("upsert")]
    public async Task<ActionResult<Guid>> Upsert([FromBody] UpsertProfileRequest req, CancellationToken ct)
    {
        var id = await _mediator.Send(new UpsertProfileCommand(req.DisplayName, req.Sex, req.DateOfBirth, req.BloodType), ct);
        return Ok(id);
    }

    // PATCH api/profiles/measurements
    public sealed record UpdateMeasurementsRequest(int? HeightCm, decimal? WeightKg);
    [HttpPatch("measurements")]
    public async Task<IActionResult> UpdateMeasurements([FromBody] UpdateMeasurementsRequest req, CancellationToken ct)
    {
        await _mediator.Send(new UpdateMeasurementsCommand(req.HeightCm, req.WeightKg), ct);
        return NoContent();
    }

    // POST api/profiles/conditions
    public sealed record NamedRequest(string Name, string? NotesOrSeverity);
    [HttpPost("conditions")]
    public async Task<IActionResult> AddCondition([FromBody] NamedRequest req, CancellationToken ct)
    {
        await _mediator.Send(new AddChronicConditionCommand(req.Name, req.NotesOrSeverity), ct);
        return NoContent();
    }

    [HttpDelete("conditions/{name}")]
    public async Task<IActionResult> RemoveCondition([FromRoute] string name, CancellationToken ct)
    {
        await _mediator.Send(new RemoveChronicConditionCommand(name), ct);
        return NoContent();
    }

    // POST api/profiles/allergies
    [HttpPost("allergies")]
    public async Task<IActionResult> AddAllergy([FromBody] NamedRequest req, CancellationToken ct)
    {
        await _mediator.Send(new AddAllergyCommand(req.Name, req.NotesOrSeverity), ct);
        return NoContent();
    }

    [HttpDelete("allergies/{name}")]
    public async Task<IActionResult> RemoveAllergy([FromRoute] string name, CancellationToken ct)
    {
        await _mediator.Send(new RemoveAllergyCommand(name), ct);
        return NoContent();
    }
}