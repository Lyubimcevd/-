using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Cards_of_defectation.Classes;

namespace Cards_of_defectation
{
    public partial class App : Application
    {
        public App()
        {
            this.Startup += new StartupEventHandler(AppStartup);
        }

        void AppStartup(object sender, StartupEventArgs e)
        {
            Main_window_of_system M = null;
            if (Environment.UserName[0] == 'c')
            {
                M = new Main_window_of_system();
            }
            else
            {
                M = new Main_window_of_system("024");
                //M = new Main_window_of_system(Environment.UserName.Substring(1, Environment.UserName.IndexOf('-') - 1).PadLeft(3, '0'));
            }
            M.Show();
        }
    }
}
