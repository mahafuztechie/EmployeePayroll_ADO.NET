using System;
using System.Collections.Generic;
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
                    //close connection
                    dr.Close();
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
