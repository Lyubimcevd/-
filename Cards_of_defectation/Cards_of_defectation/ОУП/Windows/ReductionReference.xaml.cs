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
using Cards_of_defectation.Classes;
using System.Collections.ObjectModel;
using System.Data;

namespace Cards_of_defectation.ОУП.Windows
{
    public partial class ReductionReference:Window
    {
        string table_name;
        bool IsSave;

        public ReductionReference()
        {
            IsSave = true;
            Log.Init.Info("Запуск ReductionReference");
            InitializeComponent();
            Binding_Commands();
            Log.Init.Info("Загрузка таблицы rz_spravochniki");
            dataGrid1.ItemsSource = Server.GetServer.DataBase("uit").Table("select naim from rz_spravochniki")
                .LoadTableFromServer().DefaultView;
            Log.Init.Info("Загружено");
        }

        void Binding_Commands()
        {
            CommandBinding bind = new CommandBinding(ApplicationCommands.Save);
            bind.Executed += Save_Execute;
            this.CommandBindings.Add(bind);
            bind = new CommandBinding(ApplicationCommands.Delete);
        }

        private void Save_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            Log.Init.Info("Сохранение в "+table_name);
            Server.GetServer.DataBase("uit").Table("select * from "+table_name).UpdateServerData((dataGrid.ItemsSource as DataView).Table);
            Log.Init.Info("Сохранено");
            MessageBox.Show("Сохранено");
            IsSave = true;
        }

        private void dataGrid1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is TextBlock)
            {
                Log.Init.Info("Загрузка таблицы " + (e.OriginalSource as TextBlock).Text);
                table_name = Server.GetServer.DataBase("uit")
                        .ExecuteCommand("select table_naim from rz_spravochniki where naim = '"
                        + (e.OriginalSource as TextBlock).Text + "'")[0].ToString();
                dataGrid.ItemsSource = Server.GetServer.DataBase("uit").Table("select * from " + table_name)
                    .LoadTableFromServer().DefaultView;
                Log.Init.Info("загружено");
                Save_item.IsEnabled = true;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!IsSave)
            {
                MessageBoxResult result = MessageBox.Show("Есть несохранённые изменения. Сохранить?", "Предупреждение", MessageBoxButton.YesNoCancel);
                if (result == MessageBoxResult.Yes) Save_Execute(null, null);
                if (result == MessageBoxResult.Cancel) e.Cancel = true;
            }
        }

        private void dataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            IsSave = false;
        }
    }
}
