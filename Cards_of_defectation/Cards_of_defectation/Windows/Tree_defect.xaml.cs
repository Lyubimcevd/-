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
using Cards_of_defectation.Classes;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using System.Windows.Media;
using Cards_of_defectation.ViewModal;
using Cards_of_defectation.ОУП.Windows;

namespace Cards_of_defectation.Windows
{
    public partial class Tree_defect : Window
    {
        int id;
        string Nom_sz;
        ObservableCollection<TreeViewModal> Modal;

        public Tree_defect(string pNom_sz)
        {
            InitializeComponent();
            Nom_sz = pNom_sz;
            id = MainOUP.GetIndexOfKartDefect(Nom_sz);
            Modal = new ObservableCollection<TreeViewModal>();
            Modal.Add(LoadTreeFromServer(id));
            Server.GetServer.DataBase("uit").InitStalker(Dispatcher.CurrentDispatcher,this);
            treeView.ItemsSource = Modal;
        }

        public void UpdateTree()
        {
            Modal[0] = LoadTreeFromServer(Modal[0].Id);
        }

        TreeViewModal LoadTreeFromServer(int pid)
        {
            RowDefectViewModal tmp = Converter.ToViewModal(Server.GetServer.DataBase("uit")
                .Table("select * from rz_kart_defect where id =" + pid).LoadFromServer() as List<Row_in_kart_defect>)[0];
            TreeViewModal root = new TreeViewModal(tmp);
            List<object> tmp_list = Server.GetServer.DataBase("uit")
                .ExecuteCommand("select id from rz_kart_defect where par =" + pid);
            foreach (int id in tmp_list)
            {
                TreeViewModal tmplist = root.Children.FirstOrDefault(x => x.Id == id);
                if (tmplist == null) root.Children.Add(LoadTreeFromServer(id));
                else tmplist = LoadTreeFromServer(id);
            }
            if (root.Children.Count != 0)
            {
                List<TreeViewModal> tmp1 = (from u in root.Children orderby u.Nom_ceh select u).ToList();
                root.Children.Clear();
                foreach (TreeViewModal list in tmp1) root.Children.Add(list);
            }
            return root;
        }

        string OboznOrNaim(RowDefectViewModal tmp)
        {
            if (tmp.Cherch == null) return tmp.Naim_det;
            return tmp.Cherch;
        }
        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!Authorization.Get.IsCeh)
            {
                if (((sender as Grid).DataContext as TreeViewModal).Spos_ustr == "Дефектация")
                {
                    Defect_map DM = new Defect_map(((sender as Grid).DataContext as TreeViewModal).Id);
                    DM.ShowDialog();
                }
            }
        }

        private void CommandBinding_Print(object sender, ExecutedRoutedEventArgs e)
        {
            Print.Init().PrintTree(Modal[0]);
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Grid).Background = Brushes.Aquamarine;
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Grid).Background = Brushes.White;
        }
    }
}
