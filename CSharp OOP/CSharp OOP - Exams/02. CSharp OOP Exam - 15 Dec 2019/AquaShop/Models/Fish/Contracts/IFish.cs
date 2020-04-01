namespace AquaShop.Models.Fish.Contracts
{
    using System;

    public interface IFish
    {
        string Name { get; }

        string Species { get; } //ВИД

        int Size { get; }

        decimal Price { get; }

        void Eat();
    }
}
