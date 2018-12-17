using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace mastrantonakis_emmanouil_4
{
    public class DatabaseAccessLayer
    {
        private string _connectionString;
        private const string _getAllWorkersQuery = "SELECT * FROM Workers";
        private const string _getAllStudentsQuery = "SELECT * FROM Students";
        private const string _InsertIntoStudentsFullQuery = "INSERT INTO Students (firstName, lastName, facultyNumber) VALUES (@FirstName, @LastName, @FacultyNumber)";
        private const string _InsertIntoStudentsQuery = "INSERT INTO Students (firstName, lastName) VALUES (@FirstName, @LastName)";
        private const string _InsertIntoWorkersFullQuery = "INSERT INTO Workers (firstName, lastName, weeklySalary, hoursPerDay) VALUES (@FirstName, @LastName, @WeeklySalary, @HoursPerDay)";
        private const string _InsertIntoWorkersQuery = "INSERT INTO Workers (firstName, lastName) VALUES (@FirstName, @LastName)";


        //Constructor
        public DatabaseAccessLayer(string connectionString)
        {
            _connectionString = connectionString;
        }

        //Get All Methods
        public List<Student> getAllStudents()
        {
            List<Student> students = new List<Student>();
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command =
                    new SqlCommand(_getAllStudentsQuery, sqlConnection))
                {
                    sqlConnection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Student student = new Student();
                                student.FirstName = reader.GetString(1);
                                student.LastName = reader.GetString(2);
                                student.FacultyNumber = reader.GetString(3);
                                students.Add(student);
                            }
                        }
                    }
                }
            }
            return students;
        }

        public List<Worker> getAllWorkers()
        {
            List<Worker> workers = new List<Worker>();
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command =
                    new SqlCommand(_getAllWorkersQuery, sqlConnection))
                {
                    sqlConnection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Worker worker = new Worker();
                                worker.FirstName = reader.GetString(1);
                                worker.LastName = reader.GetString(2);
                                worker.WeekSalary = Math.Round(reader.GetDecimal(3),2);
                                worker.HoursPerDay = reader.GetInt32(4);
                                workers.Add(worker);
                            }
                        }
                    }
                }
            }
            return workers;
        }

        //Insert Student
        public void InsertStudentFull(string firstName, string lastName, string facultyNumber)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(_InsertIntoStudentsFullQuery, sqlConnection))
                {
                    sqlConnection.Open();
                    string FirstName;
                    if (char.IsLower(firstName[0]))
                    {
                        FirstName = firstName.First().ToString().ToUpper() + String.Join("", firstName.Skip(1));
                    }
                    else
                    {
                        FirstName = firstName;
                    }
                    if (firstName.Length < 3)
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                    string LastName;
                    if (char.IsLower(lastName[0]))
                    {
                        LastName = lastName.First().ToString().ToUpper() + String.Join("", lastName.Skip(1));
                    }
                    else
                    {
                        LastName = lastName;
                    }
                    if (lastName.Length < 4)
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                    string FacultyNumber;
                    for(int i=0; i<facultyNumber.Length; i++)
                    {
                        if (Char.IsLetter(facultyNumber[i]))
                            break;
                        if(!Enumerable.Range(5, 10).Contains(facultyNumber[i]))
                        {
                            throw new ArgumentOutOfRangeException();
                        }
                    }
                    if (!facultyNumber.All(Char.IsLetterOrDigit))
                    {
                        throw new ArgumentException();
                    }
                    FacultyNumber = facultyNumber;
                    command.Parameters.AddWithValue("FirstName", FirstName);
                    command.Parameters.AddWithValue("LastName", LastName);
                    command.Parameters.AddWithValue("FacultyNumber", FacultyNumber);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void InsertStudent(string firstName, string lastName)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(_InsertIntoStudentsQuery, sqlConnection))
                {
                    sqlConnection.Open();
                    string FirstName;
                    if (char.IsLower(firstName[0]))
                    {
                        FirstName = firstName.First().ToString().ToUpper() + String.Join("", firstName.Skip(1));
                    }
                    else
                    {
                        FirstName = firstName;
                    }
                    if (firstName.Length < 3)
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                    string LastName;
                    if (char.IsLower(lastName[0]))
                    {
                        LastName = lastName.First().ToString().ToUpper() + String.Join("", lastName.Skip(1));
                    }
                    else
                    {
                        LastName = lastName;
                    }
                    if (lastName.Length < 4)
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                    command.Parameters.AddWithValue("FirstName", FirstName);
                    command.Parameters.AddWithValue("LastName", LastName);
                    command.ExecuteNonQuery();
                }
            }
        }

        //Insert Worker
        public void InsertWorkerFull(string firstName, string lastName, decimal weeklySalary, int hoursPerDay)
        {
            Worker worker = new Worker(firstName, lastName, weeklySalary, hoursPerDay);
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(_InsertIntoWorkersFullQuery, sqlConnection))
                {
                    sqlConnection.Open();
                    string FirstName;
                    if (char.IsLower(firstName[0]))
                    {
                        FirstName = firstName.First().ToString().ToUpper() + String.Join("", firstName.Skip(1));
                    }
                    else
                    {
                        FirstName = firstName;
                    }
                    if (firstName.Length < 3)
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                    string LastName;
                    if (char.IsLower(lastName[0]))
                    {
                        LastName = lastName.First().ToString().ToUpper() + String.Join("", lastName.Skip(1));
                    }
                    else
                    {
                        LastName = lastName;
                    }
                    if (lastName.Length < 4)
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                    if (weeklySalary <= 10)
                    {
                        throw new ArgumentException();
                    }
                    if (hoursPerDay < 1 || hoursPerDay > 12)
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                    command.Parameters.AddWithValue("FirstName", FirstName);
                    command.Parameters.AddWithValue("LastName", LastName);
                    command.Parameters.AddWithValue("WeeklySalary", weeklySalary);
                    command.Parameters.AddWithValue("HoursPerDay", hoursPerDay);
                    command.Parameters.AddWithValue("SalaryPerHour", worker.GetSalaryPerHour());
                    command.ExecuteNonQuery();
                }
            }
        }

        public void InsertWorker(string firstName, string lastName)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(_InsertIntoWorkersQuery, sqlConnection))
                {
                    sqlConnection.Open();
                    string FirstName;
                    if (char.IsLower(firstName[0]))
                    {
                        FirstName = firstName.First().ToString().ToUpper() + String.Join("", firstName.Skip(1));
                    }
                    else
                    {
                        FirstName = firstName;
                    }
                    if (firstName.Length < 3)
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                    string LastName;
                    if (char.IsLower(lastName[0]))
                    {
                        LastName = lastName.First().ToString().ToUpper() + String.Join("", lastName.Skip(1));
                    }
                    else
                    {
                        LastName = lastName;
                    }
                    if (lastName.Length < 4)
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                    command.Parameters.AddWithValue("FirstName", firstName);
                    command.Parameters.AddWithValue("LastName", LastName);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}





