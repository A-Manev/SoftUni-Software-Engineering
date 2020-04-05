namespace BlueOrigin.Tests
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class SpaceshipTests
    {
        private Astronaut astronaut;
        private Spaceship spaceship;

        [SetUp]
        public void Setup()
        {
            this.astronaut = new Astronaut("Vladi", 50);
            this.spaceship = new Spaceship("Krg", 100);
        }

        [Test]
        public void AstronautConstructorShouldInitializeCorrectly()
        {
            Assert.AreEqual("Vladi", this.astronaut.Name);
            Assert.AreEqual(50, this.astronaut.OxygenInPercentage);
        }

        [Test]
        public void SpaceshipConstructorShouldInitializeCorrectly()
        {
            Assert.AreEqual("Krg", this.spaceship.Name);
            Assert.AreEqual(100, this.spaceship.Capacity);
        }

        [TestCase("")]
        [TestCase(null)]
        public void ShouldThrowArgumentNullExceptionIfAddedSpaceshipNameIsNull(string name)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new Spaceship(name, 100);
            });
        }

        [Test]
        public void ShouldThrowArgumentExceptionIfCapacityIsBelowZero()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                new Spaceship("Test", -10);
            });
        }

        [Test]
        public void CountShouldIncreaseWhenAstronautIsAddedToSpaceship()
        {
            this.spaceship.Add(this.astronaut);

            Assert.AreEqual(1, this.spaceship.Count);
        }

        [Test]
        public void ShouldThrowInvalidOperationExceptionIfCapacityIsNotEnough()
        {
            Spaceship AnouterSpaceship = new Spaceship("Krg", 0);

            Assert.Throws<InvalidOperationException>(() =>
            {
                AnouterSpaceship.Add(this.astronaut);
            });
        }

        [Test]
        public void ShouldThrowInvalidOperationExceptionIfAstronautExist()
        {
            this.spaceship.Add(this.astronaut);

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.spaceship.Add(this.astronaut);
            });
        }

        [Test]
        public void TestRemovingAstronaut()
        {
            this.spaceship.Add(this.astronaut);

            Assert.IsTrue(this.spaceship.Remove(this.astronaut.Name));
        }
    }
}