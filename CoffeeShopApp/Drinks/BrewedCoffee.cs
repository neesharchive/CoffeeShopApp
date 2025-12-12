using CoffeeShopApp.Models;

namespace CoffeeShopApp.Drinks
{
    public class BrewedCoffee : CompositeDrink
    {
        public BrewedCoffee(bool large) : base("Brewed Coffee")
        {
            AddIngredient(new HotWater());
            // imagine ground coffee is included in base cost
            AddIngredient(large ? new SizeLarge() as Ingredient : new SizeSmall());
        }
    }
}
