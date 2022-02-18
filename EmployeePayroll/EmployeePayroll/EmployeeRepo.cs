using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePayroll
{
    public class EmployeeRepo
    {
        public static string connectionString = @"Data Source=DESKTOP-SBPIUH9;Initial Catalog=Employee_payroll;Integrated Security=True";
        SqlConnection connection = null;

        public void GetAllEmployee()
        {
         
            try
            {
                EmployeePayroll employeePayroll = new EmployeePayroll();
                using (connection = new SqlConnection(connectionString))
                {
                    string query = @"SELECT * FROM emp_payroll;";

                    //define SqlCommand Object
                    SqlCommand cmd = new SqlCommand(query, connection);
                    //establish connection
                    connection.Open();
                    Console.WriteLine("connected");
                    SqlDataReader dr = cmd.ExecuteReader();
                    readDataRows(dr, employeePayroll);
                    
                    dr.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                //close connection
                connection.Close();
            }
        }
        public int UpdateEmployee()
        {
            EmployeePayroll emp = new EmployeePayroll();
            emp.employeeName = "john";
            emp.basicPay = 300000;
            emp.department = "IT";
            emp.address = "bangalore";
            emp.phoneNumber = "9876543210";
            emp.deductions = 5000;
            emp.taxablePay = 15000;
            emp.tax = 5000;
            emp.netPay = 275000;
            try
            {
                //establish connection
                using (connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("spUpdateEmp", connection);
                    //trigger stored procedure
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    //pass parameters
                    sqlCommand.Parameters.AddWithValue("@Name", emp.employeeName);
                    sqlCommand.Parameters.AddWithValue("@BasicPay", emp.basicPay);
                    sqlCommand.Parameters.AddWithValue("@department", emp.department);
                    sqlCommand.Parameters.AddWithValue("@address", emp.address);
                    sqlCommand.Parameters.AddWithValue("@PhoneNumber", emp.phoneNumber);
                    sqlCommand.Parameters.AddWithValue("@Deduction", emp.deductions);
                    sqlCommand.Parameters.AddWithValue("@TaxablePay", emp.taxablePay);
                    sqlCommand.Parameters.AddWithValue("@Tax", emp.tax);
                    sqlCommand.Parameters.AddWithValue("@NetPay", emp.netPay);

                    //check if there is a row
                    int result = sqlCommand.ExecuteNonQuery();
                    if (result == 1)
                        Console.WriteLine("employee details are updated...");
                    else
                        Console.WriteLine("details are not updated!");
                    return result;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                //close connection
                connection.Close();
            }
        }

        //method to read all rows
        static void readDataRows(SqlDataReader dr, EmployeePayroll employeePayroll)
        {
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    employeePayroll.employeeId = dr.GetInt32(0);
                    employeePayroll.employeeName = dr.GetString(1);
                    employeePayroll.basicPay = dr.GetDecimal(2);
                    employeePayroll.startDate = dr.GetDateTime(3);
                    employeePayroll.Gender = dr.GetString(4);
                    employeePayroll.phoneNumber = dr.GetString(5);
                    employeePayroll.address = dr.GetString(6);
                    employeePayroll.department = dr.GetString(7);
                    employeePayroll.deductions = dr.GetDecimal(8);
                    employeePayroll.taxablePay = dr.GetDecimal(9);
                    employeePayroll.tax = dr.GetDecimal(10);
                    employeePayroll.netPay = dr.GetDecimal(11);

                    //Display retrieved record
                    Console.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}", employeePayroll.employeeId, employeePayroll.employeeName, employeePayroll.phoneNumber, employeePayroll.address, employeePayroll.department, employeePayroll.Gender, employeePayroll.basicPay, employeePayroll.deductions, employeePayroll.taxablePay, employeePayroll.tax, employeePayroll.netPay);
                    Console.WriteLine("\n");
                }
            }
            else
            {
                Console.WriteLine("No data found!");
            }

        }

        public void GetEmployeeDetailsByDate()
        {
            EmployeePayroll employee = new EmployeePayroll();
            DateTime startDate = new DateTime(2015, 01, 02);
            DateTime endDate = new DateTime(2020, 04, 15);
            try
            {
                //establish connection
                using (connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("spGetDataByDateRange", connection);
                    //stored procedure
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    //pass parameters
                    sqlCommand.Parameters.AddWithValue("@StartDate", startDate);
                    sqlCommand.Parameters.AddWithValue("@EndDate", endDate);
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    //read all rows & display data
                    readDataRows(reader, employee);
                    reader.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                //close connection
                connection.Close();
            }
        }
    }
}
