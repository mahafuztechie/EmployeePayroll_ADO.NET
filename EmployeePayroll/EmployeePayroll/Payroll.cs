using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePayroll
{
    public class Payroll
    {
        public string payrollId { get; set; }
        public decimal basicPay { get; set; }
        public decimal deductions { get; set; }
        public decimal taxablePay { get; set; }
        public decimal tax { get; set; }
        public decimal netPay { get; set; }
        public int employeeId { get; set; }
    }
}
