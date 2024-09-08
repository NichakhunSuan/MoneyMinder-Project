using CommunityToolkit.Maui.Views;
using MoneyMinder.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyMinder.Views
{
    public partial class WalletPage : ContentPage
    {
        public ObservableCollection<Wallet> Wallets { get; set; }
        public string Balance => $"Balance: {Wallets.Sum(w => w.Income - w.Outcome)} BATH";

        public WalletPage()
        {
            InitializeComponent();
            Wallets = new ObservableCollection<Wallet>();
            BindingContext = this;
            LoadDataFromSqlite();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadDataFromSqlite();
        }

        private async void LoadDataFromSqlite()
        {
            try
            {
                var walletDb = await App.Database.GetWalletsAsync();
                Wallets.Clear();
                foreach (var row in walletDb)
                {
                    Wallets.Add(row);
                }
                OnPropertyChanged(nameof(Balance));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data: {ex.Message}");
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var popup = new PopupIncome();
            popup.WalletSaved += (s, args) => { LoadDataFromSqlite(); OnPropertyChanged(nameof(Balance)); };
            this.ShowPopup(popup);
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            var popup = new PopupOutcome();
            popup.WalletSaved += (s, args) => { LoadDataFromSqlite(); OnPropertyChanged(nameof(Balance)); };
            this.ShowPopup(popup);
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is int walletId)
            {
                var wallet = Wallets.FirstOrDefault(w => w.ID == walletId);
                if (wallet != null)
                {
                    await App.Database.DeleteWalletAsync(wallet);
                    Wallets.Remove(wallet);
                    OnPropertyChanged(nameof(Balance));
                }
            }
        }
    }
}
