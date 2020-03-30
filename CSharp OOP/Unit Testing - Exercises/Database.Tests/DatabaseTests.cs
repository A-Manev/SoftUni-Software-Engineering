using NUnit.Framework;

namespace Tests
{
    //using Database;
    using System;

    public class DatabaseTests
    {
        private Database database;

        [SetUp]
        public void Setup()
        {
            this.database = new Database(new int[] { 1, 2, 3 });
        }

        [Test]
        public void ConstructorShouldInitializeCorrectly()
        {
            Assert.That(this.database.Count, Is.EqualTo(3));
        }

        [Test]
        public void DatabaseCountShouldIncreaceWhenAddElement()
        {
            this.database.Add(4);

            Assert.That(this.database.Count, Is.EqualTo(4));
        }

        [Test]
        public void DatabaseCountShouldDecreaceWhenRemoveElement()
        {
            this.database.Remove();

            Assert.That(this.database.Count, Is.EqualTo(2));
        }

        [Test]
        public void ShouldThrowInvalidOperationExceptionWhenExceededCapacity()
        {
            for (int i = 3; i < 16; i++)
            {
                this.database.Add(i);
            }

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.database.Add(17);
            });
        }

        [Test]
        public void ShouldThrowInvalidOperationExceptionWhenRevomeFromEmptyDatabase()
        {
            for (int i = 0; i < 3; i++)
            {
                this.database.Remove();
            }

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.database.Remove();
            });
        }

        [Test]
        public void ShouldFetchDatabase()
        {
            int[] newDatabase = this.database.Fetch();

            Assert.AreEqual(new int[] { 1, 2, 3 }, newDatabase);
        }
    }
}