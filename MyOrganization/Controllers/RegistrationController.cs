using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyOrganization.BusinessObject;
using MyOrganization.DataModel;

namespace MyOrganization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        public RegistrationController() { }

        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser([FromBody] Users registerUsers)
        {
            try
            {
                UserDetails userDetails = new UserDetails();
                var user = await userDetails.RegisterUserDetails(registerUsers);
                if (user != null)
                {
                    return Ok("User Registered Successfully");
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception)
            {

                throw;
            }
           
        }
    }
}
