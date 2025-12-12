using System.Collections.Generic;
using System.Linq;

namespace CoffeeShopApp.Models
{
    public class CompositeDrink : IDrink, IDrinkSubject
    {
        private readonly List<Ingredient> _ingredients = new();
        private readonly List<IDrinkObserver> _observers = new();

        public string Name { get; protected set; }
        public DrinkStatus Status { get; private set; } = DrinkStatus.Created;

        public CompositeDrink(string name)
        {
            Name = name;
        }

        public void AddIngredient(Ingredient ingredient)
        {
            _ingredients.Add(ingredient);
        }

        public void RemoveIngredient(Ingredient ingredient)
        {
            _ingredients.Remove(ingredient);
        }

        public IEnumerable<Ingredient> Ingredients => _ingredients;

        public decimal Price => _ingredients.Sum(i => i.Price);

        public string Description =>
            string.Join(", ", _ingredients.Select(i => i.Description));

        public void SetStatus(DrinkStatus status)
        {
            Status = status;
            Notify();
        }

        // Observer / Subject
        public void Attach(IDrinkObserver observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);
        }

        public void Detach(IDrinkObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (var obs in _observers)
            {
                obs.OnStatusChanged(this);
            }
        }
    }
}
