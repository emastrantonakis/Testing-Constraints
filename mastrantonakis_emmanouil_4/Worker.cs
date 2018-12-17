using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mastrantonakis_emmanouil_4
{
    public class Worker
    {
        //Properties
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }        
        public decimal WeekSalary { get; set; }
        public int HoursPerDay { get; set; }
        public decimal SalaryPerHour { get; set; }


        //Constructor
        public Worker(string firstName, string lastName, decimal weekSalary, int hoursePerDay)
        {
            FirstName = firstName;
            LastName = lastName;
            WeekSalary = weekSalary;
            HoursPerDay = hoursePerDay;  
        }

        public Worker()
        {
        }

        //Methods
        public decimal GetSalaryPerHour()
        {
            decimal result = WeekSalary / 5 / HoursPerDay;
            SalaryPerHour = result;
            return Math.Round(result, 2);
        }
    }
}
