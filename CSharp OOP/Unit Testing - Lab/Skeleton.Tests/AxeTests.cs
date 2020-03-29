using NUnit.Framework;

[TestFixture]
public class AxeTests
{
    private const int AxeAttack = 10;
    private const int AxeDurability = 10;
    private const int DummyHealth = 10;
    private const int DummyExperience = 10;

    private Axe axe;
    private Dummy dummy;

    [SetUp]
    public void CreateAxeAndDummy()
    {
        this.axe = new Axe(AxeAttack, AxeDurability);
        this.dummy = new Dummy(DummyHealth, DummyExperience);
    }

    [Test]
    public void AxeLooseDurabilityAfterAttack()
    {
        this.axe.Attack(this.dummy);

        Assert.That(axe.DurabilityPoints, Is.EqualTo(9), "Axe Durability doesn't change after attack.");
    }

    [Test]

    public void ABrokenAxeCantAttack()
    {
        Axe axe = new Axe(AxeAttack, 1);

        axe.Attack(this.dummy);

        Assert.That(() => axe.Attack(this.dummy),Throws.InvalidOperationException.With.Message.EqualTo("Axe is broken."));
    }
}