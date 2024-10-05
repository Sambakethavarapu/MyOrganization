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
    public class EmployeeController : ControllerBase
    {

        [HttpGet]
        public async Task<List<EmployeeDetails>> GetAllEmployeeDetails()
        {
            try
            {
                List<EmployeeDetails> employeeDetails = new List<EmployeeDetails>();
                UserDetails userDetails = new UserDetails();
                employeeDetails = await userDetails.GetAllEmployeeDetails();
                if (userDetails != null) { return employeeDetails; }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return null;
        }
        [HttpPost]
        public async Task<List<EmployeeDetails>> SaveEmployeeData([FromBody] EmployeeDetails employeeDetails)
        {
            try
            {
                EmployeeDetails employeeDetails1 = new EmployeeDetails();
                employeeDetails1.FirstName = employeeDetails.FirstName;
                employeeDetails1.FirstName = employeeDetails.LastName;
                employeeDetails1.SurName = employeeDetails.SurName;
                employeeDetails1.PhoneNumber = employeeDetails.PhoneNumber;
                employeeDetails1.Email = employeeDetails.Email;
                employeeDetails1.DOB = employeeDetails.DOB;
                employeeDetails1.DOJ = employeeDetails.DOJ;
                List<EmployeeDetails> empDetails = new List<EmployeeDetails>();
                UserDetails userDetails = new UserDetails();
                empDetails = await userDetails.SaveEmployeeData(employeeDetails1);
                return empDetails;
            }
            catch (Exception)
            {

                throw;
            }
            //return Ok(employeeDetails);
        }

        [Route("UpdateEmployeeDataById")]
        [HttpPost]
        public async Task<List<EmployeeDetails>> UpdateEmployeeDataById([FromBody] EmployeeDetails employeeDetails)
        {
            try
            {
                int Employeeid = employeeDetails.EmployeeId;
                string FirstName = employeeDetails.FirstName;
                string LastName = employeeDetails.LastName;
                string Email = employeeDetails.Email;
                string SurName = employeeDetails.SurName;
                List<EmployeeDetails> empDetails = new List<EmployeeDetails>();
                UserDetails userDetails = new UserDetails();
                empDetails = await userDetails.UpdateEmployeeDataById(Employeeid, FirstName, LastName, SurName, Email);
                return empDetails;
            }
            catch (Exception)
            {

                throw;
            }

        }

        [Route("DeleteEmployeeDetailsById")]
        [HttpPost]
        public async Task<List<EmployeeDetails>> DeleteEmployeeDetailsById(int employeeId)
        {
            try
            {
                List<EmployeeDetails> employeeDetails = new List<EmployeeDetails>();
                UserDetails userDetails = new UserDetails();
                employeeDetails = await userDetails.DeleteEmployeeDetailsById(employeeId);
                return employeeDetails;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
