using System.Collections.Generic;
using System.Linq;

namespace SoftUniParking
{
    public class Parking
    {
        private List<Car> cars;
        private int capacity;

        public Parking(int capacity)
        {
            this.capacity = capacity;
            this.cars = new List<Car>();
        }

        public int Count => this.cars.Count();

        public string AddCar(Car car)
        {
            if (this.cars.Any(x => x.RegistrationNumber == car.RegistrationNumber))
            {
                return $"Car with that registration number, already exists!";
            }

            if (this.cars.Count >= this.capacity)
            {
                return $"Parking is full!";
            }

            this.cars.Add(car);
            return $"Successfully added new car {car.Make} {car.RegistrationNumber}";
        }

        public string RemoveCar(string registrationNumber)
        {
            var targetCar = this.cars.FirstOrDefault(x => x.RegistrationNumber == registrationNumber);

            if (targetCar == null )
            {
                return $"Car with that registration number, doesn't exist!";
            }

            this.cars.Remove(targetCar);

            return $"Successfully removed {targetCar.RegistrationNumber}";
        }

        public Car GetCar(string registrationNumber)
        {
            return this.cars.FirstOrDefault(x => x.RegistrationNumber == registrationNumber);
        }

        public void RemoveSetOfRegistrationNumber(List<string> registrationNumbers)
        {
            foreach (var currentNumber in registrationNumbers)
            {
                this.cars.RemoveAll(x => x.RegistrationNumber == currentNumber);
            }
        }
    }
}
