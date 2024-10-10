using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyOrganization.BusinessObject;
using MyOrganization.DataModel;

namespace MyOrganization.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<Users> Login(string userName, string password)
        {
            try
            {
                UserDetails userDetails = new UserDetails();
                var Users = await userDetails.LoginUser(userName, password);
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

        [HttpPost]
        [AllowAnonymous]
        public async Task<bool> RegisterComplaints([FromBody] ComplaintDetails complaintdetails)
        {
            try {
                UserDetails userDetails = new UserDetails();
                bool val = await userDetails.RegisterComplaints(complaintdetails);
                return val;
            }
            catch (Exception) { throw; }
        }
    }
}
