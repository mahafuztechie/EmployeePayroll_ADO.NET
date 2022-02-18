using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePayroll
{
    internal class EmployeePayroll
    {
         //entity
        public int employeeId { get; set; }
        public string employeeName { get; set; }
        public string phoneNumber { get; set; }
        public string address { get; set; }
        public string department { get; set; }
        public string Gender { get; set; }
        public decimal basicPay { get; set; }
        public decimal deductions { get; set; }
        public decimal taxablePay { get; set; }
        public decimal tax { get; set; }
        public decimal netPay { get; set; }
        public DateTime startDate { get; set; }
        public string city { get; set; }
        public string country { get; set; }
    }
}

