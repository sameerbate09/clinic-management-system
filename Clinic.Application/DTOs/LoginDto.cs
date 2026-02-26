using System.ComponentModel.DataAnnotations;

namespace Clinic.Application.DTOs;

public class LoginRequestDto
{
    public string Username { get; set; }

    public string Password { get; set; }
}

public class LoginResponseDto
{
    public string Token { get; }

    public LoginResponseDto(string token)
    {
        Token = token;
    }
}
