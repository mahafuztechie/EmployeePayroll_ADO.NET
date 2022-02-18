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
           repo.GetAllEmployee();
            //repo.UpdateEmployeeSalary();
           // repo.GetEmployeeDetailsByDate();
        }
    }
}
