using MyOrganization.BusinessObject;
using MyOrganization.Contstants;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using MyOrganization.DataModel;
using Microsoft.AspNetCore.Mvc;

namespace MyOrganization.DataAccessObject
{
    public class SqlUserDAO
    {
        string connectionString = string.Empty;
        public SqlUserDAO()
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            connectionString = configuration.GetSection("ConnectionStrings:apiconnectionstring").Value;
        }
        public async Task<Users> LoginUser(string userName, string password)
        {
            try
            {
                Users users = new Users();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(SqlContstants.ORG_User_Login, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@username", userName);
                        command.Parameters.AddWithValue("@password", password);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                users.UserId = Convert.ToInt32(reader["UserId"]);
                                users.UserName = reader["UserName"].ToString();
                                users.IsActive = Convert.ToBoolean(reader["IsActive"].ToString());
                                users.Email = reader["Email"].ToString();
                            }
                        }
                        if (connection.State != ConnectionState.Closed)
                            connection.Close();
                    }
                    return users.UserId > 0 ? users : null;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return null;
        }

        public async Task<List<EmployeeDetails>> GetAllEmployeeDetails()
        {
            try
            {
                List<EmployeeDetails> lstEmployee = new List<EmployeeDetails>();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(SqlContstants.ORG_GET_ALL_EmployeeDetails, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                EmployeeDetails employee = new EmployeeDetails();
                                employee.EmployeeId = Convert.ToInt32(reader["EmployeeId"]);
                                employee.FirstName = reader["FirstName"].ToString();
                                employee.LastName = reader["LastName"].ToString();
                                employee.Email = reader["Email"].ToString();
                                employee.PhoneNumber = Convert.ToInt32(reader["PhoneNumber"]);
                                lstEmployee.Add(employee);
                            }
                        }
                        if (connection.State != ConnectionState.Closed)
                            connection.Close();
                    }
                    return lstEmployee;
                }
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
                int result;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(SqlContstants.ORG_DEL_EmployeeDetails_BYID, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@empId", EmployeeId);
                        result = command.ExecuteNonQuery();
                    }
                    if (result > 0)
                    {
                        return lstEmployee = await this.GetAllEmployeeDetails();
                    }
                    if (connection.State != ConnectionState.Closed)
                        connection.Close();
                }

            }
            catch (Exception)
            {
                throw;
            }
            return null;
        }
        public async Task<List<EmployeeDetails>> UpdateEmployeeDataById(int EmployeeId, string FirstName, string LastName, string SurName, string Email)
        {
            try
            {
                List<EmployeeDetails> lstEmployee = new List<EmployeeDetails>();
                int result;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(SqlContstants.ORG_UPDATE_EmployeeDetails_BYID, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@empId", EmployeeId);
                        command.Parameters.AddWithValue("@FirstName", FirstName);
                        command.Parameters.AddWithValue("@LastName", LastName);
                        command.Parameters.AddWithValue("@SurName", SurName);
                        command.Parameters.AddWithValue("@Email", Email);
                        result = command.ExecuteNonQuery();
                    }
                    if (result > 0)
                    {
                        return lstEmployee = await this.GetAllEmployeeDetails();
                    }

                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {

            }
            return null;
        }

        public async Task<List<EmployeeDetails>> SaveEmployeeData()
        {
            try
            {
                return await this.GetAllEmployeeDetails();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
