using Clinic.Application.Interfaces.Repositories;
using Clinic.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Clinic.Application.Services;

public class AuthService : IAuthService
{
    private readonly IAdminRepository _adminRepository;
    private readonly IConfiguration _configuration;

    public AuthService(IAdminRepository adminRepository,
                       IConfiguration configuration)
    {
        _adminRepository = adminRepository;
        _configuration = configuration;
    }

    public async Task<string?> LoginAsync(string username, string password)
    {
        var admin = await _adminRepository.GetByUsernameAsync(username);
        if (admin == null)
            return null;
        //string hash = "$2a$11$k8ij6epc.IJdSksV6wy2Eey9tSYySNMmBoaRka2gnAadDOSYqO4LO";
        bool isValid = BCrypt.Net.BCrypt.Verify(password, admin.PasswordHash);
        if (!isValid)
            return null;

        return GenerateJwtToken(admin);
    }

    private string GenerateJwtToken(Admin admin)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, admin.AdminId.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, admin.Username)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
