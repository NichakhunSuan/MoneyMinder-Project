using SQLite;

namespace MoneyMinder.Models
{
    public class Pot
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int Amount { get; set; }
    }
}
