using System.Linq;
using System.Collections.Generic;

namespace Hospital
{
    public class Hospital
    {
        public List<Doctor> Doctors { get; set; }

        public List<Department> Departments { get; set; }

        public Hospital()
        {
            this.Doctors = new List<Doctor>();
            this.Departments = new List<Department>();
        }

        public void AddDoctor(string firstName, string lastName)
        {
            if (!this.Doctors.Any(d => d.FirstName == firstName && d.LastName == lastName))
            {
                Doctor doctor = new Doctor(firstName, lastName);

                this.Doctors.Add(doctor);
            }
        }

        public void AddDepartment(string name)
        {
            if (!this.Departments.Any(dep => dep.Name == name))
            {
                Department department = new Department(name);

                this.Departments.Add(department);
            }
        }

        public void AddPatient(string departmentName, string doctorFullName, string patientName)
        {
            Doctor doctor = this.Doctors.FirstOrDefault(d => d.FullName == doctorFullName);

            Department department = this.Departments.FirstOrDefault(dep => dep.Name == departmentName);

            Patient patient = new Patient(patientName);

            department.AddPatientToRoom(patient);

            doctor.Patients.Add(patient);
        }
    }
}
