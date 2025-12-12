using CoffeeShopApp.Factory;
using CoffeeShopApp.Models;
using CoffeeShopApp.Scheduler;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CoffeeShopApp
{
    public partial class MainWindow : Window, IDrinkObserver
    {
        private int _screenIndex = 0;

        private readonly ICoffeeFactory _factory = new CoffeeFactory();
        private readonly BrewScheduler _scheduler = new BrewScheduler();

        private IDrink _currentDrink;

        public MainWindow()
        {
            InitializeComponent();
            UpdateScreen();
        }

        private void UpdateScreen()
        {
            SelectDrinkCard.Visibility = Visibility.Collapsed;
            CustomizeCard.Visibility = Visibility.Collapsed;
            SummaryCard.Visibility = Visibility.Collapsed;
            BrewingCard.Visibility = Visibility.Collapsed;

            BackButton.IsEnabled = true;
            NextButton.IsEnabled = true;

            switch (_screenIndex)
            {
                case 0:
                    TitleText.Text = "Select Your Drink";
                    SubtitleText.Text = "Pick a base drink and size.";
                    FooterHintText.Text = "Select → Customize → Summary → Brew";

                    SelectDrinkCard.Visibility = Visibility.Visible;
                    BackButton.IsEnabled = false;
                    NextButton.Content = "Next";
                    break;

                case 1:
                    TitleText.Text = "Customize Your Drink";
                    SubtitleText.Text = "Add optional add-ons (Decorator).";
                    FooterHintText.Text = "Decorator adds features dynamically.";

                    CustomizeCard.Visibility = Visibility.Visible;
                    NextButton.Content = "Next";
                    break;

                case 2:
                    TitleText.Text = "Order Summary";
                    SubtitleText.Text = "Review ingredients + add-ons.";
                    FooterHintText.Text = "Composite + Decorator combine here.";

                    SummaryCard.Visibility = Visibility.Visible;
                    NextButton.Content = "Brew";
                    BuildDrinkAndSummary();
                    break;

                case 3:
                    TitleText.Text = "Brewing Status";
                    SubtitleText.Text = "Brewing runs asynchronously (Scheduler).";
                    FooterHintText.Text = "Observer updates show up in the event log.";

                    BrewingCard.Visibility = Visibility.Visible;
                    BackButton.IsEnabled = false;
                    NextButton.IsEnabled = false;

                    StartBrewing();
                    break;
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (_screenIndex < 3)
            {
                _screenIndex++;
                UpdateScreen();
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (_screenIndex > 0)
            {
                _screenIndex--;
                UpdateScreen();
            }
        }

        private void BuildDrinkAndSummary()
        {
            bool large = LargeRadio.IsChecked == true;
            string type =
                LatteRadio.IsChecked == true ? "Latte" :
                AmericanoRadio.IsChecked == true ? "Americano" :
                "Brewed Coffee";

            // 1) Create base drink (Factory)
            IDrink drink = _factory.CreateDrink(type, large);

            // 2) Apply decorators (Decorator)
            if (ExtraShotCheck.IsChecked == true)
                drink = new ExtraShotDecorator(drink);
            if (SyrupCheck.IsChecked == true)
                drink = new SyrupDecorator(drink);
            if (WhippedCheck.IsChecked == true)
                drink = new WhippedCreamDecorator(drink);

            _currentDrink = drink;

            // 3) Attach observer AFTER wrapping so UI always receives updates
            if (_currentDrink is IDrinkSubject subject)
                subject.Attach(this);

            SummaryDrinkText.Text = $"{_currentDrink.Name} ({(large ? "Large" : "Small")})";
            SummaryDescriptionText.Text = _currentDrink.Description;
            SummaryPriceText.Text = $"${_currentDrink.Price:F2}";
        }

        private async void StartBrewing()
        {
            BrewingProgress.Value = 0;
            BrewingLogText.Text = "";

            // initial state
            OnStatusChanged(_currentDrink);

            // schedule brewing (Scheduler)
            _scheduler.Schedule(_currentDrink);

            for (int i = 0; i <= 100; i += 4)
            {
                BrewingProgress.Value = i;
                await Task.Delay(120);
            }
        }

        public void OnStatusChanged(IDrink drink)
        {
            Dispatcher.Invoke(() =>
            {
                BrewingStatusText.Text = $"Status: {drink.Status}";

                var sb = new StringBuilder(BrewingLogText.Text);
                sb.AppendLine($"{DateTime.Now:T} - {drink.Name} is {drink.Status}");
                BrewingLogText.Text = sb.ToString();

                // This part is optional, we can remove that
                // when READY, allow user to go back and create a new order
                if (drink.Status == DrinkStatus.Ready)
                {
                    NextButton.IsEnabled = true;
                    NextButton.Content = "New Order";
                    NextButton.Click -= NextButton_Click;
                    NextButton.Click += NewOrder_Click;
                }
            });
        }

        private void NewOrder_Click(object sender, RoutedEventArgs e)
        {
            // removig handler so it doesn't stack multiple times
            NextButton.Click -= NewOrder_Click;
            NextButton.Click += NextButton_Click;

            ExtraShotCheck.IsChecked = false;
            SyrupCheck.IsChecked = false;
            WhippedCheck.IsChecked = false;
            SmallRadio.IsChecked = true;
            LatteRadio.IsChecked = true;

            _screenIndex = 0;
            UpdateScreen();
        }
    }
}
