
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;
using TestRestApi.Data.Models;
using TestRestApi.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace TestRestApi.Controllers;
[Route("api/[controller]")]
[ApiController]

[Authorize]   //using Microsoft.AspNetCore.Authorization;
public class AccountController : ControllerBase
{
    public AccountController(UserManager<AppUser> userManager , IConfiguration configuration)
    {
        _userManager = userManager;
        this.configuration = configuration;
    }
    private readonly UserManager<AppUser> _userManager;
    private readonly IConfiguration configuration;

    [HttpPost("[Action]")]
    public async Task<IActionResult> Register(dtoNewUser user)
    {
        if (ModelState.IsValid)
        {
            AppUser appUser = new()
            {
                UserName = user.UserName,
                Email = user.Email

            };
            IdentityResult result = await _userManager.CreateAsync(appUser, user.Password);
            if (result.Succeeded)
            {
                return Ok("Success");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
        }
        return BadRequest(ModelState);
    }

    [HttpPost("[Action]")]
    public async Task<IActionResult> Login(dtoLogin login)
    {
        if (ModelState.IsValid)
        {
            AppUser? user = await _userManager.FindByNameAsync(login.userName);
            if (user != null)
            {
                if (await _userManager.CheckPasswordAsync(user, login.Password))
                {
                    var claims = new List<Claim>();
                    //claims.Add(new Claim("TokenNo","75"));
                    claims.Add(new Claim(ClaimTypes.Name , user.UserName));
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                    claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                    var roles = await _userManager.GetRolesAsync(user);
                    foreach (var role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
                    }
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]));
                    var sc = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                        claims: claims,
                        issuer: configuration["JWT:Issuer"],
                        audience: configuration["JWT:Audience"],
                        expires: DateTime.Now.AddHours(1),
                        signingCredentials: sc

                        );
                    var _token = new
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo,

                    };
                    return Ok(_token);
                }
                else 
                { 
                    return Unauthorized();
                }

            }
            else
            {
                ModelState.AddModelError("", "User name is invalid");


            }
          
        }
        return BadRequest(ModelState);
    }
}
