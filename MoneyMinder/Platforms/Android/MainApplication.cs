﻿using Android.App;
using Android.Runtime;
using SQLitePCL;

namespace MoneyMinder
{
    [Application]
    public class MainApplication : MauiApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
        }

        protected override MauiApp CreateMauiApp()
        {
            raw.SetProvider(new SQLite3Provider_e_sqlite3());
            return MauiProgram.CreateMauiApp();
        }
    }
}
