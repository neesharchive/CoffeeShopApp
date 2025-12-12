namespace CoffeeShopApp.Models
{
    public abstract class DrinkDecorator : IDrink, IDrinkSubject
    {
        protected IDrink InnerDrink;

        protected DrinkDecorator(IDrink innerDrink)
        {
            InnerDrink = innerDrink;
        }

        public virtual string Name => InnerDrink.Name;
        public virtual string Description => InnerDrink.Description;
        public virtual decimal Price => InnerDrink.Price;
        public DrinkStatus Status => InnerDrink.Status;

        public virtual void SetStatus(DrinkStatus status)
        {
            InnerDrink.SetStatus(status);
        }

        // Pass-through subject/observer behavior if inner drink is a subject
        public virtual void Attach(IDrinkObserver observer)
        {
            if (InnerDrink is IDrinkSubject subject)
                subject.Attach(observer);
        }

        public virtual void Detach(IDrinkObserver observer)
        {
            if (InnerDrink is IDrinkSubject subject)
                subject.Detach(observer);
        }

        public virtual void Notify()
        {
            if (InnerDrink is IDrinkSubject subject)
                subject.Notify();
        }
    }
}
