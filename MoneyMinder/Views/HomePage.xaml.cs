using System.Windows.Input;

namespace MoneyMinder.Views;

public partial class HomePage : ContentPage
{
    public ICommand OpenUrlCommand => new Command<string>(async (url) => await Launcher.OpenAsync(url));
    private decimal potMoney;

    public decimal PotMoney
    {
        get => potMoney;
        set
        {
            potMoney = value;
            OnPropertyChanged(nameof(PotMoney));
            PotMoneyLabel.Text = $"Pot Money: {potMoney} BATH";
        }
    }

    public HomePage()
    {
        InitializeComponent();
        PotMoney = App.PotMoney;
        BindingContext = this;

        MessagingCenter.Subscribe<SavingsPage, decimal>(this, "PotMoneyUpdated", (sender, updatedPotMoney) =>
        {
            PotMoney = updatedPotMoney;
        });
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        PotMoneyLabel.Text = $"Pot Money: {App.PotMoney} BATH";
    }
}
