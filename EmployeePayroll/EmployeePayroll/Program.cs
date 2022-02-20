using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePayroll
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Sql database connectivity!");

            EmployeeRepo repo = new EmployeeRepo();
            //repo.GetAllEmployee(@"SELECT * FROM Payroll;");
            //repo.UpdateEmployee();
            //repo.GetEmployeeDetailsByDate();

            // repo.UsingDatabaseFunction();
            Payroll payroll = new Payroll();    
            EmployeePayroll employeePayroll = new EmployeePayroll();    
            Department department = new Department();   
            //repo.AddEmployeeToPayroll(payroll, employeePayroll, department);
            string deleteQuery = "delete from Payroll where employee_id=6;"+"delete from Department where employee_id = 6;"+"delete from Employee where employee_id = 6;";
            repo.DeleteFeomAllATables(deleteQuery);
        }
    }
}
