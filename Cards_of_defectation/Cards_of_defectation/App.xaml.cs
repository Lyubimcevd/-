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
            switch (Environment.UserName[0])
            {
                case 'p':
                    MainOUP MOUP = new MainOUP();
                    MOUP.Show();
                    break;
                case 'o':
                    MainWindowOTGO MWOT = new MainWindowOTGO();
                    MWOT.Show();
                    break;
                case 'c':
                    ShopAlert SA = new ShopAlert(References.GetReferences.GetIdCeh(Environment.UserName.Substring(1,
                        Environment.UserName.IndexOf('-') - 1).PadLeft(3, '0')));
                    SA.Show();
                    break;
                case 'l':
                    Window1 W1 = new Window1();
                    W1.Show();
                    break;
            }
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            Server.GetServer.CloseConnections();
        }
    }
}
