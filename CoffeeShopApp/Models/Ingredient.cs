namespace CoffeeShopApp.Models
{
    public abstract class Ingredient
    {
        public string Description { get; protected set; } = "";
        public decimal Price { get; protected set; }

        protected Ingredient(string description, decimal price)
        {
            Description = description;
            Price = price;
        }
    }
}
