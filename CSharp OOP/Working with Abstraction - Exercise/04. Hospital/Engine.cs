using System;
using System.Linq;

namespace Hospital
{
    public class Engine
    {
        private Hospital hospital;

        public Engine()
        {
            this.hospital = new Hospital();
        }

        public void Run()
        {
            string command = Console.ReadLine();

            while (command != "Output")
            {
                string[] argumets = command
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                string departmentName = argumets[0];
                string doctorFirstName = argumets[1];
                string doctorLastName = argumets[2];
                string doctorFullName = doctorFirstName + " " + doctorLastName;
                string patientName = argumets[3];

                this.hospital.AddDepartment(departmentName);
                this.hospital.AddDoctor(doctorFirstName, doctorLastName);
                this.hospital.AddPatient(departmentName, doctorFullName, patientName);

                command = Console.ReadLine();
            }

            command = Console.ReadLine();

            while (command != "End")
            {
                string[] argumets = command
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                if (argumets.Length == 1)
                {
                    string departmentName = argumets[0];

                    Department department = this.hospital.Departments.FirstOrDefault(dep => dep.Name == departmentName);

                    Console.WriteLine(department);
                }
                else
                {
                    bool isRoom = int.TryParse(argumets[1], out int roomNumber);

                    if (isRoom)
                    {
                        string departmentName = argumets[0];

                        Department department = this.hospital.Departments.FirstOrDefault(dep => dep.Name == departmentName);

                        Room currentRoom = department.Rooms[roomNumber - 1];

                        Console.WriteLine(currentRoom);
                    }
                    else
                    {
                        string doctorFirstName = argumets[0];
                        string doctorLastName = argumets[1];
                        string doctorFullName = doctorFirstName + " " + doctorLastName;

                        Doctor doctor = this.hospital.Doctors.FirstOrDefault(d => d.FullName == doctorFullName);

                        Console.WriteLine(doctor);
                    }
                }

                command = Console.ReadLine();
            }
        }
    }
}
