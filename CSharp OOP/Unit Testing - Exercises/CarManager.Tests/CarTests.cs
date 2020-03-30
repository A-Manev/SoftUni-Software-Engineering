//using CarManager;
using NUnit.Framework;
using System;

namespace Tests
{
    public class CarTests
    {
        private Car car;

        [SetUp]
        public void Setup()
        {
            this.car = new Car("VW", "Golf", 3, 10);
        }

        [Test]
        public void ConstructorShouldInitializeCorrectly()
        {
            Assert.AreEqual("VW", this.car.Make);
            Assert.AreEqual("Golf", this.car.Model);
            Assert.AreEqual(3, this.car.FuelConsumption);
            Assert.AreEqual(10, this.car.FuelCapacity);
        }

        [TestCase(null, "Golf", 3, 2)]
        [TestCase("", "Golf", 3, 2)]
        [TestCase("VW", null, 3, 2)]
        [TestCase("VW", "", 3, 2)]
        [TestCase("VW", "Golf", 0, 2)]
        [TestCase("VW", "Golf", -2, 2)]
        [TestCase("VW", "Golf", 2, 0)]
        [TestCase("VW", "Golf", 2, -2)]
        public void AllPropertiesShouldThrowArgumentExceptionForInvalidValues(string make, string model, double fuelConsumption, double fuelCapacity)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                new Car(make, model, fuelConsumption, fuelCapacity);
            });
        }

        [Test]
        public void ShouldRefuelNormally()
        {
            this.car.Refuel(10);
            Assert.AreEqual(10, this.car.FuelAmount);
        }

        [Test]
        public void ShouldRefuelUntilTheTotalFuelCapacity()
        {
            this.car.Refuel(15);
            Assert.AreEqual(10, this.car.FuelAmount);
        }
        
        [TestCase(0)]
        [TestCase(-2)]
        public void RefuelShouldThrowArgumentExceptionWhenInputIsZeroOrBellow(double amount)
        {

            Assert.Throws<ArgumentException>(() => this.car.Refuel(amount));
        }

        [Test] 
        public void ShouldDriveNormally()
        {
            this.car.Refuel(50);
            this.car.Drive(50);

            Assert.AreEqual(8.5, this.car.FuelAmount);
        }

        [TestCase(10000)]
        public void DvireShouldThrowInvalidOperationExceptionWhenDistanIsTooBug(double distance)
        {
            Assert.Throws<InvalidOperationException>(() => this.car.Drive(distance));
        }
    }
}