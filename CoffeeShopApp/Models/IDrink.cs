using System;

namespace CoffeeShopApp.Models
{
    public interface IDrink
    {
        string Name { get; }
        string Description { get; }
        decimal Price { get; }
        DrinkStatus Status { get; }

        void SetStatus(DrinkStatus status);
    }
}
