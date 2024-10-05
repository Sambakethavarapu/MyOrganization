using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyOrganization.BusinessObject;
using MyOrganization.DataModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MyOrganization.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        //private readonly JwtSettings jwtSettings;
        //public AuthorizeController( IOptions<JwtSettings> options)
        //{
        //    this.jwtSettings = options.Value;
        //}

        [HttpPost("GenerateToken")]
        public async Task<IActionResult> GenerateToken(string UserName, string Password)
        {
            UserDetails userDetails = new UserDetails();
            var user = await userDetails.LoginUser(UserName, Password);
            //var user = await this.context.TblUsers.FirstOrDefaultAsync(item => item.Username == userCred.username && item.Password == userCred.password);
            if (user != null)
            {
                var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
                var securitykey = configuration.GetSection("JwtSettings:securitykey").Value;
                //generate token
                var tokenhandler = new JwtSecurityTokenHandler();
                var tokenkey = Encoding.UTF8.GetBytes(securitykey);
                var tokendesc = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Email, user.Email),
                    }),
                    Expires = DateTime.UtcNow.AddDays(14),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenhandler.CreateToken(tokendesc);
                var finaltoken = tokenhandler.WriteToken(token);
                ValidateToken(finaltoken);
                return Ok(finaltoken);
                //return Ok(new TokenResponse() { Token = finaltoken, RefreshToken = await this.refresh.GenerateToken(userCred.username) });

            }
            else
            {
                return Unauthorized();
            }

        }
        [Route("ValidateToken")]
        [HttpGet]
        public static string ValidateToken(string token)
        {
            string username = null;
            ClaimsPrincipal principal = GetPrincipal(token);
            if (principal == null)
                return null;
            ClaimsIdentity identity = null;
            try
            {
                identity = (ClaimsIdentity)principal.Identity;
            }
            catch (NullReferenceException)
            {
                return null;
            }
            Claim usernameClaim = identity.FindFirst(ClaimTypes.Name);
            username = usernameClaim.Value;
            return username;
        }
        public static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
                if (jwtToken == null)
                    return null;
                //ASCIIEncoding.GetEncoding(jwtToken.SigningCredentials?.ToString()); 
                byte[] key = Convert.FromBase64String(jwtToken.ToString());
                TokenValidationParameters parameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
                SecurityToken securityToken;
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token,
                      parameters, out securityToken);
                return principal;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
