using System;
using System.Collections.Generic;
using System.Linq;
//Concepts involved 
// Deferred execution - https://chat.openai.com/share/10a117b8-e39a-4384-934a-120c64192063
// 

namespace LINQ_Examples_1
{
    class Program
    {
        public static void Main(string[] args)
        {
            List<Employee> employeeList = Data.GetEmployees(); //Employees List
            List<Department> departmentsLsit = Data.GetDepartments(); //Depts list



            //Select and Where method syntax
            /* var results = employeeList.Select(e => new
             {
                 FullName = e.FirstName + " " + e.LastName,
                 AnnualSalary = e.AnnualSalary
             }).Where(e => e.AnnualSalary >= 5000); 

             foreach ( var item in results ) 
             {
                 Console.WriteLine($"{item.FullName, -20} {item.AnnualSalary, 10}");
             }*/




            // Select Where operators - Query syntax
            /*var results = from emp in employeeList
                          where emp.AnnualSalary > 5000
                          select new
                          {
                              FullName = emp.FirstName + " " + emp.LastName,
                              AnnualSalary = emp.AnnualSalary
                          };

            foreach (var item in results)
            {
                Console.WriteLine($"{item.FullName,-20} {item.AnnualSalary,10}");
            } */




            //Immediate execution example
            /*var results = from emp in employeeList.GetHighSalariedEmployees()
                          select new
                          {
                              FullName = emp.FirstName + " " + emp.LastName,
                              AnnualSalary = emp.AnnualSalary
                          };

            employeeList.Add(new Employee
            {
                Id = 5,
                FirstName = "Sam",
                LastName = "Davis",
                AnnualSalary = 100000.20m,
                IsManager = true,
                DepartmentId = 2
            }) ;

            foreach (var item in results)
            {
                Console.WriteLine($"{item.FullName,-20} {item.AnnualSalary,10}");
            }
           */



            //Join operations example - method syntax
            /*var results = departmentsLsit.Join(employeeList,
                department => department.Id,
                employee => employee.DepartmentId,
                (department, employee) => new
                {
                    FullName = employee.FirstName + " " + employee.LastName,
                    AnnualSalary = employee.AnnualSalary,
                    departmentName = department.LongName
                });

            foreach (var item in results)
            {
                Console.WriteLine($"{item.FullName,-20} {item.AnnualSalary,10} \t {item.departmentName}");
            }*/


            //Join operation example query syntax
            /*var results = from dept in departmentsLsit
                          join emp in employeeList
                          on dept.Id equals emp.DepartmentId
                          select new
                          {
                              FullName = emp.FirstName + " " + emp.LastName,
                              AnnualSalary = emp.AnnualSalary,
                              departmentName = dept.LongName
                          }*/



            // Group Join - Method Syntax
            /*var results = departmentsLsit.GroupJoin(employeeList,
                dept => dept.Id,
                emp => emp.DepartmentId,
                (dept, employeesGroup) => new
                {
                    Employees = employeesGroup,
                    DepartmentName = dept.LongName
                }
                ); 

            foreach(var item in results)
            {
                Console.WriteLine($"Department name {item.DepartmentName}");
                foreach(var employee in item.Employees)
                {
                    Console.WriteLine($"\t {employee.FirstName} {employee.LastName}");
                }
            }*/


            //Group join - Query Syntax
            var results = from dept in departmentsLsit
                          join emp in employeeList
                          on dept.Id equals emp.DepartmentId
                          into employeeGroup
                          select new
                          {
                              Employees = employeeGroup,
                              DepartmentName = dept.LongName
                          };

            foreach (var item in results)
            {
                Console.WriteLine($"Department name {item.DepartmentName}");
                foreach (var employee in item.Employees)
                {
                    Console.WriteLine($"\t {employee.FirstName} {employee.LastName}");
                }


                Console.ReadKey();
            }
        }

        /*public static class EnumerableColletionExtensionMethods
        {
            //everytime a "this" is at the satart of the function its an extension method
            //Ienumerable is a built in interface
            public static IEnumerable<Employee> GetHighSalariedEmployees(this IEnumerable<Employee> employees)
            {
                foreach(Employee emp in employees)
                {
                    Console.WriteLine($"Accessing employee {emp.FirstName + " " + emp.LastName}");

                    if(emp.AnnualSalary >= 50000)
                    {
                        yield return emp;
                    }
                }


            }
        }*/

        public class Employee
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public decimal AnnualSalary { get; set; }
            public bool IsManager { get; set; }
            public int DepartmentId { get; set; }
        }

        public class Department
        {
            public int Id { get; set; }
            public string ShortName { get; set; }
            public string LongName { get; set; }
        }

        public static class Data
        {
            public static List<Employee> GetEmployees()
            {
                List<Employee> employees = new List<Employee>();

                Employee employee = new Employee
                {
                    Id = 1,
                    FirstName = "Bob",
                    LastName = "Jones",
                    AnnualSalary = 60000.3m,
                    IsManager = true,
                    DepartmentId = 1
                };
                employees.Add(employee);
                employee = new Employee
                {
                    Id = 2,
                    FirstName = "Sarah",
                    LastName = "Jameson",
                    AnnualSalary = 80000.1m,
                    IsManager = true,
                    DepartmentId = 2
                };
                employees.Add(employee);
                employee = new Employee
                {
                    Id = 3,
                    FirstName = "Douglas",
                    LastName = "Roberts",
                    AnnualSalary = 40000.2m,
                    IsManager = false,
                    DepartmentId = 2
                };
                employees.Add(employee);
                employee = new Employee
                {
                    Id = 4,
                    FirstName = "Jane",
                    LastName = "Stevens",
                    AnnualSalary = 30000.2m,
                    IsManager = false,
                    DepartmentId = 3
                };
                employees.Add(employee);

                return employees;
            }

            public static List<Department> GetDepartments()
            {
                List<Department> departments = new List<Department>();

                Department department = new Department
                {
                    Id = 1,
                    ShortName = "HR",
                    LongName = "Human Resources"
                };
                departments.Add(department);
                department = new Department
                {
                    Id = 2,
                    ShortName = "FN",
                    LongName = "Finance"
                };
                departments.Add(department);
                department = new Department
                {
                    Id = 3,
                    ShortName = "TE",
                    LongName = "Technology"
                };
                departments.Add(department);

                return departments;
            }
        }
    }
}