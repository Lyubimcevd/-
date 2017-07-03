using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Cards_of_defectation.Classes;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using System.Windows.Data;
using NLog;
using Cards_of_defectation.ОУП.ViewModal;
using Cards_of_defectation.Windows;
using Cards_of_defectation.ОУП.Windows;

namespace Cards_of_defectation.ОУП.Windows
{
    public partial class MainOUP : Window
    {
        ObservableCollection<RowPlanViewModal> Rows;
        bool IsSave = true;

        public MainOUP()
        {
            IsSave = true;
            Log.Init.Info("Запуск MainOUP");
            InitializeComponent();
            UpdateTable();
            Server.GetServer.DataBase("uit").InitStalker(Dispatcher.CurrentDispatcher, this);
        }

        public void UpdateTable()
        {
            Log.Init.Info("Загрузка таблицы rz_plan_rabot");
            Rows = Converter.ToViewModal(Server.GetServer.DataBase("uit")
                .Table("select * from rz_plan_rabot").LoadFromServer() as List<Row_in_plan_rabot>);
            Log.Init.Info("Загру");
            main_table.ItemsSource = Rows;
        }

        private void CommandBinding_New(object sender, ExecutedRoutedEventArgs e)
        {          
            Rows.Add(new RowPlanViewModal());
            Log.Init.Info("Добавлена строка в rz_plan_rabot");
            IsSave = false;
        }

        private void CommandBinding_Save(object sender, ExecutedRoutedEventArgs e)
        {
            Log.Init.Info("Сохранение в rz_plan_rabot");
            Server.GetServer.DataBase("uit").Table("select * from rz_plan_rabot")
                .UpdateServerData(Converter.ToSave(Rows));
            Log.Init.Info("Сохранено");
            MessageBox.Show("Сохранено");
            IsSave = true;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Server.GetServer.DataBase("uit").Table("select * from rz_plan_rabot")
                .UpdateServerData(Converter.ToSave(Rows));
            IsSave = true;
            string Nom_sz = (main_table.SelectedItem as RowPlanViewModal).Nom_sz;
            if (References.GetReferences.IsSerInReference((main_table.SelectedItem as RowPlanViewModal).Ser_nom))
            {
                switch ((sender as MenuItem).Header as string)
                {
                    case "Дерево дефектации":
                        Tree_defect TD = new Tree_defect(Nom_sz);
                        TD.Show();
                        break;
                    case "Цеха":
                        Work_shop WS = new Work_shop(Nom_sz);
                        WS.Show();
                        break;
                    case "Служебная записка":
                        if (Nom_sz == (main_table.SelectedItem as RowPlanViewModal).Ser_nom)
                        {
                            CreateWindowNumberSZ_OUP Nom_sz_enter = new CreateWindowNumberSZ_OUP();
                            Nom_sz_enter.ShowDialog();
                            Nom_sz = Nom_sz_enter.Return_nom_sz();
                        }
                        if (Nom_sz != null)
                        {
                            SlugebZapiska FL = new SlugebZapiska(Nom_sz);
                            FL.Show();
                        }
                        break;
                }           
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Alert_time AT = new Alert_time();
            AT.ShowDialog();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            ReductionReference RR = new ReductionReference();
            RR.ShowDialog();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach (RowPlanViewModal row in Rows)
                if (row.IsChange)
                {
                    IsSave = false;
                    break;
                }
            if (!IsSave)
            {
                MessageBoxResult result = MessageBox.Show("Данные не были сохранены. Сохранить?", "Предупреждение", MessageBoxButton.YesNoCancel);
                if (result == MessageBoxResult.Yes) CommandBinding_Save(null, null);
                if (result == MessageBoxResult.Cancel) e.Cancel = true;
            }
            if (!e.Cancel) Server.GetServer.CloseConnections();
        }

        private void main_table_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete&&e.OriginalSource is DataGridCell)
            {
                if (MessageBox.Show("Подтвердите удаление", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (Server.GetServer.DataBase("uit").ExecuteCommand("select id from rz_kart_defect where nom_sz ='"
                        + ((sender as DataGrid).SelectedItem as RowPlanViewModal).Nom_sz+"'").Count != 0)
                        if (MessageBox.Show("Для этой заявки есть карты дефектации. Они будут удалены вместе с заявкой."
                            + " Продолжить?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            Log.Init.Info("Удаление из rz_kart_defect по номеру служебки "+ ((sender as DataGrid).SelectedItem as RowPlanViewModal).Nom_sz);
                            Server.GetServer.DataBase("uit").ExecuteCommand("delete from rz_kart_defect where nom_sz='"
                                + ((sender as DataGrid).SelectedItem as RowPlanViewModal).Nom_sz+"'");
                            Log.Init.Info("Удалено");
                            Log.Init.Info("Удаление из rz_plan_rabot по номеру служебки " + ((sender as DataGrid).SelectedItem as RowPlanViewModal).Nom_sz);
                            Server.GetServer.DataBase("uit").ExecuteCommand("delete from rz_plan_rabot where nom_sz='"
                                + ((sender as DataGrid).SelectedItem as RowPlanViewModal).Nom_sz+"'");
                            Log.Init.Info("Удалено");
                        }
                        else
                        {
                            e.Handled = true;
                            return;
                        }
                    IsSave = false;
                }
                else e.Handled = true;
            }
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            SlugebZapiska FL = new SlugebZapiska(Rows[Rows.Count - 1].Nom_sz + 1);
            FL.Show();
        }

        public static int GetIndexOfKartDefect(string Nom_sz)
        {
            Log.Init.Info("Запрос нового индекса в rz_kart_defect");
            if (Server.GetServer.DataBase("uit")
                .ExecuteCommand("select * from rz_kart_defect where nom_sz = '" + Nom_sz+"'").Count == 0)
            {
                List<Row_in_kart_defect> save_list = new List<Row_in_kart_defect>();
                save_list.Add(new Row_in_kart_defect((Server.GetServer.DataBase("uit")
                    .Table("select * from rz_plan_rabot where nom_sz = '"+Nom_sz+"'")
                    .LoadFromServer() as List<Row_in_plan_rabot>)[0]));
                Server.GetServer.DataBase("uit")
                    .Table("select * from rz_kart_defect where nom_sz = '" + Nom_sz+"'")
                    .UpdateServerData(save_list);
            }
            int result = Convert.ToInt32(Server.GetServer.DataBase("uit")
                        .ExecuteCommand("select id from rz_kart_defect where nom_sz = '" + Nom_sz + "' and par is NULL")[0]);
            Log.Init.Info("Возвращён индекс "+result);
            return result;
        }

        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !IsSave;
        }
    }
}
