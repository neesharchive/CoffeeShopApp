namespace CoffeeShopApp.Models
{
    public class ExtraShotDecorator : DrinkDecorator
    {
        public ExtraShotDecorator(IDrink inner) : base(inner) { }

        public override string Description =>
            InnerDrink.Description + ", Extra Espresso Shot";

        public override decimal Price => InnerDrink.Price + 1.00m;
    }

    public class SyrupDecorator : DrinkDecorator
    {
        public SyrupDecorator(IDrink inner) : base(inner) { }

        public override string Description =>
            InnerDrink.Description + ", Syrup";

        public override decimal Price => InnerDrink.Price + 0.75m;
    }

    public class WhippedCreamDecorator : DrinkDecorator
    {
        public WhippedCreamDecorator(IDrink inner) : base(inner) { }

        public override string Description =>
            InnerDrink.Description + ", Whipped Cream";

        public override decimal Price => InnerDrink.Price + 0.50m;
    }
}
