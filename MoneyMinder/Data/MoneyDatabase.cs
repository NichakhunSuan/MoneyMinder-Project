using System;
using System.Collections.Generic;
using SQLite;
using System.Threading.Tasks;

namespace MoneyMinder.Data
{
    public class MoneyDatabase
    {
        SQLiteAsyncConnection _database;

        public async Task Init()
        {
            if (_database is not null)
                return;

            try
            {
                _database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
                await _database.CreateTableAsync<Models.Wallet>();
                await _database.CreateTableAsync<Models.SavingGoal>();
                await _database.CreateTableAsync<Models.Pot>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database initialization failed: {ex.Message}");
            }
        }

        public async Task<List<Models.Wallet>> GetWalletsAsync()
        {
            await Init();
            return await _database
                .Table<Models.Wallet>()
                .ToListAsync();
        }

        public async Task<List<Models.Wallet>> GetWalletsAsync(string pKeyword)
        {
            await Init();
            return await _database
                    .Table<Models.Wallet>()
                    .Where(x => x.Detail.Contains(pKeyword))
                    .ToListAsync();
        }

        public async Task<Models.Wallet> GetWalletAsync(int pWalletId)
        {
            await Init();
            return await _database
                .Table<Models.Wallet>()
                .Where(x => x.ID == pWalletId)
                .FirstOrDefaultAsync();
        }

        public async Task<int> SaveWalletAsync(Models.Wallet wallet)
        {
            await Init();
            if (wallet.ID != 0)
            {
                return await _database.UpdateAsync(wallet);
            }
            else
            {
                return await _database.InsertAsync(wallet);
            }
        }

        public async Task<int> DeleteWalletAsync(Models.Wallet pDelWallet)
        {
            await Init();
            return await _database.DeleteAsync(pDelWallet);
        }

        public async Task<List<Models.SavingGoal>> GetSavingGoalsAsync()
        {
            await Init();
            return await _database
                .Table<Models.SavingGoal>()
                .ToListAsync();
        }

        public async Task<int> SaveSavingGoalAsync(Models.SavingGoal savingGoal)
        {
            await Init();
            if (savingGoal.ID != 0)
            {
                return await _database.UpdateAsync(savingGoal);
            }
            else
            {
                return await _database.InsertAsync(savingGoal);
            }
        }

        public async Task<int> DeleteSavingGoalAsync(Models.SavingGoal savingGoal)
        {
            await Init();
            return await _database.DeleteAsync(savingGoal);
        }

        public async Task<int> SavePotMoneyAsync(int potMoney)
        {
            await Init();
            var pot = await _database.Table<Models.Pot>().FirstOrDefaultAsync();
            if (pot == null)
            {
                pot = new Models.Pot { Amount = potMoney };
                return await _database.InsertAsync(pot);
            }
            else
            {
                pot.Amount = potMoney;
                return await _database.UpdateAsync(pot);
            }
        }
    }
}
