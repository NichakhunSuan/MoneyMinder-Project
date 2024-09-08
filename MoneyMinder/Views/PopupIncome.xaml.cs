using CommunityToolkit.Maui.Views;
using MoneyMinder.Models;

namespace MoneyMinder.Views
{
    public partial class PopupIncome : Popup
    {
        public event EventHandler WalletSaved;

        public PopupIncome()
        {
            InitializeComponent();
        }

        private async void Done_Clicked(object sender, EventArgs e)
        {
            var wallet = new Wallet
            {
                Income = int.Parse(incomeEntryField.Text),
                Detail = detailEntryField.Text,
                Date = DateTime.Now,
                Emote = emotePicker.SelectedItem?.ToString()
            };

            await App.Database.SaveWalletAsync(wallet);
            WalletSaved?.Invoke(this, EventArgs.Empty);
            Close();
        }

        private void Cancel_Clicked(object sender, EventArgs e)
        {
            Close();
        }
    }
}
