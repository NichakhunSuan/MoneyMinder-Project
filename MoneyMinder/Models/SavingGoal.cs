using SQLite;

namespace MoneyMinder.Models
{
    public class SavingGoal
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int Amount { get; set; }
        public string Goal { get; set; }
        public DateTime Date { get; set; }
    }
}