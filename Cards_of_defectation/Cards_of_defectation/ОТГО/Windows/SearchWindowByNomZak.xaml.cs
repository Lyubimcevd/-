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
using Cards_of_defectation.Classes;
using Cards_of_defectation.Windows;

namespace Cards_of_defectation.ОТГО.Windows
{
    public partial class SearchWindowByNomZak : Window
    {
        public SearchWindowByNomZak()
        {
            InitializeComponent();
            combo_box.DataContext = new SearchWindowByNomZakViewModal();
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
                datagrid.DataContext = Server.GetServer.DataBase("uit")
                    .Table("select rtrim(nom_sz) from rz_plan_rabot where nom_zak = '" + combo_box.SelectedItem+"'")
                    .LoadTableFromServer();
            }
        }
        private void listBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if ((sender as DataGrid).SelectedItem != null)
            {
                SlugebZapiska SZ = new SlugebZapiska((e.OriginalSource as TextBlock).Text);
                SZ.Show();
            }
        }
    }
}
