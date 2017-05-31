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
using Cards_of_defectation.ОТГО.ViewModal;
using Cards_of_defectation.Classes;

namespace Cards_of_defectation.ОТГО.Windows
{
    public partial class CreateWindowNaim : Window
    {
        NomSZ_Cherch_Naim tmp;
        public CreateWindowNaim(NomSZ_Cherch_Naim ptmp)
        {
            InitializeComponent();
            tmp = ptmp;
            combo_box.DataContext = new CreateWindowNaimViewModal(Server.InitServer().DataBase("cvodka")
                .ExecuteCommand("select ltrim(rtrim(naim)) from naim where ltrim(nom) = '"+tmp.Cherch+"'"));
            Loaded += delegate { combo_box.Focus(); };
        }
        private void ComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)e.OriginalSource;
            tb.Select(tb.SelectionStart + tb.SelectionLength, 0);
        }

        private void ComboBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                tmp.Naim = combo_box.Text;
                CreateWindowZavNomIzd Next_window = new CreateWindowZavNomIzd(tmp);
                Next_window.Show();
                this.Close();              
            }
        }
    }
}
