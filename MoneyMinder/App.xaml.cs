using SQLite;

namespace MoneyMinder
{
    public partial class App : Application
    {
        public static int PotMoney { get; set; }
        public static Data.MoneyDatabase Database { get; private set; }
        public App()
        {
            InitializeComponent();
            Database = new Data.MoneyDatabase();
            MainPage = new AppShell();
        }
    }
}
