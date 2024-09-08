using MoneyMinder.ViewModels;

namespace MoneyMinder.Views
{
    public partial class SummaryPage : ContentPage
    {
        public SummaryPage()
        {
            InitializeComponent();
            BindingContext = new SummaryViewModel();
        }
    }
}
