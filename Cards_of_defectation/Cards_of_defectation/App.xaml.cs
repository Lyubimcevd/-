using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Cards_of_defectation.Classes;
using Cards_of_defectation.ОТГО.Windows;
using Cards_of_defectation.Windows;
using Cards_of_defectation.ОУП.Windows;

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
            Window1 W1 = new Window1();
            W1.Show();
            //if (Environment.UserName[0] != 'c')
            //{
            //    MainWindowOTGO MWOT = new MainWindowOTGO();
            //    MWOT.Show();
            //    //MainOUP MOUP = new MainOUP();
            //    //MOUP.Show();
            //}
            //else
            //{
            //    //ShopAlert SA = new ShopAlert("024", true);
            //    ShopAlert SA = new ShopAlert(Environment.UserName.Substring(1, Environment.UserName.IndexOf('-') - 1).PadLeft(3, '0'),true);
            //    SA.Show();
            //}
        }
    }
}
