using FightingArena;
using NUnit.Framework;
using System;

namespace Tests
{
    public class WarriorTests
    {
        private Warrior warrior;

        [SetUp]
        public void Setup()
        {
            this.warrior = new Warrior("Vladi", 50, 100);
        }

        [Test]
        public void ConstructorShouldInitializeCorrectly()
        {
            Assert.AreEqual("Vladi", this.warrior.Name);
            Assert.AreEqual(50, this.warrior.Damage);
            Assert.AreEqual(100, this.warrior.HP);
        }

        [TestCase(null, 50, 100)]
        [TestCase("", 50, 100)]
        [TestCase("     ", 50, 100)]
        [TestCase("Vladi", 0, 100)]
        [TestCase("Vladi", -10, 100)]
        [TestCase("Vladi", 10, -10)]
        public void AllPropertiesShouldThrowArgumentExceptionForInvalidValues(string name, int damage, int hp)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                new Warrior(name, damage, hp);
            });
        }

        [TestCase(25)]
        [TestCase(30)]
        public void ShouldThrowInvalidOperationExceptionWhenHpIsTooLowAndTryToAttackOtherWarriors(int attackerHp)
        {
            Warrior attacker = new Warrior("Vladi", 10 , attackerHp);
            Warrior defender = new Warrior("Oki", 10 , 40);

            Assert.Throws<InvalidOperationException>(() =>
            {
                attacker.Attack(defender);
            });
        }

        [TestCase(25)]
        [TestCase(30)]
        public void ShouldThrowInvalidOperationExceptionWhenTryToAttackEnenyWithLowThanMinHp(int defenderHp)
        {
            Warrior attacker = new Warrior("Vladi", 10, 100);
            Warrior defender = new Warrior("Oki", 10, defenderHp);

            Assert.Throws<InvalidOperationException>(() =>
            {
                attacker.Attack(defender);
            });
        }

        [Test]
        public void ShouldThrowInvalidOperationExceptionWhenTryToAttackTooStrongerEnemy()
        {
            Warrior attacker = new Warrior("Vladi", 10, 35);
            Warrior defender = new Warrior("Oki", 40, 35);

            Assert.Throws<InvalidOperationException>(() =>
            {
                attacker.Attack(defender);
            });
        }

        [Test]
        public void BothWarriorShouldTakeDamageWhenAttackEachOther()
        {
            Warrior attacker = new Warrior("Vladi", 10, 40);
            Warrior defender = new Warrior("Oki", 5, 50);

            attacker.Attack(defender);

            Assert.AreEqual(35, attacker.HP);
            Assert.AreEqual(40, defender.HP);
        }

        [Test]
        public void WarriorShouldTakeDamageWhenAttackEnemyAndIfEnemyWarrirDieHisHpShouldBeZero()
        {
            Warrior attacker = new Warrior("Vladi", 80 , 100);
            Warrior defender = new Warrior("Oki", 10, 60);

            attacker.Attack(defender);

           Assert.AreEqual(90, attacker.HP);
            Assert.AreEqual(0, defender.HP);
        }
    }
}
