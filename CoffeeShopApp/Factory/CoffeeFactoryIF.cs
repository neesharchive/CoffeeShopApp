using CoffeeShopApp.Models;

namespace CoffeeShopApp.Factory
{
    public interface ICoffeeFactory
    {
        IDrink CreateDrink(string type, bool large);
    }
}
