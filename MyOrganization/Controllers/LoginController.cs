using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyOrganization.BusinessObject;
using MyOrganization.DataModel;

namespace MyOrganization.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<Users> Login(string UserName, string Password)
        {
            try
            {
                UserDetails userDetails = new UserDetails();
                var Users = await userDetails.LoginUser(UserName, Password);
                if (Users != null)
                {
                    return Users;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
