using System;
using System.Collections.ObjectModel;
using System.Linq;
using MoneyMinder.Models;
using MoneyMinder.Data;

namespace MoneyMinder.ViewModels
{
    public class SummaryViewModel : BaseViewModel
    {
        public ObservableCollection<MonthlySummary> MonthlySummaries { get; set; }
        public ObservableCollection<YearlySummary> YearlySummaries { get; set; }

        public SummaryViewModel()
        {
            MonthlySummaries = new ObservableCollection<MonthlySummary>();
            YearlySummaries = new ObservableCollection<YearlySummary>();
            LoadSummaries();
        }

        private async void LoadSummaries()
        {
            var wallets = await App.Database.GetWalletsAsync();

            var monthlyGroups = wallets.GroupBy(w => new { w.Date.Year, w.Date.Month })
                                       .Select(g => new MonthlySummary
                                       {
                                           Year = g.Key.Year,
                                           Month = $"{g.Key.Year}-{g.Key.Month:D2}",
                                           TotalIncome = g.Sum(w => w.Income),
                                           TotalOutcome = g.Sum(w => w.Outcome),
                                           Balance = g.Sum(w => w.Income - w.Outcome)
                                       })
                                       .OrderBy(ms => ms.Year).ThenBy(ms => ms.Month);

            foreach (var group in monthlyGroups)
            {
                MonthlySummaries.Add(group);
            }

            var yearlyGroups = wallets.GroupBy(w => w.Date.Year)
                                      .Select(g => new YearlySummary
                                      {
                                          Year = g.Key,
                                          TotalIncome = g.Sum(w => w.Income),
                                          TotalOutcome = g.Sum(w => w.Outcome),
                                          Balance = g.Sum(w => w.Income - w.Outcome)
                                      })
                                      .OrderBy(ys => ys.Year);

            foreach (var group in yearlyGroups)
            {
                YearlySummaries.Add(group);
            }
        }
    }

    public class MonthlySummary
    {
        public int Year { get; set; }
        public string Month { get; set; }
        public int TotalIncome { get; set; }
        public int TotalOutcome { get; set; }
        public int Balance { get; set; }
    }

    public class YearlySummary
    {
        public int Year { get; set; }
        public int TotalIncome { get; set; }
        public int TotalOutcome { get; set; }
        public int Balance { get; set; }
    }
}
