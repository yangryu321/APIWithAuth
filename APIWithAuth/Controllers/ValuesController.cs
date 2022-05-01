using APIWithAuth.DataContext;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ValuesController : ControllerBase
    {
        //private const string AuthSchemes = JwtBearerDefaults.AuthenticationScheme;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ApplicationContext applicationContext;

        public ValuesController(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationContext applicationContext)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.applicationContext = applicationContext;
        }

        [HttpGet("getFruit")]
        [AllowAnonymous]
        public IEnumerable<string> Getfruit()
        {
            return new string[] { "Peach" };
        }


        [HttpGet("getFruits")]
        public IEnumerable<string> Getfruits()
        {
            return new string[] { "Apple", "Banana" };
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
            
            return BadRequest(result.Errors);
                
            
        }


        //TODO 
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody] RegisterModel model)
        {
            var user = applicationContext.Users.FirstOrDefault(x => x.Email == model.Email);


            if (user == null)
            {
                return NotFound();

            }
            else
            {
                var signInResult = await signInManager.CheckPasswordSignInAsync(user, model.Password,false);
                if(signInResult.Succeeded)
                {
                    

                    //if user is registed then return a JWT token
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes("My_secret_key_HAHAHAHAHHAHAHAHAHAHAHA");
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.Name, model.Email)
                        }),
                        Expires = DateTime.UtcNow.AddDays(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var tokenstring = tokenHandler.WriteToken(token);
                    
                    return Ok(new { Token = tokenstring });


                }
                else
                {
                    return Ok("Try again");
                }

            }
         
        }


    }
}
