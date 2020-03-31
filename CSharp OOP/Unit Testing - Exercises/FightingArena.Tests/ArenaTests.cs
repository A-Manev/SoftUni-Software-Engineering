using FightingArena;
using NUnit.Framework;
using System;

namespace Tests
{
    public class ArenaTests
    {
        private Arena arena;
        private Warrior warrior;
        private Warrior attackerWarrior;
        private Warrior defenderWarrior;

        [SetUp]
        public void Setup()
        {
            this.arena = new Arena();
            this.warrior = new Warrior("Vladi", 50, 100);
            this.attackerWarrior = new Warrior("Vladi", 10, 80);
            this.defenderWarrior = new Warrior("Oki", 5, 60);
        }

        [Test]
        public void TestIfConstructorWorksCorrectly()
        {
            Assert.IsNotNull(this.arena.Warriors);
        }

        [Test]
        public void ArenaEnrollTest()
        {
            this.arena.Enroll(warrior);

            Assert.That(this.arena.Warriors, Has.Member(warrior));
        }

        [Test]
        public void EnrollShouldIncreaseCount()
        {
            this.arena.Enroll(warrior);
            this.arena.Enroll(new Warrior("Oki", 50, 100));

            Assert.AreEqual(2, this.arena.Count);
            Assert.AreEqual(2, this.arena.Warriors.Count);
        }

        [Test]
        public void ShouldThrowInvalidOperationExceptionIfWarriorIsAlredyEnrolled()
        {
            this.arena.Enroll(warrior);

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.arena.Enroll(warrior);
            });
        }

        [Test]
        public void ShouldThrowInvalidOperationExceptionIfTryToAddWarriorWithSameParams()
        {
            this.arena.Enroll(warrior);

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.arena.Enroll(new Warrior("Vladi", 50, 100));
            });
        }    

        [Test]
        public void ShouldThrowInvalidOperationExceptionWhenAttackerDoesntExist()
        {
            this.arena.Enroll(this.defenderWarrior);

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.arena.Fight(this.attackerWarrior.Name, this.defenderWarrior.Name);
            });
        }

        [Test]
        public void ShouldThrowInvalidOperationExceptionWhenDefenderDoesntExist()
        {
            this.arena.Enroll(this.attackerWarrior);

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.arena.Fight(this.attackerWarrior.Name, this.defenderWarrior.Name);
            });
        }

        [Test]
        public void SuccessfulFight()
        {
            this.arena.Enroll(this.attackerWarrior);
            this.arena.Enroll(this.defenderWarrior);

            this.arena.Fight(this.attackerWarrior.Name, this.defenderWarrior.Name);

            this.attackerWarrior.Attack(this.defenderWarrior);

            Assert.AreEqual(40, this.defenderWarrior.HP); // 40 ??? 
            Assert.AreEqual(70, this.attackerWarrior.HP); // 70 ???
        }
    }
}
