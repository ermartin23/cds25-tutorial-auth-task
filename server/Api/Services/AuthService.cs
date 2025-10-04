using System.Linq;
using Api.Models.Dtos.Requests;
using Api.Models.Dtos.Responses;
using DataAccess.Entities;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Api.Etc;


namespace Api.Services;

public class AuthService : IAuthService
{
    private readonly ILogger<AuthService> _logger;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IRepository<User> _userRepository;

    public AuthService(
        ILogger<AuthService> logger,
        IPasswordHasher<User> passwordHasher,
        IRepository<User> userRepository)
    {
        _logger = logger;
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
    }

    public AuthUserInfo Authenticate(LoginRequest request)
    {
        // Lookup after emil  
        var user = _userRepository.Query().FirstOrDefault(u => u.Email == request.Email);

        if (user is null)
        {
            _logger.LogWarning("Authentication failed: user not found");
            throw new AuthenticationError();

        }

        // Verify password
        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
        if (result == PasswordVerificationResult.Success)
        {
            return new AuthUserInfo(user.Id, user.UserName, user.Role);
        }

        _logger.LogWarning("Authentication failed: invalid password");
        throw new System.Security.Authentication.AuthenticationException("Invalid login");
    }

    public async Task<AuthUserInfo> Register(RegisterRequest request)
    {
        // Prevent duplicate   
        if (_userRepository.Query().Any(u => u.Email == request.Email))
        {
            throw new System.ComponentModel.DataAnnotations.ValidationException("Email already in use");
        }

        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            UserName = request.UserName,
            Email = request.Email,
            Role = Role.Reader,
            PasswordHash = _passwordHasher.HashPassword(null!, request.Password)
        };

        await _userRepository.Add(user);

        return new AuthUserInfo(user.Id, user.UserName, user.Role);
    }
}
