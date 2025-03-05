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
        //public async Task<Users> LoginUser(string userName, string password)
        //{
        //    try
        //    {
        //        Users users = new Users();
        //        using (SqlConnection connection = new SqlConnection(connectionString))
        //        {
        //            connection.Open();
        //            using (SqlCommand command = new SqlCommand(SqlContstants.ORG_User_Login, connection))
        //            {
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.Parameters.AddWithValue("@username", userName);
        //                command.Parameters.AddWithValue("@password", password);
        //                using (SqlDataReader reader = command.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        users.UserId = Convert.ToInt32(reader["Role_Id"]);
        //                        users.UserName = reader["User_FirstName"].ToString();
        //                        users.IsActive = Convert.ToBoolean(reader["User_IsActive"].ToString());
        //                        users.Email = reader["User_Email"].ToString();
        //                    }
        //                }
        //                if (connection.State != ConnectionState.Closed)
        //                    connection.Close();
        //            }
        //            return users.UserId > 0 ? users : null;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw new UnauthorizedAccessException();
        //    }
        //    return null;
        //}

        public async Task<Users> LoginUser(string userName, string password)
        {
            Users user = null;

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand(SqlContstants.ORG_User_Login, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@username", userName);
                        command.Parameters.AddWithValue("@password", password);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                user = new Users
                                {
                                    UserId = Convert.ToInt32(reader["User_Id"]),
                                    UserName = reader["User_FirstName"].ToString(),
                                    IsActive = reader.GetBoolean(reader.GetOrdinal("User_IsActive")),
                                    Email = reader.GetString(reader.GetOrdinal("User_Email")),
                                    RoleId = reader.GetInt32(reader.GetOrdinal("Role_Id"))
                                };
                            }
                        }
                    }
                }

                return user;
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                //_logger.LogError(ex, "An error occurred during login.");

                // Throw a custom exception for invalid credentials
                throw new UnauthorizedAccessException("Invalid username or password.", ex);
            }
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
                                employee.IsActive = Convert.ToBoolean(reader["IsActive"]);                                
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
        public async Task<List<EmployeeDetails>> UpdateEmployeeDataById(EmployeeDetails employeeDetails)
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
                        command.Parameters.AddWithValue("@empId", employeeDetails.EmployeeId);
                        command.Parameters.AddWithValue("@FirstName", employeeDetails.FirstName);
                        command.Parameters.AddWithValue("@LastName", employeeDetails.LastName);
                        command.Parameters.AddWithValue("@SurName", employeeDetails.SurName);
                        command.Parameters.AddWithValue("@Email", employeeDetails.Email);
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
        public async Task<List<EmployeeDetails>> SaveEmployeeData(EmployeeDetails employeeDetails)
        {
            try
            {

                List<EmployeeDetails> lstEmployee = new List<EmployeeDetails>();
                int result;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(SqlContstants.ORG_SAVE_EmployeeDetails, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@firstName", employeeDetails.FirstName);
                        command.Parameters.AddWithValue("@lastName", employeeDetails.LastName);
                        command.Parameters.AddWithValue("@surName", employeeDetails.SurName);
                        command.Parameters.AddWithValue("@phoneNumber", employeeDetails.PhoneNumber);
                        command.Parameters.AddWithValue("@email", employeeDetails.Email);
                        command.Parameters.AddWithValue("@password", employeeDetails.Password);
                        command.Parameters.AddWithValue("@dob", employeeDetails.DOB);
                        command.Parameters.AddWithValue("@doj", employeeDetails.DOJ);
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

        public async Task<List<ProductDetails>> GetAllProductDetails()
        {
            try
            {
                List<ProductDetails> prodDetails = new List<ProductDetails>();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(SqlContstants.ORG_GET_ALL_ProductDetails, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ProductDetails employee = new ProductDetails();
                                employee.ProductId = Convert.ToInt32(reader["ProductId"]);
                                employee.ProductName = reader["ProductType"].ToString();
                                employee.ProductType = reader["ProductName"].ToString();
                                employee.PricePerItem = Convert.ToDouble(reader["PricePerItem"]);
                                employee.InOffer = Convert.ToBoolean(reader["InOffer"]);
                                employee.OfferPercentage = Convert.ToDouble(reader["OfferPercentage"]);
                                employee.ProductItems = Convert.ToDouble(reader["ProductItems"]);
                                employee.DateCreated = Convert.ToDateTime(reader["DateCreated"]);
                                employee.CreatedBy = Convert.ToString(reader["CreatedBy"]);                 
                                employee.IsExists = Convert.ToBoolean(reader["IsExists"]);
                                prodDetails.Add(employee);
                            }
                        }
                        if (connection.State != ConnectionState.Closed)
                            connection.Close();
                    }
                    return prodDetails;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<List<ProductDetails>> SaveProductDetails(ProductDetails productDetails)
        {
            try
            {
                List<ProductDetails> lstEmployee = new List<ProductDetails>();
                int result;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(SqlContstants.ORG_SAVE_ProductDetails, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@productType", productDetails.ProductType);
                        command.Parameters.AddWithValue("@productName", productDetails.ProductName);
                        command.Parameters.AddWithValue("@pricePerItem", productDetails.PricePerItem);
                        command.Parameters.AddWithValue("@inOffer", productDetails.InOffer);
                        command.Parameters.AddWithValue("@offerPercentage", productDetails.OfferPercentage);
                        command.Parameters.AddWithValue("@productItems", productDetails.ProductItems);
                        result = command.ExecuteNonQuery();
                    }
                    if (result > 0)
                    {
                        return lstEmployee = await this.GetAllProductDetails();
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

        //public async Task<bool> RegisterComplaints(ComplaintDetails complaintDetails)
        //{
        //    try {
        //        int result;
        //        using (SqlConnection connection = new SqlConnection(connectionString))
        //        {
        //            connection.Open();
        //            using (SqlCommand command = new SqlCommand(SqlContstants.ORG_SAVE_EmployeeDetails, connection))
        //            {
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.Parameters.AddWithValue("@complaintUserName", complaintDetails.ComplaintUserName);
        //                command.Parameters.AddWithValue("@mobileNumber", complaintDetails.MobileNumber);
        //                command.Parameters.AddWithValue("@complaintDescription", complaintDetails.ComplaintDescription);
        //                result = command.ExecuteNonQuery();
        //                return result >0 ? true :false;
        //            }
                    
        //            if (connection.State != ConnectionState.Closed)
        //                connection.Close();
        //        }
        //    }
        //    catch (Exception) { }
        //    return false;
        //}

        public async Task<bool> RegisterComplaints(ComplaintDetails complaintDetails)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand(SqlContstants.ORG_SAVE_EmployeeDetails, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@complaintUserName", complaintDetails.ComplaintUserName);
                        command.Parameters.AddWithValue("@mobileNumber", complaintDetails.MobileNumber);
                        command.Parameters.AddWithValue("@complaintDescription", complaintDetails.ComplaintDescription);

                        int result = await command.ExecuteNonQueryAsync();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                // _logger.LogError(ex, "An error occurred while registering complaints.");

                // Re-throw the exception or handle it as needed
                throw new ApplicationException("An error occurred while registering complaints.", ex);
            }
        }
        public async Task<bool> RegisterUserDetails(Users  userDetails) {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand(SqlContstants.ORG_Save_UserDetail, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@FirstName", userDetails.FirstName);                        
                        command.Parameters.AddWithValue("@LastName", userDetails.LastName);
                        command.Parameters.AddWithValue("@SurName", userDetails.SurName);
                        command.Parameters.AddWithValue("@Role_Id", userDetails.RoleId);
                        command.Parameters.AddWithValue("@Email",userDetails.Email);
                        command.Parameters.AddWithValue("@PhoneNumber", userDetails.PhoneNumber);
                        command.Parameters.AddWithValue("@Password", userDetails.Password);
                        command.Parameters.AddWithValue("@IsActive", userDetails.IsActive);
                        command.Parameters.AddWithValue("@DateOfBirth", userDetails.DateOfBirth);
                        int result = await command.ExecuteNonQueryAsync();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                // _logger.LogError(ex, "An error occurred while registering complaints.");

                // Re-throw the exception or handle it as needed
                throw new ApplicationException("An error occurred while registering complaints.", ex);
            }
        }

        public async Task<List<EmployeeDetails>> DeleteUserDetailsById(int userId)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand(SqlContstants.ORG_Delete_UserDetail, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserId", userId);

                        int result = await command.ExecuteNonQueryAsync();

                        if (result > 0)
                        {
                            return await GetAllEmployeeDetails();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                // _logger.LogError(ex, "An error occurred while deleting employee details.");

                // Re-throw the exception or handle it as needed
                throw new ApplicationException("An error occurred while deleting employee details.", ex);
            }

            return null;
        }

    }
}
