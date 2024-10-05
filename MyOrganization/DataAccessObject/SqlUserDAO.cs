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
                                users.UserId = Convert.ToInt32(reader["EmployeeId"]);
                                users.UserName = reader["FirstName"].ToString();
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

    }
}
