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
using Cards_of_defectation.ОТГО.Classes;

namespace Cards_of_defectation.ОТГО.Windows
{
    public partial class MainWindowOTGO : Window
    {
        public MainWindowOTGO()
        {
            InitializeComponent();
        }

        private void Create_new_SZ(object sender, RoutedEventArgs e)
        {
            CreateWindowNumberSZ Next_window = new CreateWindowNumberSZ(new NomSZ_Cherch_Naim_ZavNom());
            Next_window.Show();
        }

        private void Open_SZ(object sender, RoutedEventArgs e)
        {
            SearchWindowSZ Next_window = new SearchWindowSZ();
            Next_window.Show();
        }
    }
}
