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

namespace Cards_of_defectation
{
    public partial class ReductionReference: UserControl
    {
        string table_name;
        bool IsSave;

        public ReductionReference()
        {
            IsSave = true;
            InitializeComponent();
            Binding_Commands();
            dataGrid1.ItemsSource = Server.InitServer().DataBase("test1").Table("select naim from spravochniki")
                .LoadTableFromServer().DefaultView;
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
            Server.InitServer().DataBase("test1").Table("select * from "+table_name).UpdateServerData((dataGrid.ItemsSource as DataView).Table);
            MessageBox.Show("Сохранено");
            IsSave = true;
        }

        private void dataGrid1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            table_name = Server.InitServer().DataBase("test1")
                    .ExecuteCommand("select table_naim from spravochniki where naim = '"
                    + ((sender as DataGrid).SelectedItem as DataRowView).Row.ItemArray[0].ToString() + "'")[0].ToString();
            dataGrid.ItemsSource = Server.InitServer().DataBase("test1").Table("select * from " + table_name)
                .LoadTableFromServer().DefaultView;
            Save_item.IsEnabled = true;
        }

        public void Window_Closing(WPF.MDI.Event.ClosingEventArgs e)
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
