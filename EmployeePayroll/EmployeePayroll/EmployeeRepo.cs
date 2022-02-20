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

        public void GetAllEmployee(string q)
        {
         
            try
            {
                Payroll Payroll = new Payroll();
                using (connection = new SqlConnection(connectionString))
                {
                    string query = q;

                    //define SqlCommand Object
                    SqlCommand cmd = new SqlCommand(query, connection);
                    //establish connection
                    connection.Open();
                    Console.WriteLine("connected");
                    SqlDataReader dr = cmd.ExecuteReader();
                    readDataRows(dr, Payroll);
                    
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

        //method to read all rows
        static void readDataRows(SqlDataReader dr, Payroll payroll)
        {
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    payroll.payrollId = dr.GetString(0);
                    payroll.basicPay = dr.GetDecimal(1);
                    payroll.deductions = dr.GetDecimal(2);
                    payroll.taxablePay = dr.GetDecimal(3);
                    payroll.tax = dr.GetDecimal(4);
                    payroll.netPay = dr.GetDecimal(5);
                    payroll.employeeId = dr.GetInt32(6);

                    //Display retrieved record
                    Console.WriteLine("{0},{1},{2},{3},{4},{5},{6}", payroll.employeeId, payroll.basicPay, payroll.deductions, payroll.taxablePay, payroll.tax, payroll.netPay, payroll.employeeId);
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


        public void UsingDatabaseFunction(string q)
        {
            
            try
            {
                DataBaseFunctions df = new DataBaseFunctions();

                connection = new SqlConnection(connectionString);
                string queryDb = @"SELECT gender,COUNT(basic_pay) AS TotalCount,SUM(basic_pay) AS TotalSum, 
                                   AVG(basic_pay) AS AverageValue, 
                                   MIN(basic_pay) AS MinValue, MAX(basic_pay) AS MaxValue
                                   FROM emp_payroll 
                                   WHERE Gender =  GROUP BY Gender;";

                //define SqlCommand Object
                SqlCommand cmd = new SqlCommand(queryDb, connection);
                connection.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        df.gender = Convert.ToString(dr["Gender"]);
                        df.count = Convert.ToInt32(dr["TotalCount"]);
                        df.totalSum = Convert.ToDecimal(dr["TotalSum"]);
                        df.avg = Convert.ToDecimal(dr["AverageValue"]);
                        df.min = Convert.ToDecimal(dr["MinValue"]);
                        df.max = Convert.ToDecimal(dr["MaxValue"]);
                        Console.WriteLine("Gender: {0}, TotalCount: {1}, TotalSalary: {2}, AvgSalary:  {3}, MinSalary:  {4}, MinSalary:  {5}", df.gender, df.count, df.totalSum, df.avg, df.min, df.max);
                    }
                }
                else
                {
                    Console.WriteLine("Rows doesn't exist!");
                }
                dr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                connection.Close();
            }
            finally
            {
                connection.Close();
            }
            Console.WriteLine();
        }


        public void AddEmployeeToPayroll(Payroll payroll, EmployeePayroll employeePayroll, Department depart)
        {
           
            try
            {
                using (connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("spAddEmpPayrollDetails", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@emp_id", employeePayroll.employeeId = 6);
                    command.Parameters.AddWithValue("@employee_name", employeePayroll.employeeName = "james");
                    command.Parameters.AddWithValue("@phone_no", employeePayroll.phoneNumber = "1234560");
                    command.Parameters.AddWithValue("@address", employeePayroll.address = "up");
                    command.Parameters.AddWithValue("@gender", employeePayroll.Gender = "M");
                    command.Parameters.AddWithValue("@payroll_Id", payroll.payrollId = "#2945");
                    command.Parameters.AddWithValue("@basic_pay", payroll.basicPay = 100000);
                    command.Parameters.AddWithValue("@deduction", payroll.deductions = 20000);
                    command.Parameters.AddWithValue("@income_tax", payroll.tax = 5000);
                    command.Parameters.AddWithValue("@taxable_pay", payroll.taxablePay = 5000);
                    command.Parameters.AddWithValue("@net_pay", payroll.netPay = 70000);
                    command.Parameters.AddWithValue("@department_Id", depart.departmentId = 505);
                    command.Parameters.AddWithValue("@departmentName", depart.departmentName = "Fullstack");

                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    if (result != 0)
                    {
                        Console.WriteLine("added successfully...");

                    }
                    else
                    {
                        Console.WriteLine("adding data failed...");
                    }
                    
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


        public void DeleteFeomAllATables(string q)
        {

            try
            {
                Payroll Payroll = new Payroll();
                using (connection = new SqlConnection(connectionString))
                {
                    string query = q;

                    //define SqlCommand Object
                    SqlCommand cmd = new SqlCommand(query, connection);
                    //establish connection
                    connection.Open();
                    Console.WriteLine("connected");
                    cmd.ExecuteReader();
                    Console.WriteLine("deleted payroll successfully");
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
    }
}
