using Microsoft.AspNetCore.Mvc;
using NummyApi.Services.Abstract;
using NummyShared.DTOs;

namespace NummyApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        var result = await userService.LoginAsync(request);

        return result.Success ? Ok(result) : Unauthorized(result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
    {
        var result = await userService.RegisterAsync(request);

        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await userService.GetAsync(id);

        return result == null ? NotFound("User not found") : Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var users = await userService.GetAsync();
        return Ok(users);
    }
}