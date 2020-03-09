using System;

namespace PersonsInfo
{
    public class Person
    {
        private const int MINIMUM_NAME_LENGTH = 3;
        private const decimal MINIMUN_SALARY = 460;

        private string firstName;
        private string lastName;
        private int age;
        private decimal salary;

        public Person(string firstName, string lastName, int age, decimal salary)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Age = age;
            this.Salary = salary;
        }

        public string FirstName
        {
            get
            {
                return this.firstName;
            }
            private set
            {
                if (value.Length < MINIMUM_NAME_LENGTH)
                {
                    throw new ArgumentException($"First name cannot contain fewer than {MINIMUM_NAME_LENGTH} symbols!");
                }

                this.firstName = value;
            }
        }

        public string LastName
        {
            get
            {
                return this.lastName;
            }
            private set
            {
                if (value.Length < MINIMUM_NAME_LENGTH)
                {
                    throw new ArgumentException($"Last name cannot contain fewer than {MINIMUM_NAME_LENGTH} symbols!");
                }

                this.lastName = value;
            }
        }

        public int Age
        {
            get
            {
                return this.age;
            }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Age cannot be zero or a negative integer!");
                }

                this.age = value;
            }
        }

        public decimal Salary 
        {
            get
            {
                return this.salary;
            }
            private set
            {
                if (value < MINIMUN_SALARY)
                {
                    throw new ArgumentException($"Salary cannot be less than {MINIMUN_SALARY} leva!");
                }

                this.salary = value;
            }
        }

        public void IncreaseSalary(decimal percentage)
        {
            if (this.Age < 30)
            {
                percentage /= 2;
            }

            this.Salary *= 1 + (percentage / 100);
        }

        public override string ToString()
        {
            return $"{this.FirstName} {this.LastName} gets {this.Salary:F2} leva.";
        }
    }
}
