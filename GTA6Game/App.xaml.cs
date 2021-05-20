using GTA6Game.PlayerData;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Globalization;
using GTA6Game.Languages;

namespace GTA6Game
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            LanguageManager.PreferredLanguage = "en-US";
            LanguageManager.SetThreadCulture();
            SaveLoader.LoadSave();
        }
    }
}
