using System;
using NUnit.Framework;

namespace TheRace.Tests
{
    [TestFixture]
    public class RaceEntryTests
    {
        private UnitMotorcycle unitMotorcycle;
        private UnitRider unitRider;
        private RaceEntry race;

        [SetUp]
        public void Setup()
        {
            this.unitMotorcycle = new UnitMotorcycle("Honda", 100, 50);
            this.unitRider = new UnitRider("Pesho", this.unitMotorcycle);
            this.race = new RaceEntry();
        }

        [Test]
        public void UnitMotorcycleConstructorShouldInitializeCorrectly()
        {
            Assert.AreEqual("Honda", this.unitMotorcycle.Model);
            Assert.AreEqual(100, this.unitMotorcycle.HorsePower);
            Assert.AreEqual(50, this.unitMotorcycle.CubicCentimeters);
        }

        [Test]
        public void UnitRiderConstructorShouldInitializeCorrectly()
        {
            Assert.AreEqual("Pesho", this.unitRider.Name);
            Assert.AreEqual("Honda", this.unitRider.Motorcycle.Model);
            Assert.AreEqual(100, this.unitRider.Motorcycle.HorsePower);
            Assert.AreEqual(50, this.unitRider.Motorcycle.CubicCentimeters);
        }

        [Test]
        public void NamePropertyShouldThrowArgumentNullExceptionWhenValueIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new UnitRider(null, this.unitMotorcycle);
            });
        }

        [Test]
        public void ShouldThrowInvalidOperationExceptionWhenRiderIsNull()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                this.race.AddRider(null);
            });
        }

        [Test]
        public void ShouldThrowInvalidOperationExceptionWhenRiderAlredyExist()
        {
            this.race.AddRider(this.unitRider);

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.race.AddRider(this.unitRider);
            });
        }

        [Test]
        public void TestAddingRaiderAndCountIncrease()
        {
            Assert.AreEqual($"Rider {this.unitRider.Name} added in race.",
                this.race.AddRider(this.unitRider));

            Assert.AreEqual(1, this.race.Counter);
        }

        [Test]
        public void ShouldThrowInvalidOperationExceptionWhenIfRaseStartWithUnderTwoRiders()
        {
            this.race.AddRider(this.unitRider);

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.race.CalculateAverageHorsePower();
            });
        }

        [Test]
        public void ShouldCalculateAverageHorsePower()
        {
            UnitMotorcycle motorcycleSuzuki = new UnitMotorcycle("Suzuki", 100, 50);
            UnitRider riderGosho = new UnitRider("Gosho", motorcycleSuzuki);

            UnitMotorcycle motorcycleKawasaki = new UnitMotorcycle("Kawasaki", 100, 50);
            UnitRider riderStoyan= new UnitRider("Stoyan", motorcycleKawasaki);

            this.race.AddRider(this.unitRider);
            this.race.AddRider(riderGosho);
            this.race.AddRider(riderStoyan);

            Assert.AreEqual(100, this.race.CalculateAverageHorsePower());
        }
    }
}