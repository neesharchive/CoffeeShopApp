namespace CoffeeShopApp.Models
{
    public interface IDrinkObserver
    {
        void OnStatusChanged(IDrink drink);
    }
}
