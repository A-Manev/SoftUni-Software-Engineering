namespace Presents.Tests
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    [TestFixture]
    public class PresentsTests
    {
        private Present present;
        private Bag bag;

        [SetUp]
        public void SetUp()
        {
            this.present = new Present("Test", 20);
            this.bag = new Bag();
        }

        [Test]
        public void PresentConstructorShouldInitializeCorrectly()
        {
            Assert.AreEqual("Test", this.present.Name);
            Assert.AreEqual(20, this.present.Magic);
        }

        [Test]
        public void SettersTest()
        {
            this.present.Name = "Gosho";
            this.present.Magic = 30;

            Assert.AreEqual("Gosho", this.present.Name);
            Assert.AreEqual(30, this.present.Magic);
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWhenCreatedPresenIsNull()
        {
            Assert.Throws<ArgumentNullException>(()=>
            {
                this.bag.Create(null);
            });
        }

        [Test]
        public void ShouldThrowInvalidOperationExceptionWhenTryToAddAlredyExistingPresent()
        {
            this.bag.Create(this.present);

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.bag.Create(this.present);
            });
        }

        [Test]
        public void TestSuccessfullyAddedPresent()
        {
            Assert.AreEqual($"Successfully added present {this.present.Name}.", this.bag.Create(this.present));
        }

        [Test] 
        public void ShouldReturnRemovedPresent()
        {
            this.bag.Create(this.present);

            Assert.IsTrue(this.bag.Remove(this.present));
        }

        [Test]
        public void TestGetPresentWithLeastMagic()
        {
            this.bag.Create(this.present);
            this.bag.Create(new Present("Test2", 30));

            Assert.AreEqual(this.present, this.bag.GetPresentWithLeastMagic());
        }

        [Test]
        public void TestGetPresentWithCurrentName()
        {
            this.bag.Create(this.present);

            Assert.AreEqual(this.present, this.bag.GetPresent("Test"));
        }

        [Test]
        public void CountShouldIncreaseWhenPresentIsAddedSuccessfully()
        {
            this.bag.Create(this.present);

            var test = new List<Present>();

            test.Add(this.present);

            CollectionAssert.AreEqual(test, this.bag.GetPresents());
        }
    }
}
