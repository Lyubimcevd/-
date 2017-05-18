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
using System.Collections.ObjectModel;
using System.Data;

namespace Cards_of_defectation
{
    public partial class Defect_map : UserControl
    {
        ObservableCollection<RowDefectViewModal> Rows;
        RowDefectViewModal Header;
        int Id;
        bool IsSave;
        List<object> arm_search_list;

        public Defect_map(int pid)
        {
            InitializeComponent();
            Id = pid;

            CommandBinding bind = new CommandBinding(ApplicationCommands.Save);
            bind.Executed += Save_Execute;
            this.CommandBindings.Add(bind);
            bind = new CommandBinding(ApplicationCommands.Print);
            bind.Executed += Print_Execute;
            this.CommandBindings.Add(bind);

            Header = Converter.ToViewModal(Server.InitServer().DataBase("test1")
                .Table("select * from kart_defect where id = " + Id).LoadFromServer() as List<Row_in_kart_defect>)[0];
            cap.DataContext = Header;
            if (Header.Par == 0)
                Rows = Converter.ToViewModal(Server.InitServer().DataBase("test1")
                    .Table("select * from kart_defect where par = " + Id + " and spos_ustr = 0")
                    .LoadFromServer() as List<Row_in_kart_defect>);
            else
                Rows = Converter.ToViewModal(Server.InitServer().DataBase("test1")
                    .Table("select * from kart_defect where par = " + Id).LoadFromServer() as List<Row_in_kart_defect>);
            main_table.ItemsSource = Rows;
            listBox_sostav.ItemsSource = References.InitReferences().InitComposition(Header);
            IsSave = true;
        }

        private void Save_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            Server.InitServer().DataBase("test1").Table("select * from kart_defect where par = " + Id)
                .UpdateServerData(Converter.ToSave(Rows));
            ObservableCollection<RowDefectViewModal> tmp = new ObservableCollection<RowDefectViewModal>();
            Header.Data_def = DateTime.Now.ToShortDateString();
            tmp.Add(Header);
            Server.InitServer().DataBase("test1").Table("select * from kart_defect where id = " + Id)
                .UpdateServerData(Converter.ToSave(tmp));
            MessageBox.Show("Сохранено");
            IsSave = true;
        }

        private void Print_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            if (Header.Nom_zak.ToString().Length == 8) Print.Init().PrintDocument(Header,Rows);
            else MessageBox.Show("Номер заказа введён неверно","Ошибка");
        }

        private void main_table_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (MessageBox.Show("Подтвердите удаление", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    List<RowDefectViewModal> for_delete = new List<RowDefectViewModal>();
                    foreach (RowDefectViewModal row in main_table.SelectedItems)
                    {
                        List<object> tmp = Server.InitServer().DataBase("test1")
                            .ExecuteCommand("select id from kart_defect where par = " + row.Id);
                        if (tmp.Count != 0) for_delete.Add(row);
                    }
                    if (for_delete.Count > 0)
                        if (for_delete.Count == 1)
                        {
                            if (MessageBox.Show("Деталь " + for_delete.First().Obozn_det + " дефектирована. Продолжить?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                                DeleteRecurs((main_table.SelectedItem as RowDefectViewModal).Id);
                            else
                            {
                                e.Handled = true;
                                return;
                            }
                        }
                        else
                        {
                            string details = "";
                            foreach (RowDefectViewModal row in for_delete) details += "," + row.Obozn_det;
                            details = details.Substring(1);
                            if (MessageBox.Show("Детали " + details + " дефектированы. Продолжить?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                                foreach (RowDefectViewModal row in for_delete) DeleteRecurs(row.Id);
                            else
                            {
                                e.Handled = true;
                                return;
                            }
                        }
                    IsSave = false;
                }
                else e.Handled = true;
            }
        }

        void DeleteRecurs(int pid)
        {
            List<object> tmp = Server.InitServer().DataBase("test1")
                .ExecuteCommand("select id from kart_defect where par=" + pid);
            foreach (int id in tmp) DeleteRecurs(id);
            Server.InitServer().DataBase("test1").ExecuteCommand("delete from kart_defect where id=" + pid);
        }

        private void listBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if ((sender as ListBox).SelectedItem != null)
            {        
                Rows.Add((sender as ListBox).SelectedItem as RowDefectViewModal);
                IsSave = false;
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (RowDefectViewModal row in main_table.SelectedItems) row.Opis_def = (sender as ComboBox).SelectedIndex;
        }
        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            foreach (RowDefectViewModal row in main_table.SelectedItems) row.Prichina = (sender as ComboBox).SelectedIndex;
        }
        private void ComboBox_SelectionChanged_2(object sender, SelectionChangedEventArgs e)
        {
            foreach (RowDefectViewModal row in main_table.SelectedItems) row.Met_opr = (sender as ComboBox).SelectedIndex;
        }
        private void ComboBox_SelectionChanged_3(object sender, SelectionChangedEventArgs e)
        {
            foreach (RowDefectViewModal row in main_table.SelectedItems) row.Teh_treb = (sender as ComboBox).SelectedIndex;
        }
        private void ComboBox_SelectionChanged_4(object sender, SelectionChangedEventArgs e)
        {
            foreach (RowDefectViewModal row in main_table.SelectedItems) row.Spos_ustr = (sender as ComboBox).SelectedIndex;
        }
        private void ComboBox_SelectionChanged_5(object sender, SelectionChangedEventArgs e)
        {
            foreach (RowDefectViewModal row in main_table.SelectedItems)
                if ((sender as ComboBox).SelectedIndex != -1) row.Nom_ceh = (sender as ComboBox).SelectedIndex;
        }
        private void arm_search_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedItem!=null)
                listBox_arm.ItemsSource = References.InitReferences().SearchAndLoad(Header, (sender as ComboBox).SelectedItem.ToString());
        }
        private void ComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            arm_search_list = Server.InitServer().DataBase("uit")
                        .ExecuteCommand("select distinct top 50 Ltrim(rtrim(nc)) from table_nc1 where ltrim(nc) like '"
                                        + (sender as ComboBox).Text + "%'");
            arm_search.ItemsSource = arm_search_list;
            arm_search.IsDropDownOpen = true;
            var tb = (TextBox)e.OriginalSource;
            tb.Select(tb.SelectionStart + tb.SelectionLength, 0);
        }
        public void Window_Closing(WPF.MDI.Event.ClosingEventArgs e)
        {
            foreach (RowDefectViewModal row in Rows)
                if (row.IsChange)
                {
                    IsSave = false;
                    break;
                }
            if (!IsSave)
            {
                MessageBoxResult result = MessageBox.Show("Карта не сохранена. Сохранить?", "Предупреждение", MessageBoxButton.YesNoCancel);
                if (result == MessageBoxResult.Yes) Save_Execute(null, null);
                if (result == MessageBoxResult.Cancel) e.Cancel = true;
            }
        }
    }
}
