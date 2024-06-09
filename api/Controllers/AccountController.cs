using api.Dtos.Account;
using api.Interfaces;
using api.Models;
using api.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
  [Route("api/accounts")]
  public class AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager) : ControllerBase
  {
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly ITokenService _tokenService = tokenService;
    private readonly SignInManager<AppUser> _signInManager = signInManager;

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
      if (!ModelState.IsValid) return BadRequest(ModelState);
      var user = _userManager.Users.FirstOrDefault(u => u.UserName == loginDto.Username.ToLower());
      if (user == null) return Unauthorized("Invalid Username!");
      var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
      if (!result.Succeeded) return Unauthorized("Username or password invalid!");
      return Ok(
        new NewUserDto
        {
          Username = user.UserName,
          Email = user.Email,
          Token = _tokenService.CreateToken(user)
        }
      );
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
      await _signInManager.SignOutAsync();
      return Ok(); 
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
      try
      {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var appUser = new AppUser
        {
          UserName = registerDto.Username,
          Email = registerDto.Email
        };

        var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);
        if (createdUser.Succeeded)
        {
          var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
          if (roleResult.Succeeded)
          {
            return Ok(
                new NewUserDto
                {
                  Username = appUser.UserName,
                  Email = appUser.Email,
                  Token = _tokenService.CreateToken(appUser)
                }
            );
          }
          else
          {
            return StatusCode(500, roleResult.Errors);
          }
        }
        else
        {
          return StatusCode(500, createdUser.Errors);
        }
      }
      catch (Exception e)
      {
        return StatusCode(500, e);
      }
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetUsers()
    {
      var appUsers = _userManager.Users.AsQueryable();
      var users = await appUsers.Select(s => s.ToUserDto()).ToListAsync();
      return Ok(users);
    }
  }
}