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
using Cards_of_defectation.ОУП.Windows;
using Cards_of_defectation.ОТГО.Windows;
using Cards_of_defectation.Windows;
using Cards_of_defectation.Classes;

namespace Cards_of_defectation
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Authorization.InitAut(false);
            MainOUP MO = new MainOUP();
            MO.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Authorization.InitAut(false);

            MainWindowOTGO MOT = new MainWindowOTGO();
            MOT.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Authorization.InitAut(true);
            ShopAlert SA = new ShopAlert(References.GetReferences.GetIdCeh("024"));
            SA.Show();
        }
    }
}
