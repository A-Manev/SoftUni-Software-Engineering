namespace TeisterMask.DataProcessor
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Globalization;
    using System.Xml.Serialization;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using TeisterMask.Data.Models;
    using TeisterMask.Data.Models.Enums;
    using TeisterMask.DataProcessor.ImportDto;

    using Newtonsoft.Json;
    
    using Data;

    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedProject
            = "Successfully imported project - {0} with {1} tasks.";

        private const string SuccessfullyImportedEmployee
            = "Successfully imported employee - {0} with {1} tasks.";

        public static string ImportProjects(TeisterMaskContext context, string xmlString)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var xmlSerializer = new XmlSerializer(typeof(List<ImportProjectDto>), new XmlRootAttribute("Projects"));

            var projectDtos = (List<ImportProjectDto>)xmlSerializer.Deserialize(new StringReader(xmlString));

            List<Project> projects = new List<Project>();

            List<Task> tasks = new List<Task>();

            foreach (var projectDto in projectDtos)
            {
                if (!IsValid(projectDto))
                {
                    stringBuilder.AppendLine(ErrorMessage);
                    continue;
                }

                DateTime projectOpenDate;
                bool isProjectOpenDateValid = DateTime.TryParseExact(projectDto.OpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out projectOpenDate);

                DateTime? projectDueDate;

                if (!string.IsNullOrEmpty(projectDto.DueDate) && !string.IsNullOrWhiteSpace(projectDto.DueDate))
                {
                    projectDueDate = DateTime.ParseExact(projectDto.DueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                else
                {
                    projectDueDate = null;
                }

                if (!isProjectOpenDateValid)
                {
                    stringBuilder.AppendLine(ErrorMessage);
                    continue;
                }

                Project validProject = new Project()
                {
                    Name = projectDto.Name,
                    OpenDate = projectOpenDate,
                    DueDate = projectDueDate
                };

                int taskCount = 0;

                foreach (var taskDto in projectDto.Tasks)
                {
                    if (!IsValid(taskDto))
                    {
                        stringBuilder.AppendLine(ErrorMessage);
                        continue;
                    }

                    DateTime taskOpenDate;
                    bool isTaskOpenDateValid = DateTime.TryParseExact(taskDto.OpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out taskOpenDate);

                    DateTime taskDueDate;
                    bool isTaskDueDateValid = DateTime.TryParseExact(taskDto.DueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out taskDueDate);

                    if (!isTaskOpenDateValid || !isTaskDueDateValid)
                    {
                        stringBuilder.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (taskOpenDate < projectOpenDate)
                    {
                        stringBuilder.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (taskDueDate > projectDueDate)
                    {
                        stringBuilder.AppendLine(ErrorMessage);
                        continue;
                    }

                    Task validTask = new Task()
                    {
                        Name = taskDto.Name,
                        OpenDate = taskOpenDate,
                        DueDate = taskDueDate,
                        ExecutionType = (ExecutionType)taskDto.ExecutionType,
                        LabelType = (LabelType)taskDto.LabelType,
                        Project = validProject
                    };

                    tasks.Add(validTask);
                    taskCount++;
                }

                projects.Add(validProject);
                stringBuilder.AppendLine(string.Format(SuccessfullyImportedProject, validProject.Name, taskCount));
            }

            context.Projects.AddRange(projects);
            context.Tasks.AddRange(tasks);
            context.SaveChanges();

            return stringBuilder.ToString().TrimEnd();
        }

        public static string ImportEmployees(TeisterMaskContext context, string jsonString)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var employeeDtos = JsonConvert.DeserializeObject<List<ImportEmployeeDto>>(jsonString);

            var employees = new List<Employee>();

            var employeeTasks = new List<EmployeeTask>();

            int[] tasks = context
                .Tasks
                .Select(x => x.Id)
                .ToArray();

            foreach (var employeeDto in employeeDtos)
            {
                if (!IsValid(employeeDto))
                {
                    stringBuilder.AppendLine(ErrorMessage);
                    continue;
                }

                Employee employee = new Employee()
                {
                    Username = employeeDto.Username,
                    Email = employeeDto.Email,
                    Phone = employeeDto.Phone
                };

                employees.Add(employee);

                int taskCount = 0;

                foreach (var taskId in employeeDto.Tasks.Distinct())
                {
                    if (!tasks.Contains(taskId) /*(int)bookId.Id > books.Count()*/)
                    {
                        stringBuilder.AppendLine(ErrorMessage);
                        continue;
                    }

                    EmployeeTask employeeTask = new EmployeeTask()
                    {
                        Employee = employee,
                        TaskId = taskId
                    };

                    taskCount++;
                    employeeTasks.Add(employeeTask);
                }

                stringBuilder.AppendLine(string.Format(SuccessfullyImportedEmployee, employee.Username, taskCount));
            }

            context.Employees.AddRange(employees);
            context.EmployeesTasks.AddRange(employeeTasks);
            context.SaveChanges();

            return stringBuilder.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}