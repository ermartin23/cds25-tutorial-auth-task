using Api.Models.Dtos.Requests;
using Api.Models.Dtos.Responses;
using Microsoft.AspNetCore.Mvc;
using Api.Services;


namespace Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase

{
    [HttpPost]
    [Route("login")]
    public  Task<LoginResponse> Login([FromBody] LoginRequest request)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    [Route("register")]
    public Task<RegisterResponse> Register([FromBody] RegisterRequest request)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    [Route("logout")]
    public  Task<IResult> Logout()
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    [Route("userinfo")]
    public Task<AuthUserInfo> UserInfo()
    {
        throw new NotImplementedException();
    }
}
