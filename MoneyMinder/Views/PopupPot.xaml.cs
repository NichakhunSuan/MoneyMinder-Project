using CommunityToolkit.Maui.Views;
using MoneyMinder.Models;

namespace MoneyMinder;

public partial class PopupPot : Popup
{
    public PopupPot()
    {
        InitializeComponent();
        BindingContext = this;
    }

    private async void Done_Clicked(object sender, EventArgs e)
    {
        if (decimal.TryParse(moneyEntry.Text, out decimal amount) && !string.IsNullOrWhiteSpace(goalsEntry.Text))
        {
            var newSavingGoal = new SavingGoal
            {
                Amount = (int)amount,
                Goal = goalsEntry.Text,
                Date = datePicker.Date
            };

            await App.Database.SaveSavingGoalAsync(newSavingGoal);
            MessagingCenter.Send(this, "SavingGoalAdded", newSavingGoal);

            Close();
        }
        else
        {

        }
    }

    private void Cancel_Clicked(object sender, EventArgs e)
    {
        Close();
    }
}
