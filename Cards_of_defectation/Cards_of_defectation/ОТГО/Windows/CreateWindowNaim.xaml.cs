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
            combo_box.DataContext = new CreateWindowNaimViewModal(Server.GetServer.DataBase("cvodka")
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
        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            if (e.OriginalSource is TextBox)
                if ((e.OriginalSource as TextBox).TemplatedParent is ComboBox)
                {
                    ComboBox tmp = (e.OriginalSource as TextBox).TemplatedParent as ComboBox;
                    switch (e.Key)
                    {
                        case Key.Down:
                            e.Handled = true;
                            (tmp.DataContext as CreateWindowNaimViewModal).IsNavigate = true;
                            if (tmp.SelectedIndex < tmp.Items.Count) tmp.SelectedIndex++;
                            break;
                        case Key.Up:
                            e.Handled = true;
                            (tmp.DataContext as CreateWindowNaimViewModal).IsNavigate = true;
                            if (tmp.SelectedIndex > 0) tmp.SelectedIndex--;
                            break;
                        default:
                            (tmp.DataContext as CreateWindowNaimViewModal).IsNavigate = false;
                            base.OnPreviewKeyDown(e);
                            break;
                    }
                }
        }
    }
}
