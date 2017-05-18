using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPF.MDI;
using Cards_of_defectation.Classes;

namespace Cards_of_defectation
{
    public partial class Main_window_of_system : Window
    {
        bool is_ceh;
        public Main_window_of_system()
        {
            is_ceh = false;
            InitializeComponent();
            Main_window.Init(main_container).AddWindow("План ремонта", new MainOUP());
        }

        public Main_window_of_system(string ceh)
        {
            is_ceh = true;
            InitializeComponent();
            Main_window.Init(main_container).AddWindow("План ремонта", new ShopAlert(ceh,this));
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Main_window.Init().AddWindow("План ремонта", new MainOUP());
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            int tmp = Convert.ToInt32(Server.InitServer().DataBase("test1").ExecuteCommand("select max(nom_zay) from plan_rabot")[0]);
            Main_window.Init().AddWindow("Служебная записка", new FirstLevel(tmp + 1));
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            Main_window.Init().AddWindow("Справочники", new ReductionReference());
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (is_ceh)
            {
                e.Cancel = true;
                this.Hide();
            }
        }
    }
}
