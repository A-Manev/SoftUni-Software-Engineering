using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Hospital
{
    public class Doctor
    {
        public Doctor(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Patients = new List<Patient>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => this.FirstName + " " + this.LastName;

        public List<Patient> Patients { get; set; }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var patient in this.Patients.OrderBy(p=>p.Name))
            {
                stringBuilder.AppendLine(patient.Name);
            }

            return stringBuilder.ToString().TrimEnd();
        }
    }
}
