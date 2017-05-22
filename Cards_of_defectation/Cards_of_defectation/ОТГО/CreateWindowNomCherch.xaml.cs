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
using Cards_of_defectation.ОТГО.ViewModal;
using Cards_of_defectation.ОТГО.Classes;

namespace Cards_of_defectation.ОТГО
{
    public partial class CreateWindowNomCherch : Window
    {
        NomSZ_Cherch_Naim_ZavNom tmp;
        public CreateWindowNomCherch(NomSZ_Cherch_Naim_ZavNom ptmp)
        {
            InitializeComponent();
            combo_box.DataContext = new CreateWindowNomCherchViewModal();
            tmp = ptmp;
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
                if (combo_box.SelectedItem == null) combo_box.SelectedItem = combo_box.Items.CurrentItem;
                else
                {
                    tmp.Cherch = combo_box.SelectedItem.ToString();
                    CreateWindowNaim Next_window = new CreateWindowNaim(tmp);
                    Next_window.Show();
                    this.Close();
                }
            }
        }
    }
}
