using APIWithAuth.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APIWithAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize/*(AuthenticationSchemes = AuthSchemes)*/]
    public class ValuesController : ControllerBase
    {
        //private const string AuthSchemes = JwtBearerDefaults.AuthenticationScheme;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public ValuesController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }


        [HttpGet("getFruits")]
        public IEnumerable<string> Getfruits()
        {
            return new string[] { "value1", "value2" };
        }



        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<ActionResult> Register([FromBody] RegisterModel model)
        {
            var user = new User { UserName = model.Email, Email = model.Email, };
            var result = await userManager.CreateAsync(user,model.Password);

            if (result.Succeeded)
            {
                return Ok(new { Result = "Register Success" });
            }
            else
            {

                return BadRequest(result.Errors);
                
            }
        }


        //TODO 
        [HttpPost("Login")]
        [AllowAnonymous]
        public ActionResult Login([FromBody] RegisterModel user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");

            }
            else
            { 
                //if user is registed then return a JWT token
                if (user.Email == "123@gmail.com" && user.Password == "123456")
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes("My_secret_key_HAHAHAHAHHAHAHAHAHAHAHA");
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.Name, user.Email)
                        }),
                        Expires = DateTime.UtcNow.AddDays(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var tokenstring = tokenHandler.WriteToken(token);

                    return Ok(new { Token = tokenstring });
                }


            }

            return NotFound();

        }


    }
}
