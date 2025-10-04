using DataAccess.Entities;

namespace Api.Models.Dtos.Responses;

public record AuthUserInfo(
    string Id,
    string UserName,
    Role Role
);