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
                return lstuser = await dao.LoginUser(userName, password);

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
                List<EmployeeDetails> lstEmployee = new List<EmployeeDetails>();
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
                lstEmployee = await dao.SaveEmployeeData(employeeDetails);
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
                return lstEmployee = await dao.DeleteEmployeeDetailsById(EmployeeId);

            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<List<EmployeeDetails>> UpdateEmployeeDataById(EmployeeDetails employeeDetails)
        {
            try
            {
                List<EmployeeDetails> lstEmployee = new List<EmployeeDetails>();
                return lstEmployee = await dao.UpdateEmployeeDataById(employeeDetails);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> RegisterComplaints(ComplaintDetails complaintDetails)
        {
            try
            {
                return await dao.RegisterComplaints(complaintDetails);

            }
            catch { }
            return false;

        }

    }
}
