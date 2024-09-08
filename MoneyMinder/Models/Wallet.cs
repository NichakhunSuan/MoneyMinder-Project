using System;
using SQLite;
namespace MoneyMinder.Models
{
    public class Wallet
    {
        //ห้ามมีค่าซ้ำ    ทุกครั้งเพิ่ม +1
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; } //int != null
        public int Income { get; set; }
        public int Outcome { get; set; }
        public string Detail { get; set; }
        public DateTime Date { get; set; }
        public string Emote { get; set; }

        public string DisplayAmount => Income > 0 ? $"+{Income} BATH" : $"-{Outcome} BATH";
    }
}
