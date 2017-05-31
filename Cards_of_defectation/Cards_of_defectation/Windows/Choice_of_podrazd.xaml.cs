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
using Cards_of_defectation.Classes;
using Cards_of_defectation.ViewModal;

namespace Cards_of_defectation.Windows
{
    public partial class Choice_of_podrazd : Window
    {
        SlugebZapiskaViewModal flvm;
        List<ChoiceViewModal> Rows;
        public Choice_of_podrazd(SlugebZapiskaViewModal pflvm)
        {
            InitializeComponent();
            flvm = pflvm;
            Rows = new List<ChoiceViewModal>();
            List<object> tmp = Server.InitServer().DataBase("uit").ExecuteCommand("select * from rz_for_print");
            foreach (string podr in tmp) Rows.Add(new ChoiceViewModal(podr));
            main_list_box.ItemsSource = Rows;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Print.Init().PrintSlZap(flvm, Rows);
            this.Close();
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ((sender as TextBlock).DataContext as ChoiceViewModal).IsChecked = true;
        }
    }
}
