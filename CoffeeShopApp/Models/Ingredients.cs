namespace CoffeeShopApp.Models
{
    // Leaf ingredients used in the Composite
    public class EspressoShot : Ingredient
    {
        public EspressoShot() : base("Espresso Shot", 1.50m) { }
    }

    public class SteamedMilk : Ingredient
    {
        public SteamedMilk() : base("Steamed Milk", 0.75m) { }
    }

    public class MilkFoam : Ingredient
    {
        public MilkFoam() : base("Milk Foam", 0.50m) { }
    }

    public class HotWater : Ingredient
    {
        public HotWater() : base("Hot Water", 0.25m) { }
    }

    public class SizeSmall : Ingredient
    {
        public SizeSmall() : base("Small Size", 0.00m) { }
    }

    public class SizeLarge : Ingredient
    {
        public SizeLarge() : base("Large Size", 0.75m) { }
    }
}
