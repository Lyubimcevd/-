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
using Cards_of_defectation.ОУП.ViewModal;

namespace Cards_of_defectation.ОУП.Windows
{
    public partial class Work_shop : Window
    {
        public Work_shop(string nom_sz)
        {
            Log.Init.Info("Запуск Work_shop");
            InitializeComponent();
            Log.Init.Info("Загрузка данных");
            List<object> tmp = Server.InitServer().DataBase("uit")
                .ExecuteCommand("select nom_ceh from rz_kart_defect where nom_ceh is not null and nom_sz = '" + nom_sz 
                +"' group by nom_ceh");
            List<WorkShopViewModal> Rows = new List<WorkShopViewModal>();
            foreach (object nom_ceh in tmp) Rows.Add(new WorkShopViewModal(Convert.ToInt32(nom_ceh),nom_sz));
            Log.Init.Info("Загружено");
            dataGrid.ItemsSource = Rows;
        }

        private void dataGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ShopAlert SA = new ShopAlert(((sender as DataGrid).SelectedItem as WorkShopViewModal).Nom_ceh);
            SA.Show();
        }
    }
}
