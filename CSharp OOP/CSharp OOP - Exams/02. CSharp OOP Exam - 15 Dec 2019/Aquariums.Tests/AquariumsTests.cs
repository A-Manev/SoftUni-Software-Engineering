namespace Aquariums.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class AquariumsTests
    {
        private Fish fish;
        private Fish anotherFish;
        private Aquarium aquarium;

        [SetUp]
        public void SetUp()
        {
            this.fish = new Fish("Pesho");
            this.anotherFish = new Fish("Gosho");
            this.aquarium = new Aquarium("Vladi", 100);
        }

        [Test]
        public void FishConstructorShouldInitializeCorrectly()
        {
            Assert.AreEqual("Pesho", this.fish.Name);
            Assert.IsTrue(this.fish.Available);
        }

        [Test]
        public void FishConstructorShouldSetCorrectly()
        {
            this.fish.Name = "Gosho";
            this.fish.Available = false;

            Assert.AreEqual("Gosho", this.fish.Name);
            Assert.IsFalse(this.fish.Available);
        }

        [Test]
        public void AquariumConstructorShouldInitializeCorrectly()
        {
            Assert.AreEqual("Vladi", this.aquarium.Name);
            Assert.AreEqual(100, this.aquarium.Capacity);
        }

        [TestCase(null, 100)]
        [TestCase("", 100)]
        public void NamePropertieShouldThrowArgumentNullExceptionForInvalidValue(string name, int capacity)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new Aquarium(name, capacity);
            });
        }

        [Test]
        public void CapacityPropertieShouldThrowArgumentExceptionForInvalidValue()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                new Aquarium("Vladi", -10);
            });
        }

        [Test]
        public void AquariumCountShouldIncreaseWhenAddFish()
        {
            this.aquarium.Add(this.fish);
            this.aquarium.Add(this.anotherFish);

            Assert.AreEqual(2, this.aquarium.Count);
        }

        [Test]
        public void AquariumShouldThrowInvalidOperationExceptionWhenCapacityIsReached()
        {
            Aquarium OneMoreAquarium = new Aquarium("Test", 1);

            OneMoreAquarium.Add(this.fish);

            Assert.Throws<InvalidOperationException>(() =>
            {
                OneMoreAquarium.Add(this.anotherFish);
            });
        }

        [Test]
        public void AquariumShouldDecreaseCountWhenFishIsRemoved()
        {
            this.aquarium.Add(this.fish);
            this.aquarium.Add(this.anotherFish);

            this.aquarium.RemoveFish("Pesho");

            Assert.AreEqual(1, this.aquarium.Count);
        }

        [Test]
        public void AquariumShouldThrowInvalidOperationExceptionWhenTryToRemoveNonExistentFish()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                this.aquarium.RemoveFish("Pesho");
            });
        }

        [Test]
        public void TestSellingFish()
        {
            this.aquarium.Add(this.fish); // "Pesho"
            this.aquarium.Add(this.anotherFish);

            var selledFish = this.aquarium.SellFish("Pesho");

           Assert.AreEqual(this.fish, selledFish);
            Assert.IsFalse(this.fish.Available);
        }

        [Test]
        public void AquariumShouldThrowInvalidOperationExceptionWhenTryToSellNonExistentFish()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                this.aquarium.SellFish("Pesho");
            });
        }

        [Test]
        public void TestReport()
        {
            this.aquarium.Add(this.fish);
           var report = this.aquarium.Report();

            Assert.AreEqual($"Fish available at {this.aquarium.Name}: {this.fish.Name}", report);
        }
    }
}
