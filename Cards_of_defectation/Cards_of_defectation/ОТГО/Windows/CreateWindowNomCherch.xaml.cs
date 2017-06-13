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

namespace Cards_of_defectation.ОТГО.Windows
{
    public partial class CreateWindowNomCherch : Window
    {
        NomSZ_Cherch_Naim tmp;
        public CreateWindowNomCherch(NomSZ_Cherch_Naim ptmp)
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
                tmp.Cherch = combo_box.Text;
                CreateWindowNaim Next_window = new CreateWindowNaim(tmp);
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
                            (tmp.DataContext as CreateWindowNomCherchViewModal).IsNavigate = true;
                            if (tmp.SelectedIndex < tmp.Items.Count) tmp.SelectedIndex++;
                            break;
                        case Key.Up:
                            e.Handled = true;
                            (tmp.DataContext as CreateWindowNomCherchViewModal).IsNavigate = true;
                            if (tmp.SelectedIndex > 0) tmp.SelectedIndex--;
                            break;
                        default:
                            (tmp.DataContext as CreateWindowNomCherchViewModal).IsNavigate = false;
                            base.OnPreviewKeyDown(e);
                            break;
                    }
                }
        }
    }
}
