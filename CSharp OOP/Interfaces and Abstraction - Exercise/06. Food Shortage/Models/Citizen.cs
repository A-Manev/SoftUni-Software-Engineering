using System;

using FoodShortage.Interfaces;

namespace FoodShortage.Models
{
    public class Citizen : IIdentifiable, IBirthable, IName, IBuyer, IAge
    {
        public Citizen(string name, int age, string id, string birthdate)
        {
            this.Name = name;
            this.Age = age;
            this.Id = id;
            this.Birthdate = DateTime.ParseExact(birthdate, "dd/mm/yyyy", null);
        }

        public string Name { get; private set; }

        public int Age { get; set; }

        public string Id { get; private set; }

        public DateTime Birthdate { get; private set; }

        public int Food { get; private set; }

        public void BuyFood()
        {
            this.Food += 10;
        }
    }
}
