using System;
using NUnit.Framework;

[TestFixture]
public class HeroRepositoryTests
{
    private Hero hero;
    private HeroRepository heroRepository;

    [SetUp]
    public void SetUp()
    {
        this.hero = new Hero("Gosho", 100);
        this.heroRepository = new HeroRepository();
    }

    [Test]
    public void HeroConstructorShouldSetCorrectly()
    {
        Assert.AreEqual("Gosho", this.hero.Name);
        Assert.AreEqual(100, this.hero.Level);
    }

    [Test]
    public void ShouldThrowArgumentNullExceptionWhenCreatedHeroIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => 
        {
            this.heroRepository.Create(null);
        });
    }

    [Test]
    public void ShouldThrowInvalidOperationExceptionWhenTrytoAddAlreadyExistingHero()
    {
        this.heroRepository.Create(this.hero);

        Assert.Throws<InvalidOperationException>(() =>
        {
            this.heroRepository.Create(new Hero("Gosho", 50));
        });
    }

    [Test]
    public void ShouldReturnMessageWhenHeroIsAddedSucsefuly()
    {
        Assert.AreEqual($"Successfully added hero {this.hero.Name} with level {this.hero.Level}", this.heroRepository.Create(this.hero));
    }

    [TestCase(null)]
    [TestCase("")]
    [TestCase("   ")]
    public void ShouldThrowArgumentNullExceptionWhenHeroIsNullOrWhiteSpaceOrEmpty(string name)
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
            this.heroRepository.Remove(name);
        });
    }

    [Test]
    public void ShouldReturnTrueIfRemovedHeroIsFinded()
    {
        this.heroRepository.Create(this.hero);

        Assert.IsTrue(this.heroRepository.Remove("Gosho"));
    }

    [Test]
    public void TestGetHeroWithHighestLevel()
    {
        this.heroRepository.Create(this.hero);
        this.heroRepository.Create(new Hero("Pesho", 50));

        Assert.AreEqual(this.hero, this.heroRepository.GetHeroWithHighestLevel());
    }

    [Test]
    public void TestGetHero()
    {
        this.heroRepository.Create(this.hero);

        Assert.AreEqual(this.hero, this.heroRepository.GetHero("Gosho"));
    }

    [Test]
    public void ShouldReturnCountOfAllHeros()
    {
        this.heroRepository.Create(this.hero);
        this.heroRepository.Create(new Hero("Pesho", 50));

        Assert.AreEqual(2, this.heroRepository.Heroes.Count);
    }
}