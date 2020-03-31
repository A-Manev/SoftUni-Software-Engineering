using NUnit.Framework;

namespace Tests
{
    using ExtendedDatabase;
    using System;

    public class ExtendedDatabaseTests
    {
        private Person person;
        private ExtendedDatabase extendedDatabase;
        private Person gosho;
        private Person pesho;

        [SetUp]
        public void Setup()
        {
            this.person = new Person(1213123132132332353, "username");
            this.extendedDatabase = new ExtendedDatabase();
            this.gosho = new Person(123, "Gosho");
            this.pesho = new Person(321, "Pesho");
        }

        [Test]
        public void ConstructorShouldInitializeCorrectlyPersonClass()
        {
            Assert.That(this.person.UserName, Is.Not.EqualTo(null));
            Assert.AreEqual("username", this.person.UserName);
            Assert.AreEqual(1213123132132332353, this.person.Id);
        }

        [Test] // Add
        public void TestAddnigPeople()
        {
            this.extendedDatabase.Add(gosho);
            this.extendedDatabase.Add(pesho);

            Assert.AreEqual(2, this.extendedDatabase.Count);
        }

        [Test]
        public void ShouldThrowInvalidOperationExceptionWhenArrayCapacityIsReached()
        {
            Person[] persons = new Person[16];

            for (int i = 0; i < persons.Length; i++)
            {
                persons[i] = new Person(i, $"A{i}");
            }

            ExtendedDatabase newExtendedDatabase = new ExtendedDatabase(persons);
            Person newPerson = new Person(12233, "Vladi");

            Assert.Throws<InvalidOperationException>(() =>
            {
                newExtendedDatabase.Add(newPerson);
            });
        }

        [Test]
        public void ShouldThrowInvalidOperationExceptionWhenAddPersonWithSameUsername()
        {
            Person[] persons = new Person[] { pesho, gosho };
            ExtendedDatabase newExtendedDatabase = new ExtendedDatabase(persons);
            Person newPerson = new Person(16523, "Gosho");

            Assert.Throws<InvalidOperationException>(() =>
            {
                newExtendedDatabase.Add(newPerson);
            });
        }

        [Test]
        public void ShouldThrowInvalidOperationExceptionWhenThisIdAlreadyExist()
        {
            Person[] persons = new Person[] { pesho, gosho };
            ExtendedDatabase newExtendedDatabase = new ExtendedDatabase(persons);
            Person newPerson = new Person(123, "Stamat");

            Assert.Throws<InvalidOperationException>(() =>
            {
                newExtendedDatabase.Add(newPerson);
            });
        }

        [Test] // Remove
        public void CoundShouldDecreaseWhenRemoveSomeone()
        {
            this.extendedDatabase.Add(gosho);
            this.extendedDatabase.Add(pesho);

            this.extendedDatabase.Remove();

            Assert.AreEqual(1, this.extendedDatabase.Count);
        }

        [Test]
        public void ShouldThrowInvalidOperationExceptionWhenTryToRemoveForEmptyColection()
        {
            Assert.Throws<InvalidOperationException>(()=>
            {
                this.extendedDatabase.Remove();
            });
        }

        [Test] // FindByUsername
        public void TestFindUserByUsername()
        {
            this.extendedDatabase.Add(gosho);
            var findedPerson = this.extendedDatabase.FindByUsername("Gosho");

            Assert.AreEqual(this.gosho.UserName, findedPerson.UserName);
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWhenTryToFindNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                this.extendedDatabase.FindByUsername(null);
            });
        }

        [Test]
        public void ShouldThrowInvalidOperationExceptionWhenUserDoesntExist()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                var findedPerson = this.extendedDatabase.FindByUsername("Niki");
            });
        }

        [Test] // FindById
        public void TestFindUserById()
        {
            this.extendedDatabase.Add(gosho);
            var findedPerson = this.extendedDatabase.FindById(123);

            Assert.AreEqual(this.gosho.Id, findedPerson.Id);
        }

        [Test]
        public void ShouldThrowArgumentOutOfRangeExceptionWhenUserIdIsBelowZero()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                 this.extendedDatabase.FindById(-10);
            });
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWhenUserDoesntExist()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                var findedPerson = this.extendedDatabase.FindById(666);
            });
        }
    }
}