using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Cards_of_defectation.Classes;
using System.Threading;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using System.Windows.Forms;
using Cards_of_defectation.ViewModal;

namespace Cards_of_defectation.Windows
{
    public partial class ShopAlert : Window
    {
        ObservableCollection<ShopAlertViewModal> Rows;
        int current_kolvo,nom_ceh;
        bool realy_close;
        NotifyIcon NI;

        public ShopAlert(int pnom_ceh)
        {
            if (Authorization.Get.IsCeh)
            {
                NI = new NotifyIcon();
                NI.Icon = Cards_of_defectation.Properties.Resources.advancedsettings_5685;
                NI.Text = "Карты дефектации";
                NI.DoubleClick += new EventHandler(Click_by_Icon);
                NI.BalloonTipClicked += new EventHandler(Click_by_Icon);
                NI.Visible = true;
                NI.ContextMenu = new System.Windows.Forms.ContextMenu();
                NI.ContextMenu.MenuItems.Add(new System.Windows.Forms.MenuItem("Выход", Click_for_exit));
            }
            nom_ceh = pnom_ceh;
            InitializeComponent();          
            UpdateRow();
            Server.GetServer.DataBase("uit").InitStalker(Dispatcher.CurrentDispatcher, this);
            realy_close = !Authorization.Get.IsCeh;
        }

        void Click_by_Icon(object sender, EventArgs e)
        {
            this.Visibility = Visibility.Visible;
        }
        void Click_for_exit(object sender, EventArgs e)
        {
            realy_close = true;
            this.Close();
        }

        public void UpdateRow()
        {
            Rows = Converter.ToViewModalShop(Server.GetServer.DataBase("uit")
               .Table("select * from rz_kart_defect where nom_ceh = '" 
               + nom_ceh + "' and spos_ustr = 1 order by data_post").LoadFromServerForShopAlert());
            if (Rows.Count != current_kolvo)
            {
                if (Rows.Count > current_kolvo && current_kolvo != 0)
                {
                    NI.ShowBalloonTip(30000, "Оповещение", "Получена новая карта на дефектацию", ToolTipIcon.Info);
                    for (int i = 0; i < Rows.Count - current_kolvo; i++) Rows[i].Color = "GreenYellow";
                }
                current_kolvo = Rows.Count;
            }
            main_grid.ItemsSource = Rows;
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                (main_grid.SelectedItem as ShopAlertViewModal).Color = "White";
                Defect_map DM = new Defect_map((main_grid.SelectedItem as ShopAlertViewModal).Id);
                DM.ShowDialog();
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Tree_defect TD = new Tree_defect((main_grid.SelectedItem as ShopAlertViewModal).Nom_sz);
            TD.Show();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            if (main_grid.SelectedItem != null)
            {
                Server.GetServer.DataBase("uit").ExecuteCommand("update rz_kart_defect set is_faster = 1 where id = "
                + (main_grid.SelectedItem as ShopAlertViewModal).Id);
            }     
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            if (main_grid.SelectedItem != null)
            {
                Server.GetServer.DataBase("uit").ExecuteCommand("update rz_kart_defect set is_faster = 0 where id = "
                + (main_grid.SelectedItem as ShopAlertViewModal).Id);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!realy_close)
            {
                e.Cancel = true;
                this.Hide();
            }
        }
    }
}
