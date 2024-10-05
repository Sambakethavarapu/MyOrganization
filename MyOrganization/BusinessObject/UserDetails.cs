using MyOrganization.DataAccessObject;
using MyOrganization.DataModel;

namespace MyOrganization.BusinessObject
{
    public class UserDetails
    {
        SqlUserDAO dao = new SqlUserDAO();
        public async Task<Users> LoginUser(string userName, string password)
        {
            try
            {
                Users lstuser = new Users();
                lstuser = await dao.LoginUser(userName, password);
                return lstuser;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<List<EmployeeDetails>> GetAllEmployeeDetails()
        {
            try
            {
                List< EmployeeDetails> lstEmployee = new List< EmployeeDetails>();
                lstEmployee = await dao.GetAllEmployeeDetails();
                return lstEmployee;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<EmployeeDetails>> SaveEmployeeData(EmployeeDetails employeeDetails)
        {
            try
            {
                List<EmployeeDetails> lstEmployee = new List<EmployeeDetails>();
                lstEmployee = await dao.DeleteEmployeeDetailsById(employeeDetails.EmployeeId);
                return lstEmployee;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<List<EmployeeDetails>> DeleteEmployeeDetailsById(int EmployeeId)
        {
            try
            {
                List<EmployeeDetails> lstEmployee = new List<EmployeeDetails>();
                lstEmployee = await dao.DeleteEmployeeDetailsById(EmployeeId);
                return lstEmployee;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<List<EmployeeDetails>> UpdateEmployeeDataById(int EmployeeId,string FirstName,string LastName,string SurName,string Email)
        {
            try
            {
                List<EmployeeDetails> lstEmployee = new List<EmployeeDetails>();
                lstEmployee = await dao.UpdateEmployeeDataById(EmployeeId, FirstName, LastName, SurName, Email);
                return lstEmployee;
            }
            catch (Exception)
            {

                throw;
            }
        }
        
    }
}
