﻿using System;
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
        bool IsSave = true;
        DataTable DT;

        public ReductionReference()
        {
            Log.Init.Info("Запуск ReductionReference");
            InitializeComponent();
            Log.Init.Info("Загрузка таблицы rz_spravochniki");
            dataGrid1.ItemsSource = Server.GetServer.DataBase("uit").Table("select naim from rz_spravochniki")
                .LoadTableFromServer().DefaultView;
            Log.Init.Info("Загружено");
        }

        private void CommandBinding_Save(object sender, ExecutedRoutedEventArgs e)
        {
            Log.Init.Info("Сохранение в "+table_name);
            DT = (dataGrid.ItemsSource as DataView).Table;
            ChangeColumnsName(DT);
            Server.GetServer.DataBase("uit").Table("select * from "+table_name).UpdateServerData(DT);
            ChangeColumnsName(DT);
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
                DT = Server.GetServer.DataBase("uit").Table("select * from " + table_name)
                    .LoadTableFromServer();
                ChangeColumnsName(DT);
                dataGrid.ItemsSource = DT.DefaultView;
                Log.Init.Info("загружено");
            }
        }

        void ChangeColumnsName(DataTable DT)
        {
            bool IsChange = false;
            foreach (DataColumn col in DT.Columns)
            {
                switch (col.ColumnName)
                {
                    case "naim":
                        col.ColumnName = "Наименование";
                        IsChange = true;
                        break;
                    case "ser_nom":
                        col.ColumnName = "Серийный номер";
                        IsChange = true;
                        break;
                    case "cherch":
                        col.ColumnName = "Чертёж";
                        IsChange = true;
                        break;
                }
            }
            if (!IsChange)
                foreach (DataColumn col in DT.Columns)
                {
                    switch (col.ColumnName)
                    {
                        case "Наименование":
                            col.ColumnName = "naim";
                            break;
                        case "Серийный номер":
                            col.ColumnName = "ser_nom";
                            break;
                        case "Чертёж":
                            col.ColumnName = "cherch";
                            break;
                    }
                }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!IsSave)
            {
                MessageBoxResult result = MessageBox.Show("Есть несохранённые изменения. Сохранить?", "Предупреждение", MessageBoxButton.YesNoCancel);
                if (result == MessageBoxResult.Yes) CommandBinding_Save(null, null);
                if (result == MessageBoxResult.Cancel) e.Cancel = true;
            }
        }

        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !IsSave;
        }

        private void dataGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            IsSave = false;
        }
    }
}
