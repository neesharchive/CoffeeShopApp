using CoffeeShopApp.Models;
using System;
using System.Threading.Tasks;

namespace CoffeeShopApp.Scheduler
{
    public class BrewTask
    {
        public IDrink Drink { get; }

        public BrewTask(IDrink drink)
        {
            Drink = drink;
        }

        public async Task StartAsync()
        {
            Drink.SetStatus(DrinkStatus.Brewing);
            await Task.Delay(TimeSpan.FromSeconds(3));//again, just for show, simulating brewing, can be removed if not needed. Just thought it looked cool.
            Drink.SetStatus(DrinkStatus.Ready);
        }
    }
}
