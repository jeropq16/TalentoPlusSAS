using System.IdentityModel.Tokens.Jwt;
using System.Text;
using _1_Application.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace TalentoPlus.Api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class AuthController  : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;

    public AuthController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null) return Unauthorized();

        var valid = await _userManager.CheckPasswordAsync(user, model.Password);
        if (!valid) return Unauthorized();

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("5F9C1F1A9E3B4F67A2C8D7E1F5A9B3C4D6E8F1A2B3C4D5E6A7F8A9B0C1D2E3F4"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            signingCredentials: creds,
            expires: DateTime.UtcNow.AddHours(5)
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return Ok(new { token = jwt });
    }
    
    
    [HttpPost("create-admin")]
    public async Task<IActionResult> CreateAdmin()
    {
        var email = "admin@admin.com";
        var pass = "Admin123$";

        var exists = await _userManager.FindByEmailAsync(email);
        if (exists != null)
            return Ok("Admin already exists");

        var user = new IdentityUser
        {
            UserName = email,
            Email = email
        };

        var result = await _userManager.CreateAsync(user, pass);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "Admin");
            return Ok("Admin created");
        }

        return BadRequest(result.Errors);
    }

}