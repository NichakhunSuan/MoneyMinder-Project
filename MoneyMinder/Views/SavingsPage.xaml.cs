using CommunityToolkit.Maui.Views;
using MoneyMinder.Models;
using MoneyMinder.Data;
using System.Collections.ObjectModel;

namespace MoneyMinder.Views;

public partial class SavingsPage : ContentPage
{
    public ObservableCollection<SavingGoal> SavingGoals { get; set; }

    public SavingsPage()
    {
        InitializeComponent();
        SavingGoals = new ObservableCollection<SavingGoal>();
        BindingContext = this;
        LoadSavingGoals();

        MessagingCenter.Subscribe<PopupPot, SavingGoal>(this, "SavingGoalAdded", (sender, newSavingGoal) =>
        {
            SavingGoals.Add(newSavingGoal);
            App.PotMoney += newSavingGoal.Amount;
            UpdateUI();
        });
    }

    private async void LoadSavingGoals()
    {
        var goals = await App.Database.GetSavingGoalsAsync();
        foreach (var goal in goals)
        {
            SavingGoals.Add(goal);
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        info.Children.Clear();
        foreach (var goal in SavingGoals)
        {
            var goalLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Margin = new Thickness(0, 5, 0, 0)
            };

            var goalLabel = new Label
            {
                VerticalOptions = LayoutOptions.Center
            };

            var formattedString = new FormattedString();
            formattedString.Spans.Add(new Span { Text = $"{goal.Date.ToShortDateString()}\n" });
            formattedString.Spans.Add(new Span { Text = $"💰 {goal.Amount} BATH", TextColor = Colors.Green });

            goalLabel.FormattedText = formattedString;

            var deleteButton = new Button
            {
                Text = "Delete",
                BackgroundColor = Colors.Red,
                TextColor = Colors.White,
                HorizontalOptions = LayoutOptions.End
            };
            deleteButton.Clicked += async (sender, e) => await DeleteSavingGoal(goal);

            var confirmCheckBox = new CheckBox
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.End
            };
            confirmCheckBox.CheckedChanged += async (sender, e) => await ConfirmDeposit(goal, e.Value);

            goalLayout.Children.Add(goalLabel);
            goalLayout.Children.Add(confirmCheckBox);
            goalLayout.Children.Add(deleteButton);

            info.Children.Add(goalLayout);
        }
    }

    private async Task DeleteSavingGoal(SavingGoal goal)
    {
        await App.Database.DeleteSavingGoalAsync(goal);
        SavingGoals.Remove(goal);
        App.PotMoney -= goal.Amount;
        MessagingCenter.Send(this, "PotMoneyUpdated", App.PotMoney);
        UpdateUI();
    }

    private async Task ConfirmDeposit(SavingGoal goal, bool isConfirmed)
    {
        if (isConfirmed)
        {
            App.PotMoney += goal.Amount;
        }
        else
        {
            App.PotMoney -= goal.Amount;
        }

        await App.Database.SavePotMoneyAsync(App.PotMoney);
        MessagingCenter.Send(this, "PotMoneyUpdated", App.PotMoney);
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        this.ShowPopup(new PopupPot());
    }
}
