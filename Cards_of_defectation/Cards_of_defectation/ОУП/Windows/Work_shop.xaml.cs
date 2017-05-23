using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Cards_of_defectation.Classes;
using System.Data;
using Cards_of_defectation.Windows;
using System.Windows.Threading;

namespace Cards_of_defectation.ОУП.Windows
{
    public partial class Work_shop : Window
    {
        string Nom_sz;

        public Work_shop(string pNom_sz)
        {
            Nom_sz = pNom_sz;
            InitializeComponent();
            UpdateTable();
            Server.InitServer().DataBase("uit").InitStalker(Dispatcher.CurrentDispatcher, this);
        }

        public void UpdateTable()
        {
            DataTable DT = Server.InitServer().DataBase("uit")
                .Table("select nom_ceh as Цех,Count(*) as [Карт на дефектации] from kart_defect where Nom_sz = "
                + Nom_sz.ToString() + "and nom_ceh is not null and spos_ustr = 0 group by nom_ceh").LoadTableFromServer();
            foreach (DataRow row in DT.Rows) row[0] = References.InitReferences().Cehs[Convert.ToInt32(row[0])];
            dataGrid.ItemsSource = DT.DefaultView;
        }

        private void dataGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ShopAlert SA = new ShopAlert(((sender as DataGrid).SelectedItems[0] as DataRowView).Row.ItemArray[0].ToString(),false);
            SA.Show();
        }
    }
}
