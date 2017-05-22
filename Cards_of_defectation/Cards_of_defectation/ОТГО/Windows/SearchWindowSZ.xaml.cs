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
using Cards_of_defectation.Windows;

namespace Cards_of_defectation.ОТГО.Windows
{
    public partial class SearchWindowSZ : Window
    {
        public SearchWindowSZ()
        {
            InitializeComponent();
            combo_box.DataContext = new SearchWindowSZViewModal();
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
                    SlugebZapiska SZ = new SlugebZapiska(combo_box.SelectedItem.ToString());
                    SZ.Show();
                    this.Close();
                }

            }
        }
        private void Search_by_nom_zak(object sender, RoutedEventArgs e)
        {
            SearchWindowByNomZak Next_window = new SearchWindowByNomZak();
            Next_window.Show();
            this.Close();
        }
        private void Search_by_ser_nom(object sender, RoutedEventArgs e)
        {
            SearchWindowBySerNom Next_window = new SearchWindowBySerNom();
            Next_window.Show();
            this.Close();
        }
    }
}
