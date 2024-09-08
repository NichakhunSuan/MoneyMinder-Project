using CommunityToolkit.Maui.Views;
using MoneyMinder.Models;
using System;

namespace MoneyMinder
{
    public partial class PopupOutcome : Popup
    {
        public event EventHandler WalletSaved;

        public PopupOutcome()
        {
            InitializeComponent();
        }

        private void Done_Clicked(object sender, EventArgs e)
        {
            SaveOutcome();
            WalletSaved?.Invoke(this, EventArgs.Empty);
            Close();
        }

        private void Cancel_Clicked(object sender, EventArgs e)
        {
            Close();
        }

        private async void SaveOutcome()
        {
            int outcomeAmount = int.Parse(outcomeEntryField.Text);
            string detail = detailEntryField.Text;

            var wallet = new Wallet
            {
                Income = 0,
                Outcome = outcomeAmount,
                Detail = detail,
                Date = DateTime.Now,
                Emote = currencyPicker.SelectedItem?.ToString()
            };

            await App.Database.SaveWalletAsync(wallet);
        }
    }
}
