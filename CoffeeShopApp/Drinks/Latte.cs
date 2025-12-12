using CoffeeShopApp.Models;

namespace CoffeeShopApp.Drinks
{
    public class Latte : CompositeDrink
    {
        public Latte(bool large) : base("Latte")
        {
            AddIngredient(new EspressoShot());
            AddIngredient(new SteamedMilk());
            AddIngredient(new MilkFoam());
            AddIngredient(large ? new SizeLarge() as Ingredient : new SizeSmall());
        }
    }
}
