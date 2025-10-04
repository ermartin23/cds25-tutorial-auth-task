namespace Api.Models.Dtos.Responses;

public record RegisterResponse(string UserName);

public record LoginResponse(string Jwt);