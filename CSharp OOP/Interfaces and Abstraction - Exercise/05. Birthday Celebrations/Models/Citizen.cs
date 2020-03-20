using System;
using BirthdayCelebrations.Interfaces;

namespace BirthdayCelebrations.Models
{
    public class Citizen : IIdentifiable, IBirthable, IName
    {
        private int age;

        public Citizen(string name, int age, string id, string birthdate)
        {
            this.Name = name;
            this.age = age;
            this.Id = id;
            this.Birthdate = DateTime.ParseExact(birthdate, "dd/mm/yyyy", null);
        }

        public string Name { get; private set; }

        public string Id { get; private set; }

        public DateTime Birthdate { get; private set; }
    }
}
