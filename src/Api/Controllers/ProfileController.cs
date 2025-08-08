using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfileController : ControllerBase
{
    private readonly ProfileService _profileService;

    public ProfileController(ProfileService profileService)
    {
        _profileService = profileService;
    }

    [HttpGet("me")]
    public IActionResult GetMyProfile()
    {
        // ตัวอย่างใช้ username คงที่ก่อน
        var profile = _profileService.GetProfile("john.doe");

        if (profile == null)
            return NotFound();

        return Ok(profile);
    }
}
