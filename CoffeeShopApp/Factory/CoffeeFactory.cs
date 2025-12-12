using CoffeeShopApp.Drinks;
using CoffeeShopApp.Models;
using System;

namespace CoffeeShopApp.Factory
{
    public class CoffeeFactory : ICoffeeFactory
    {
        public IDrink CreateDrink(string type, bool large)
        {
            return type switch
            {
                "Latte" => new Latte(large),
                "Americano" => new Americano(large),
                "Brewed Coffee" => new BrewedCoffee(large),
                _ => throw new ArgumentException("Unknown drink type")
            };
        }
    }
}
