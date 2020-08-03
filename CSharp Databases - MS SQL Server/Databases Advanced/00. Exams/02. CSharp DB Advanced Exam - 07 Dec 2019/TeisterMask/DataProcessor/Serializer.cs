namespace TeisterMask.DataProcessor
{
    using System;
    using System.IO;
    using System.Text;
    using System.Linq;
    using System.Globalization;
    using System.Xml.Serialization;
    using System.Collections.Generic;

    using Data;

    using Newtonsoft.Json;

    using TeisterMask.DataProcessor.ExportDto;

    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportProjectWithTheirTasks(TeisterMaskContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var projects = context
                .Projects
                .ToList()
                .Where(p => p.Tasks.Any())
                .Select(p => new ExportProjectDto
                {
                    TasksCount = p.Tasks.Count,
                    ProjectName = p.Name,
                    HasEndDate = p.DueDate == null ? "No" : "Yes",
                    Tasks = p.Tasks.Select(pt => new ExportTaskDto
                    {
                        Name = pt.Name,
                        Label = pt.LabelType.ToString()
                    })
                    .OrderBy(t => t.Name)
                    .ToList()
                })
                .OrderByDescending(p => p.TasksCount)
                .ThenBy(p => p.ProjectName)
                .ToList();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<ExportProjectDto>), new XmlRootAttribute("Projects"));

            var namespaces = new XmlSerializerNamespaces();

            namespaces.Add(string.Empty, string.Empty);

            xmlSerializer.Serialize(new StringWriter(stringBuilder), projects, namespaces);

            return stringBuilder.ToString().TrimEnd();
        }

        public static string ExportMostBusiestEmployees(TeisterMaskContext context, DateTime date)
        {
            var employees = context
                .Employees
                .Where(e => e.EmployeesTasks.Any(t => t.Task.OpenDate >= date))
                .Select(e => new
                {
                    e.Username,
                    Tasks = e.EmployeesTasks
                                    .Where(t => t.Task.OpenDate >= date)
                                    .OrderByDescending(x=>x.Task.DueDate)
                                    .ThenBy(x=>x.Task.Name)
                                    .Select(et => new
                                    {
                                        TaskName = et.Task.Name,
                                        OpenDate = et.Task.OpenDate.ToString("d", CultureInfo.InvariantCulture),
                                        DueDate = et.Task.DueDate.ToString("d", CultureInfo.InvariantCulture),
                                        LabelType = et.Task.LabelType.ToString(),
                                        ExecutionType = et.Task.ExecutionType.ToString()
                                    })
                                    .ToList()
                })
                .ToList()
                .OrderByDescending(x=>x.Tasks.Count)
                .ThenBy(x=>x.Username)
                .Take(10)
                .ToList();

            string json = JsonConvert.SerializeObject(employees, Formatting.Indented);

            return json;
        }
    }
}