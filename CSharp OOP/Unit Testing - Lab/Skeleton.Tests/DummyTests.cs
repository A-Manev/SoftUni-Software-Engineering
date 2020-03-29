using NUnit.Framework;

[TestFixture]
public class DummyTests
{
    [Test]
    public void DummyLosesHealthAfterAttack()
    {
        //Arrange
        Dummy dummy = new Dummy(10, 10);

        //Act
        dummy.TakeAttack(5);

        //Assert
        Assert.That(dummy.Health, Is.EqualTo(5));
    }

    [Test]
    public void DummyCannotAttack()
    {
        Dummy dummy = new Dummy(0, 0);

        dummy.IsDead();

        Assert.That(() => dummy.TakeAttack(5),
            Throws.InvalidOperationException.With.Message.EqualTo("Dummy is dead."));
    }

    [Test]
    public void DummyGiveXPWhenDie()
    {
        Dummy dummy = new Dummy(10, 10);

        dummy.TakeAttack(10);

        Assert.That(dummy.GiveExperience, Is.EqualTo(10));
    }

    [Test]
    public void DummyCannotGiveXPIfIsDead()
    {
        Dummy dummy = new Dummy(20, 10);

        dummy.TakeAttack(10);

        Assert.That(() => dummy.GiveExperience(), 
            Throws.InvalidOperationException.With.Message.EqualTo("Target is not dead."));
    }
}
