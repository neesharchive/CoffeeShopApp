namespace CoffeeShopApp.Models
{
    public interface IDrinkSubject
    {
        void Attach(IDrinkObserver observer);
        void Detach(IDrinkObserver observer);
        void Notify();
    }
}
