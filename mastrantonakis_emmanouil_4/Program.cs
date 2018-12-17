using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mastrantonakis_emmanouil_4
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=localhost\\SQLExpress;" +
                                     "Database=School;" +
                                     "Integrated Security=SSPI;";

            DatabaseAccessLayer dbal = new DatabaseAccessLayer(connectionString);

            //Insert a student and a worker
            dbal.InsertStudentFull("Derrick", "Rose", "DR999");
            dbal.InsertWorkerFull("George", "Pasparakis", 800, 12);

            //Uncomment the following to test the constraints:

            ////If first letter is not capital software fixes it
            //dbal.InsertWorkerFull("steve", "jobs", 5800, 10);

            ////Faculty Number should be consisted only of digits and numbers
            //dbal.InsertStudentFull("Jin", "Liyu", "C_69786");

            ////Faculty Number digits should be in range 5-10
            //dbal.InsertStudentFull("Jin", "Liyu", "D156");

            ////Weekly wage should be more than 10
            //dbal.InsertWorkerFull("John", "Poor", 10, 12);

            ////Working hours should be in range 1 - 12
            //dbal.InsertWorkerFull("Josh", "Lazy", 100, 0);

            //Print all the students and the workers
            List<Student> listOfAllStudents = dbal.getAllStudents();
            List<Worker> listOfAllWorkers = dbal.getAllWorkers();

            foreach(Student student in listOfAllStudents)
            {
                Console.WriteLine($"STUDENT:\nFirst Name - {student.FirstName} " +
                                    $"\nLast Name - {student.LastName} " +
                                    $"\nFaculty Number - {student.FacultyNumber}");
            }

            foreach(Worker worker in listOfAllWorkers)
            {
                Console.WriteLine($"TEACHER:\nFirst Name - {worker.FirstName} " +
                    $"\nLast Name - {worker.LastName} \nWeekly Salary - {worker.WeekSalary} " +
                    $"euros \nHours Per Day - {worker.HoursPerDay} " +
                            $"\nSalary per hour - {worker.GetSalaryPerHour()} euros ");
            }

            //Finally print the workforce of the company
            Console.WriteLine($"Our company has - {listOfAllWorkers.Count} workers");

            Console.ReadKey();

        }

    }
}
