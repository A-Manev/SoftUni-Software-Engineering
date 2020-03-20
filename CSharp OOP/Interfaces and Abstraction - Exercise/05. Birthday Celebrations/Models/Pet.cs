using System;
using BirthdayCelebrations.Interfaces;

namespace BirthdayCelebrations.Models
{
    public class Pet : IName , IBirthable
    {
        public Pet(string name, string birthdate)
        {
            this.Name = name;
            this.Birthdate = DateTime.ParseExact(birthdate, "dd/mm/yyyy", null);
        }

        public string Name { get; private set; }

        public DateTime Birthdate { get; private set; }
    }
}
