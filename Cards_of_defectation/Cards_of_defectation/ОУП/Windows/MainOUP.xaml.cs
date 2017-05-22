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

namespace Cards_of_defectation.ОУП.Windows
{
    public partial class MainOUP : Window
    {
        ObservableCollection<RowPlanViewModal> Rows;
        bool IsSave;
        Logger log;
        public MainOUP()
        {
            log = LogManager.GetCurrentClassLogger();
            IsSave = true;
            InitializeComponent();
            Binding_Commands();
            UpdateTable();
            Server.InitServer().DataBase("test1").InitStalker(Dispatcher.CurrentDispatcher, this);
            log.Debug("Первая ступень отработала");
        }

        public void UpdateTable()
        {
            Rows = Converter.ToViewModal(Server.InitServer().DataBase("test1")
                .Table("select * from plan_rabot").LoadFromServer() as List<Row_in_plan_rabot>);
            main_table.ItemsSource = Rows;
        }

        void Binding_Commands()
        {
            CommandBinding bind = new CommandBinding(ApplicationCommands.New);
            bind.Executed += New_Execute;
            this.CommandBindings.Add(bind);
            bind = new CommandBinding(ApplicationCommands.Save);
            bind.Executed += Save_Execute;
            this.CommandBindings.Add(bind);
        }

        private void New_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            Rows.Add(new RowPlanViewModal(Rows[Rows.Count - 1].Nom_zay+1));
            IsSave = false;
        }

        private void Save_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            Server.InitServer().DataBase("test1").Table("select * from plan_rabot")
                .UpdateServerData(Converter.ToSave(Rows));
            MessageBox.Show("Сохранено");
            IsSave = true;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Server.InitServer().DataBase("test1").Table("select * from plan_rabot")
                .UpdateServerData(Converter.ToSave(Rows));
            IsSave = true;
            string nom_zay = (main_table.SelectedItem as RowPlanViewModal).Nom_zay;
            if (References.InitReferences().IsSerInReference((main_table.SelectedItem as RowPlanViewModal).Ser_nom_izd))
            {
                switch ((sender as MenuItem).Header as string)
                {
                    case "Дерево дефектации":
                        Tree_defect TD = new Tree_defect(nom_zay, false);
                        TD.Show();
                        break;
                    case "Цеха":
                        Work_shop WS = new Work_shop(nom_zay);
                        WS.Show();
                        break;
                    case "Служебная записка":
                        SlugebZapiska FL = new SlugebZapiska(nom_zay);
                        FL.Show();
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
                if (result == MessageBoxResult.Yes) Save_Execute(null, null);
                if (result == MessageBoxResult.Cancel) e.Cancel = true;
            }
        }

        private void main_table_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (MessageBox.Show("Подтвердите удаление", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (Server.InitServer().DataBase("test1").ExecuteCommand("select id from kart_defect where nom_zay ="
                        + ((sender as DataGrid).SelectedItem as RowPlanViewModal).Nom_zay).Count != 0)
                        if (MessageBox.Show("Для этой заявки есть карты дефектации. Они будут удалены вместе с заявкой."
                            + " Продолжить?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            Server.InitServer().DataBase("test1").ExecuteCommand("delete from kart_defect where nom_zay="
                                + ((sender as DataGrid).SelectedItem as RowPlanViewModal).Nom_zay);
                            Server.InitServer().DataBase("test1").ExecuteCommand("delete from plan_rabot where nom_zay="
                                + ((sender as DataGrid).SelectedItem as RowPlanViewModal).Nom_zay);
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
            SlugebZapiska FL = new SlugebZapiska(Rows[Rows.Count - 1].Nom_zay + 1);
            FL.Show();
        }

        public static int GetIndexOfKartDefect(string nom_zay)
        {
            if (Server.InitServer().DataBase("test1")
                .ExecuteCommand("select * from kart_defect where nom_zay = " + nom_zay).Count == 0)
            {
                List<Row_in_kart_defect> save_list = new List<Row_in_kart_defect>();
                save_list.Add(new Row_in_kart_defect(nom_zay));
                Server.InitServer().DataBase("test1")
                    .Table("select * from kart_defect where nom_zay = " + nom_zay)
                    .UpdateServerData(save_list);
            }
            return Convert.ToInt32(Server.InitServer().DataBase("test1")
                    .ExecuteCommand("select id from kart_defect where nom_zay = " + nom_zay + " and par is NULL")[0]);
        }
    }
}
