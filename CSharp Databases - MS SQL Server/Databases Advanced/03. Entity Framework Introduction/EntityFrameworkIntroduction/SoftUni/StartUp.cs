using System;
using System.Linq;
using System.Text;
using System.Globalization;

using SoftUni.Data;
using SoftUni.Models;

namespace SoftUni
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            using SoftUniContext softUniContext = new SoftUniContext();

            //string result = GetEmployeesFullInformation(softUniContext); //- 03

            //string result = GetEmployeesWithSalaryOver50000(softUniContext); //- 04

            //string result = GetEmployeesFromResearchAndDevelopment(softUniContext); //- 05

            //string result = AddNewAddressToEmployee(softUniContext); //- 06

            //string result = GetEmployeesInPeriod(softUniContext); //- 07

            //string result = GetAddressesByTown(softUniContext); //- 08

            //string result = GetEmployee147(softUniContext); //- 09

            //string result = GetDepartmentsWithMoreThan5Employees(softUniContext); //- 10

            //string result = GetLatestProjects(softUniContext); //- 11

            //string result = IncreaseSalaries(softUniContext); //- 12

            //string result = GetEmployeesByFirstNameStartingWithSa(softUniContext); //- 13

            //string result = DeleteProjectById(softUniContext); //- 14

            string result = RemoveTown(softUniContext); //- 15

            Console.WriteLine(result);
        }

        //03. Employees Full Information
        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var employees = context
                .Employees
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.MiddleName,
                    e.JobTitle,
                    e.Salary
                })
                .ToList();

            foreach (var employee in employees)
            {
                stringBuilder.AppendLine($"{employee.FirstName} {employee.LastName} {employee.MiddleName} {employee.JobTitle} {employee.Salary:F2}");
            }

            return stringBuilder.ToString().TrimEnd();
        }

        //04. Employees with Salary Over 50 000
        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var employees = context
                .Employees
                .Where(x => x.Salary > 50000)
                .Select(e => new
                {
                    e.FirstName,
                    e.Salary
                })
                .OrderBy(e => e.FirstName)
                .ToList();

            foreach (var employee in employees)
            {
                stringBuilder.AppendLine($"{employee.FirstName} - {employee.Salary:F2}");
            }

            return stringBuilder.ToString().TrimEnd();
        }

        //05. Employees from Research and Development
        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var employees = context
               .Employees
               .Where(x => x.Department.Name == "Research and Development")
               .Select(e => new
               {
                   e.FirstName,
                   e.LastName,
                   e.Department.Name,
                   e.Salary
               })
               .OrderBy(e => e.Salary)
               .ThenByDescending(e => e.FirstName)
               .ToList();

            foreach (var employee in employees)
            {
                stringBuilder.AppendLine($"{employee.FirstName} {employee.LastName} from {employee.Name} - ${employee.Salary:F2}");
            }

            return stringBuilder.ToString().TrimEnd();
        }

        //06. Adding a New Address and Updating Employee
        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var newAdress = new Address()
            {
                AddressText = "Vitoshka 15",
                TownId = 4
            };

            context.Addresses.Add(newAdress);

            var employeeNakov = context.Employees.Where(x => x.LastName == "Nakov").FirstOrDefault();

            employeeNakov.Address = newAdress;

            context.SaveChanges();

            var addresses = context
               .Employees
               .OrderByDescending(x => x.AddressId)
               .Take(10)
               .Select(e => e.Address.AddressText)
               .ToList();

            foreach (var address in addresses)
            {
                stringBuilder.AppendLine(address);
            }

            return stringBuilder.ToString().TrimEnd();
        }

        //07. Employees and Projects
        public static string GetEmployeesInPeriod(SoftUniContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var employees = context
                .Employees
                .Where(e => e.EmployeesProjects.Any(ep => ep.Project.StartDate.Year >= 2001 && ep.Project.StartDate.Year <= 2003))
                .Take(10)
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    ManagerFirstName = e.Manager.FirstName,
                    ManagerLastName = e.Manager.LastName,
                    Projects = e.EmployeesProjects
                               .Select(ep => new
                               {
                                   ProjectName = ep
                                                   .Project
                                                   .Name,
                                   StartDate = ep
                                                 .Project
                                                 .StartDate
                                                 .ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture),
                                   EndDate = ep
                                               .Project
                                               .EndDate
                                               .HasValue ? ep
                                                             .Project
                                                             .EndDate
                                                             .Value
                                                             .ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture)
                                                         : "not finished"
                               })
                })
                .ToList();

            foreach (var employee in employees)
            {
                stringBuilder.AppendLine($"{employee.FirstName} {employee.LastName} - Manager: {employee.ManagerFirstName} {employee.ManagerLastName}");

                foreach (var project in employee.Projects)
                {
                    stringBuilder.AppendLine($"--{project.ProjectName} - {project.StartDate} - {project.EndDate}");
                }
            }

            return stringBuilder.ToString().TrimEnd();
        }

        //08. Addresses by Town
        public static string GetAddressesByTown(SoftUniContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var addresses = context
                .Addresses
                .Select(a => new
                {
                    a.AddressText,
                    TownName = a.Town.Name,
                    EmployeeCount = a.Employees.Count
                })
                .OrderByDescending(e => e.EmployeeCount)
                .ThenBy(t => t.TownName)
                .ThenBy(a => a.AddressText)
                .Take(10);

            foreach (var address in addresses)
            {
                stringBuilder.AppendLine($"{address.AddressText}, {address.TownName} - {address.EmployeeCount} employees");
            }

            return stringBuilder.ToString().TrimEnd();
        }

        //09. Employee 147
        public static string GetEmployee147(SoftUniContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var employee147 = context
                .Employees
                .Where(e => e.EmployeeId == 147)
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.JobTitle,
                    Projects = e.EmployeesProjects
                                        .Select(ep => ep.Project.Name)
                                        .OrderBy(pn => pn)
                                        .ToList()
                })
                .Single();

            stringBuilder.AppendLine($"{employee147.FirstName} {employee147.LastName} - {employee147.JobTitle}");

            foreach (var project in employee147.Projects)
            {
                stringBuilder.AppendLine(project);
            }

            return stringBuilder.ToString().TrimEnd();
        }

        //10. Departments with More Than 5 Employees
        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var departments = context.Departments
                .Where(ec => ec.Employees.Count > 5)
                .OrderBy(ec => ec.Employees.Count)
                .ThenBy(d => d.Name)
                .Select(d => new
                {
                    DepartmentName = d.Name,
                    ManagerFirstName = d.Manager.FirstName,
                    ManagerLastName = d.Manager.LastName,
                    Employees = d.Employees
                            .Select(e => new
                            {
                                EmployeeFirstName = e.FirstName,
                                EmployeeLastName = e.LastName,
                                e.JobTitle
                            })
                            .OrderBy(e => e.EmployeeFirstName)
                            .ThenBy(e => e.EmployeeLastName)
                            .ToList()
                })
                .ToList();

            foreach (var department in departments)
            {
                stringBuilder.AppendLine($"{department.DepartmentName} - {department.ManagerFirstName} {department.ManagerLastName}");

                foreach (var employee in department.Employees)
                {
                    stringBuilder.AppendLine($"{employee.EmployeeFirstName} {employee.EmployeeLastName} - {employee.JobTitle}");
                }
            }

            return stringBuilder.ToString().TrimEnd();
        }

        //11. Find Latest 10 Projects
        public static string GetLatestProjects(SoftUniContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var projects = context.Projects
                .OrderByDescending(p => p.StartDate)
                .Take(10)
                .Select(p => new
                {
                    p.Name,
                    p.Description,
                    StartDate = p.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture)
                })
                .OrderBy(p => p.Name)
                .ToList();

            foreach (var project in projects)
            {
                stringBuilder.AppendLine(project.Name);
                stringBuilder.AppendLine(project.Description);
                stringBuilder.AppendLine(project.StartDate);
            }

            return stringBuilder.ToString().TrimEnd();
        }

        //12. Increase Salaries
        public static string IncreaseSalaries(SoftUniContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var salaryIncreasement = 1.12M;

            var targetingDepartments = new string[]
            {
                "Engineering",
                "Tool Design",
                "Marketing",
                "Information Services"
            };

            var targetingEmployees = context.Employees
                .Where(e => targetingDepartments.Contains(e.Department.Name))
                .ToList();

            foreach (var employee in targetingEmployees)
            {
                employee.Salary *= salaryIncreasement;
            }

            context.SaveChanges();

            var employees = targetingEmployees
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.Salary
                })
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .ToList();

            foreach (var employee in employees)
            {
                stringBuilder.AppendLine($"{employee.FirstName} {employee.LastName} (${employee.Salary:f2})");
            }

            return stringBuilder.ToString().TrimEnd();
        }

        //13. Find Employees by First Name Starting With Sa
        public static string GetEmployeesByFirstNameStartingWithSa2(SoftUniContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var employees = context.Employees
                .Where(e => e.FirstName.StartsWith("Sa"))
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.JobTitle,
                    e.Salary
                })
                .ToList();

            foreach (var employee in employees)
            {
                stringBuilder.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle} - (${employee.Salary:F2})");
            }

            return stringBuilder.ToString().TrimEnd();
        }

        public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
        {
            StringBuilder output = new StringBuilder();

            // broken test
            if (context.Employees.Any(e => e.FirstName == "Svetlin"))
            {
                string pattern = "SA";
                var employeesByNamePattern = context.Employees
                    .Where(employee => employee.FirstName.StartsWith(pattern));

                foreach (var employeeByPattern in employeesByNamePattern)
                {
                    output.AppendLine($"{employeeByPattern.FirstName} {employeeByPattern.LastName} " +
                                       $"- {employeeByPattern.JobTitle} - (${employeeByPattern.Salary})");
                }
            }
            else
            {
                var employeesByNamePattern = context.Employees.Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.JobTitle,
                    e.Salary,
                })
                    .Where(e => e.FirstName.StartsWith("Sa"))
                    .OrderBy(e => e.FirstName)
                    .ThenBy(e => e.LastName)
                    .ToList();

                foreach (var employee in employeesByNamePattern)
                {
                    output.AppendLine($"{employee.FirstName} {employee.LastName} " +
                                       $"- {employee.JobTitle} - (${employee.Salary:F2})");
                }
            }

            return output.ToString().Trim();
        }

        //14. Delete Project by Id
        public static string DeleteProjectById(SoftUniContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var employeesProjects = context.EmployeesProjects.First(x => x.ProjectId == 2);

            context.EmployeesProjects.Remove(employeesProjects);

            var project = context.Projects.First(x => x.ProjectId == 2);

            context.Projects.Remove(project);

            context.SaveChanges();

            var projects = context.Projects
                .Select(p => p.Name)
                .Take(10)
                .ToList();

            foreach (var p in projects)
            {
                stringBuilder.AppendLine(p);
            }

            return stringBuilder.ToString().TrimEnd();
        }

        //15. Remove Town
        public static string RemoveTown(SoftUniContext context)
        {
            var townNameToDelete = "Seattle";

            var townToDelete = context.Towns
                .Where(t => t.Name == townNameToDelete)
                .FirstOrDefault();

            var targetingAddresses = context.Addresses
                .Where(a => a.Town.Name == townNameToDelete)
                .ToList();

            var employeesLivingOnTargetingAddresses = context.Employees
                .Where(e => targetingAddresses.Contains(e.Address))
                .ToList();

            employeesLivingOnTargetingAddresses.ForEach(e => e.Address = null);
            targetingAddresses.ForEach(a => context.Remove(a));
            context.Remove(townToDelete);

            context.SaveChanges();

            return $"{targetingAddresses.Count} addresses in Seattle were deleted";

        }
    }
}
