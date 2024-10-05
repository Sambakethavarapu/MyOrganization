using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MyOrganization;
using MyOrganization.Controllers;
using MyOrganization.BusinessObject;
using MyOrganization.DataModel;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        EmployeeController employeeController = new EmployeeController();
        [TestMethod]
        public void DeleteEmployeeDetailsById()
        {
            try
            {
                
                employeeController.DeleteEmployeeDetailsById(1);
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        [TestMethod]
        public void UpdateEmployeeDataById(int Id)
        {
            try
            {
                EmployeeDetails employeeDetails = new EmployeeDetails();
                employeeDetails.PhoneNumber = 965656565;
                employeeDetails.FirstName = "Samba";
                employeeDetails.LastName = "Siva";
                employeeDetails.Email = "sambasiva@gmail.com";
                employeeDetails.SurName = "K";
                employeeDetails.DOB = DateTime.Now;
                employeeDetails.DOJ = DateTime.Now;
                employeeController.UpdateEmployeeDataById(employeeDetails);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [TestMethod]
        public void GetAllEmployeeDetails()
        {
            try
            {
                employeeController.GetAllEmployeeDetails();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}