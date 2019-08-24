namespace MessagesAPI.Controllers
{
    using MessagesAPI.Data;
    using MessagesAPI.Domain;
    using MessagesAPI.Jwt;
    using MessagesAPI.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MessagesAPIDbContext context;
        private readonly JwtSettings jwtSettings;

        public UsersController(MessagesAPIDbContext context, IOptions<JwtSettings> jwtSettings)
        {
            this.context = context;
            this.jwtSettings = jwtSettings.Value;
        }

        [HttpPost(Name = "Login")]
        [Route("login")]
        public async Task<ActionResult> Login(UserCreateBindingModel model)
        {
            var currenUser = await this.context
                .Users
                .SingleOrDefaultAsync(user => user.Username == model.Username && user.Password == model.Password);

            if (currenUser == null)
            {
                return this.BadRequest("Username or password is invalid.");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.jwtSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, currenUser.Username)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return this.Ok(token);
        }

        [HttpPost(Name = "Register")]
        [Route("register")]
        public async Task<ActionResult> Register(UserCreateBindingModel model)
        {
            this.context.Users.Add(new User
            {
                Username = model.Username,
                Password = model.Password
            });

            await this.context.SaveChangesAsync();

            return this.Ok();
        }
    }
}
