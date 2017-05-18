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
using System.Windows.Threading;

namespace Cards_of_defectation
{
    public partial class Work_shop : UserControl
    {
        int nom_zay;

        public Work_shop(int Nom_zay)
        {
            nom_zay = Nom_zay;
            InitializeComponent();
            UpdateTable();
            Server.InitServer().DataBase("test1").InitStalker(Dispatcher.CurrentDispatcher, this);
        }

        public void UpdateTable()
        {
            DataTable DT = Server.InitServer().DataBase("test1")
                .Table("select nom_ceh as Цех,Count(*) as [Карт на дефектации] from kart_defect where nom_zay = "
                + nom_zay.ToString() + "and nom_ceh is not null and spos_ustr = 0 group by nom_ceh").LoadTableFromServer();
            foreach (DataRow row in DT.Rows) row[0] = References.InitReferences().Cehs[Convert.ToInt32(row[0])];
            dataGrid.ItemsSource = DT.DefaultView;
        }

        private void dataGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Main_window.Init().AddWindow("Карты дефектации цеха", new ShopAlert(((sender as DataGrid).SelectedItems[0] as DataRowView).Row.ItemArray[0].ToString()));
        }
    }
}
