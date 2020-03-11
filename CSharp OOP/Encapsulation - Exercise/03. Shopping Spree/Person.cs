using ShoppingSpree.Common;
using System;
using System.Collections.Generic;

namespace ShoppingSpree
{
    public class Person
    {
        private const decimal MONEY_MIN_VALUE = 0m;

        private string name;
        private decimal money;
        private List<Product> products;

        private Person()
        {
            this.products = new List<Product>();
        }

        public Person(string name, decimal money)
            : this()
        {
            this.Name = name;
            this.Money = money;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(GlobalConstants.InvalidNameExceptionMessage);
                }

                this.name = value;
            }
        }

        public decimal Money
        {
            set
            {
                if (value < MONEY_MIN_VALUE)
                {
                    throw new ArgumentException(GlobalConstants.InvalidMoneyExceptionMessage);
                }

                this.money = value;
            }
        }

        public IReadOnlyCollection<Product> Products
        {
            get
            {
                return this.products.AsReadOnly();
            }
        }

        public void BuyProduct(Product product)
        {
            if (this.money >= product.Cost)
            {
                this.money -= product.Cost;
                this.products.Add(product);
            }
            else
            {
                throw new InvalidOperationException(string.Format(GlobalConstants.InsufficientMoneyExceptionMessage, this.Name, product.Name));
            }
        }

        public override string ToString()
        {
            return this.Products.Count > 0
                ? $"{this.Name} - {string.Join(", ", this.Products)}"
                : $"{this.Name} - Nothing bought";
        }
    }
}
