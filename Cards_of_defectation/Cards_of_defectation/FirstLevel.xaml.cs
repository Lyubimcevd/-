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
using System.Data;
using Cards_of_defectation.Classes;
using System.Collections.ObjectModel;

namespace Cards_of_defectation
{
    public partial class FirstLevel : Window
    {
        bool IsSave;
        FirstLevelViewModal flvm;

        public FirstLevel(int pnom_zay)
        {
            InitializeComponent();
            CommandBinding bind = new CommandBinding(ApplicationCommands.Print);
            bind.Executed += Print_Execute;
            this.CommandBindings.Add(bind);
            bind = new CommandBinding(ApplicationCommands.Save);
            bind.Executed += Save_Execute;
            this.CommandBindings.Add(bind);
            flvm = new FirstLevelViewModal(pnom_zay);
            main_grid.DataContext = flvm;            
            IsSave = true;
        }

        private void Print_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            Choice_of_podrazd CoP = new Choice_of_podrazd(flvm);
            CoP.Show();
        }
        private void Save_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            Server.InitServer().DataBase("test1").Table("select * from plan_rabot where nom_zay = " + flvm.Nom_zay)
                .UpdateServerData(flvm.SaveInPlanRabot());
            Server.InitServer().DataBase("test1").Table("select * from kart_defect where par = " + flvm.Id)
                .UpdateServerData(flvm.SaveInKartDefect());
            if (flvm.Id == 0) MessageBox.Show("Сохранено в план");
            else MessageBox.Show("Сохранено");
            IsSave = true;
        }
        private void Key_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
                if (e.OriginalSource is DataGridCell)
                    if (MessageBox.Show("Подтвердите удаление", "Подтверждение", MessageBoxButton.YesNo)
                        == MessageBoxResult.Yes)
                    {
                        switch ((sender as DataGrid).Name)
                        {
                            case "table_izgot":
                                if (flvm.Izgot.Count == (sender as DataGrid).SelectedItems.Count)
                                    flvm.Izgot.Add(new FirstLevelIzgotViewModal(flvm.Id));
                                break;
                            case "table_remont":
                                if (flvm.Remont.Count == (sender as DataGrid).SelectedItems.Count)
                                    flvm.Remont.Add(new FirstLevelRemontViewModal(flvm.Id));
                                break;
                            case "table_priobr":
                                if (flvm.Priobr.Count == (sender as DataGrid).SelectedItems.Count)
                                    flvm.Priobr.Add(new FirstLevelPriobrViewModal(flvm.Id));
                                break;
                            case "table_stor_rem":
                                if (flvm.Stor_rem.Count == (sender as DataGrid).SelectedItems.Count)
                                    flvm.Stor_rem.Add(new FirstLevelStorRemViewModal(flvm.Id));
                                break;
                        }
                        IsSave = false;
                    }
                    else e.Handled = true;
            if (e.Key == Key.Enter)
            {
                if (e.OriginalSource is DataGridCell)
                    if ((sender as DataGrid).SelectedIndex == (sender as DataGrid).Items.Count - 1)
                    {
                        switch ((sender as DataGrid).Name)
                        {
                            case "table_izgot":
                                flvm.Izgot.Add(new FirstLevelIzgotViewModal(flvm.Id));
                                break;
                            case "table_remont":
                                flvm.Remont.Add(new FirstLevelRemontViewModal(flvm.Id));
                                break;
                            case "table_priobr":
                                flvm.Priobr.Add(new FirstLevelPriobrViewModal(flvm.Id));
                                break;
                            case "table_stor_rem":
                                flvm.Stor_rem.Add(new FirstLevelStorRemViewModal(flvm.Id));
                                break;
                        }
                        IsSave = false;
                    }
                if (e.OriginalSource is TextBox)
                    if ((e.OriginalSource as TextBox).TemplatedParent!=null)
                    {
                        ComboBox tmp = (e.OriginalSource as TextBox).TemplatedParent as ComboBox;
                        tmp.SelectedItem = tmp.Items.CurrentItem;
                    }
            }
        }
        private void ComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {       
            var tb = (TextBox)e.OriginalSource;
            tb.Select(tb.SelectionStart + tb.SelectionLength, 0);
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach (FirstLevelIzgotViewModal row in flvm.Izgot)
                if (row.IsChange)
                {
                    IsSave = false;
                    break;
                }
            foreach (FirstLevelPriobrViewModal row in flvm.Priobr)
                if (row.IsChange)
                {
                    IsSave = false;
                    break;
                }
            foreach (FirstLevelRemontViewModal row in flvm.Remont)
                if (row.IsChange)
                {
                    IsSave = false;
                    break;
                }
            foreach (FirstLevelStorRemViewModal row in flvm.Stor_rem)
                if (row.IsChange)
                {
                    IsSave = false;
                    break;
                }
            if (flvm.IsChange) IsSave = false;
            if (!IsSave)
            {
                MessageBoxResult result = MessageBox.Show("Заявка не сохранена. Сохранить?", "Предупреждение", MessageBoxButton.YesNoCancel);
                if (result == MessageBoxResult.Yes) Save_Execute(null, null);
                if (result == MessageBoxResult.Cancel) e.Cancel = true;
            }
        }
    }
}
