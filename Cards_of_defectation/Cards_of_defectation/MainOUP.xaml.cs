using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Cards_of_defectation.Classes;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using System.Windows.Data;

namespace Cards_of_defectation
{
    public partial class MainOUP : UserControl
    {
        ObservableCollection<RowPlanViewModal> Rows;
        bool IsSave;

        public MainOUP()
        {
            IsSave = true;
            InitializeComponent();
            Binding_Commands();
            UpdateTable();
            Server.InitServer().DataBase("test1").InitStalker(Dispatcher.CurrentDispatcher, this);
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
            int nom_zay = (main_table.SelectedItem as RowPlanViewModal).Nom_zay;
            if (References.InitReferences().IsSerInReference((main_table.SelectedItem as RowPlanViewModal).Ser_nom_izd))
            {
                if (Server.InitServer().DataBase("test1").ExecuteCommand("select id from kart_defect where nom_zay="
                         + nom_zay).Count == 0) RegistrIndexInKartDefect(nom_zay);
                switch ((sender as MenuItem).Header as string)
                {
                    case "Дерево дефектации":
                        Main_window.Init().AddWindow("Дерево дефектации", new Tree_defect(nom_zay, false));
                        break;
                    case "Цеха":
                        Main_window.Init().AddWindow("Цеха", new Work_shop(nom_zay));
                        break;
                    case "Служебная записка":
                        Main_window.Init().AddWindow("Служебная записка", new FirstLevel(nom_zay));
                        break;
                }           
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Main_window.Init().AddWindow("Контроль сроков", new Alert_time());
        }

        public void Window_Closing(WPF.MDI.Event.ClosingEventArgs e)
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
                if (result == MessageBoxResult.Cancel) e.Cancel= true;
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

        void RegistrIndexInKartDefect(int nom_zay)
        {
            List<Row_in_kart_defect> save_list = new List<Row_in_kart_defect>();
            save_list.Add(new Row_in_kart_defect((Server.InitServer().DataBase("test1")
                .Table("select * from plan_rabot where nom_zay = " + nom_zay)
                .LoadFromServer() as List<Row_in_plan_rabot>)[0]));
            Server.InitServer().DataBase("test1")
                .Table("select * from kart_defect where nom_zay = " + nom_zay)
                .UpdateServerData(save_list);
        }
    }
}
