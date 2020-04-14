namespace Telecom.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class Tests
    {
        private Phone phone;

        [SetUp]
        public void SetUp()
        {
            this.phone = new Phone("Samsung", "S10");
        }

        [Test]
        public void ConstructorShouldInitializeCorrectly()
        {
            Assert.AreEqual("Samsung", this.phone.Make);
            Assert.AreEqual("S10", this.phone.Model);
        }

        [TestCase("", "S10")]
        [TestCase(null, "S10")]
        [TestCase("Samsung", "")]
        [TestCase("Samsung", null)]
        public void ConstructorShouldInitializeCorrectlySetters(string make, string model)
        {
            Assert.Throws<ArgumentException>(() => 
            {
                new Phone(make, model);
            });
        }

        [Test]
        public void TestAddContact()
        {
            this.phone.AddContact("Vladi", "088123456");

            Assert.AreEqual(1, this.phone.Count);
        }

        [Test]
        public void TestAddContactSameEx()
        {
            this.phone.AddContact("Vladi", "088123456");

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.phone.AddContact("Vladi", "088123457");
            });
        }

        [Test]
        public void CallTest()
        {
            this.phone.AddContact("Vladi", "088123456");

            this.phone.Call("Vladi");

            Assert.AreEqual("Calling Vladi - 088123456...", this.phone.Call("Vladi"));
        }

        [Test]
        public void CallTestEx()
        {

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.phone.Call("Vladi");
            });
        }
    }
}