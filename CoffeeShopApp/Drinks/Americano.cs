using CoffeeShopApp.Models;

namespace CoffeeShopApp.Drinks
{
    public class Americano : CompositeDrink
    {
        public Americano(bool large) : base("Americano")
        {
            AddIngredient(new EspressoShot());
            AddIngredient(new HotWater());
            AddIngredient(large ? new SizeLarge() as Ingredient : new SizeSmall());
        }
    }
}
