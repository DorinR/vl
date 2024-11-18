using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using webapitest.Controllers.Models;
using webapitest.Repository.Interfaces;

namespace webapitest.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userTdg;

    public UserController(IUserRepository userTdg)
    {
        _userTdg = userTdg;
    }

    [HttpPost]
    [Route("Register")]
    public async Task<ActionResult> Register([FromBody] CreateUserModel model)
    {
        // check if user exists already
        var existingUser = await _userTdg.FindUser(model.Email);

        // if yes, throw
        if (existingUser is not null) return BadRequest("user already exists");

        // otherwise create user
        var userCreationResult = await _userTdg.Register(model);

        // Get the JWT token
        var tokenString = GetTokenString(model.Email, userCreationResult);

        // return new created user's Guid
        return Ok(new
        {
            Token = tokenString, UserId = userCreationResult
        });
    }

    [HttpPost]
    [Route("Login")]
    public async Task<ActionResult> Login([FromBody] LoginModel model)
    {
        //retrieve user with that email
        var user = await _userTdg.FindUser(model.Email);

        //if it doesn't exist throw
        if (user is null) return BadRequest("Wrong email or password, or user doesn't exist");

        //else validate password is good and send token
        if (model.Password != user.Password) return BadRequest("Wrong password");

        var tokenString = GetTokenString(model.Email, user.Id.ToString());

        return Ok(new
        {
            Token = tokenString,
            UserId = user.Id
        });
    }

    private static string GetTokenString(string email, string userId)
    {
        // Replace these values with your actual secret key and issuer
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("hello-there-friend-this-is-the-key"));
        var issuer = "my-secret-issuer";

        // Define claims for the user (you can customize this based on your application)
        var claims = new[]
        {
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, "user"),
            new Claim(ClaimTypes.NameIdentifier, userId)
        };

        // Create a new JWT token
        var token = new JwtSecurityToken(
            issuer,
            issuer,
            claims,
            expires: DateTime.UtcNow.AddHours(1), // Token expiration time
            signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256)
        );

        // Generate the JWT token
        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenString;
    }
}