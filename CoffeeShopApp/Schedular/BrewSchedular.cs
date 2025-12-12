using CoffeeShopApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeeShopApp.Scheduler
{
    public class BrewScheduler
    {
        private readonly Queue<BrewTask> _tasks = new();
        private bool _isRunning;

        public void Schedule(IDrink drink)
        {
            var task = new BrewTask(drink);
            _tasks.Enqueue(task);
            if (!_isRunning)
            {
                _ = RunNextAsync();
            }
        }

        private async Task RunNextAsync()
        {
            _isRunning = true;
            while (_tasks.Count > 0)
            {
                var next = _tasks.Dequeue();
                await next.StartAsync();
            }
            _isRunning = false;
        }
    }
}
