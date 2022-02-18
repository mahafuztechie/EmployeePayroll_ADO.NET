using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePayroll
{
    internal class EmployeeRepo
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
                            Console.WriteLine("{0},{1},{2},{3},{4},{5}", employeePayroll.employeeId, employeePayroll.employeeName, employeePayroll.phoneNumber, employeePayroll.address, employeePayroll.department, employeePayroll.Gender, employeePayroll.phoneNumber);
                            Console.WriteLine("\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found!");
                    }
                    
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
        public int UpdateEmployeeSalary()
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
                using (connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("spUpdateEmp", connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Name", emp.employeeName);
                    sqlCommand.Parameters.AddWithValue("@BasicPay", emp.basicPay);
                    sqlCommand.Parameters.AddWithValue("@department", emp.department);
                    sqlCommand.Parameters.AddWithValue("@address", emp.address);
                    sqlCommand.Parameters.AddWithValue("@PhoneNumber", emp.phoneNumber);
                    sqlCommand.Parameters.AddWithValue("@Deduction", emp.deductions);
                    sqlCommand.Parameters.AddWithValue("@TaxablePay", emp.taxablePay);
                    sqlCommand.Parameters.AddWithValue("@Tax", emp.tax);
                    sqlCommand.Parameters.AddWithValue("@NetPay", emp.netPay);


                    int result = sqlCommand.ExecuteNonQuery();
                    if (result == 1)
                        Console.WriteLine("Salary is updated...");
                    else
                        Console.WriteLine("Salary not updated!");
                    return result;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public bool AddEmployee(EmployeePayroll payroll)
        {
            try
            {
                using (this.connection)
                {
                    SqlCommand command = new SqlCommand("spAddEmployeeDetail", this.connection);
                    command.CommandType = CommandType.StoredProcedure;


                    command.Parameters.AddWithValue("@name", payroll.employeeName);
                    command.Parameters.AddWithValue("@basic_Pay", payroll.basicPay);
                    command.Parameters.AddWithValue("@start_date", payroll.startDate);
                    command.Parameters.AddWithValue("@Gender", payroll.Gender);
                    command.Parameters.AddWithValue("@phonenumber", payroll.phoneNumber);
                    command.Parameters.AddWithValue("@address", payroll.address);
                    command.Parameters.AddWithValue("@department", payroll.department);
                    command.Parameters.AddWithValue("@Deductions", payroll.deductions);
                    command.Parameters.AddWithValue("@taxable_pay", payroll.taxablePay);
                    command.Parameters.AddWithValue("@income_tax", payroll.tax);
                    command.Parameters.AddWithValue("@net_pay", payroll.netPay);


                    connection.Open();
                    var result = command.ExecuteNonQuery();
                    connection.Close();

                    if (result != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
