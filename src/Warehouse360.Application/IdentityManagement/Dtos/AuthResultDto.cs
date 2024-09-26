namespace Warehouse360.Application.IdentityManagement.Dtos;

public class AuthResultDto
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }

    public AuthResultDto() {}

    public AuthResultDto(string accessToken, string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }
}