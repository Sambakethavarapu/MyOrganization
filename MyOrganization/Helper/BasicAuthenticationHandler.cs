using Azure.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text;
using MyOrganization.BusinessObject;

namespace MyOrganization.Helper
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
      
        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
            //this.context = context;
        }

        protected async override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("No header found");
            }
            var headervalue = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            if (headervalue != null)
            {
                var bytes = Convert.FromBase64String(headervalue.Parameter);
                string credentials = Encoding.UTF8.GetString(bytes);
                string[] array = credentials.Split(":");
                string username = array[0];
                string password = array[1]; 
                UserDetails userDetails = new UserDetails();
                var user = await userDetails.LoginUser(username, password);
                //await this.context.TblUsers.FirstOrDefaultAsync(item => item.Username == username && item.Password == password);
                if (user != null)
                {
                    var claim = new[] { new Claim(ClaimTypes.Name, user.UserName) };
                    var identity = new ClaimsIdentity(claim, Scheme.Name);
                    var principal = new ClaimsPrincipal(identity);
                    var ticket = new AuthenticationTicket(principal, Scheme.Name);
                    return AuthenticateResult.Success(ticket);
                }
                else
                {
                    return AuthenticateResult.Fail("UnAutorized");
                }
            }
            else
            {
                return AuthenticateResult.Fail("Empty header");
            }
        }
    }
}
