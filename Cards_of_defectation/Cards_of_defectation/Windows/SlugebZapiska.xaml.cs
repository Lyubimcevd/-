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
using Cards_of_defectation.ViewModal;
using Cards_of_defectation.ОУП.Windows;

namespace Cards_of_defectation.Windows
{
    public partial class SlugebZapiska : Window
    {
        SlugebZapiskaViewModal SZVM;
        bool IsSave;

        public SlugebZapiska(string pNom_sz)
        {
            InitializeComponent();
            SZVM = new SlugebZapiskaViewModal(pNom_sz);
            DefaultAction();
        }
        public SlugebZapiska(string pNom_sz,string ser_nom)
        {
            InitializeComponent();
            SZVM = new SlugebZapiskaViewModal(pNom_sz, ser_nom);
            DefaultAction();
        }
        void DefaultAction()
        {         
            CommandBinding bind = new CommandBinding(ApplicationCommands.Print);
            bind.Executed += Print_Execute;
            this.CommandBindings.Add(bind);
            bind = new CommandBinding(ApplicationCommands.Save);
            bind.Executed += Save_Execute;
            this.CommandBindings.Add(bind);
            main_grid.DataContext = SZVM;
            IsSave = true;
        }

        private void Print_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            Choice_of_podrazd CoP = new Choice_of_podrazd(SZVM);
            CoP.Show();
        }
        private void Save_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            if (SZVM.SaveInPlanRabot() != null)
            {
                Server.GetServer.DataBase("uit").Table("select * from rz_plan_rabot where nom_sz = '" + SZVM.Nom_sz+"'")
                    .UpdateServerData(SZVM.SaveInPlanRabot());
            }
            Server.GetServer.DataBase("uit").Table("select * from rz_kart_defect where par = " 
                + SZVM.Id + " and spos_ustr != "+References.GetReferences.GetId("rz_spos_ustr","Ремонт"))
                .UpdateServerData(SZVM.SaveInKartDefect());
            if (SZVM.Id == 0) MessageBox.Show("Сохранено в план");
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
                                if (SZVM.Izgot.Count == (sender as DataGrid).SelectedItems.Count)
                                    SZVM.Izgot.Add(new SlugebZapiskaIzgotViewModal(SZVM.Id));
                                break;
                            case "table_remont":
                                if (SZVM.Remont.Count == (sender as DataGrid).SelectedItems.Count)
                                    SZVM.Remont.Add(new SlugebZapiskaRemontViewModal(SZVM.Id));
                                break;
                            case "table_priobr":
                                if (SZVM.Priobr.Count == (sender as DataGrid).SelectedItems.Count)
                                    SZVM.Priobr.Add(new SlugebZapiskaPriobrViewModal(SZVM.Id));
                                break;
                            case "table_stor_rem":
                                if (SZVM.Stor_rem.Count == (sender as DataGrid).SelectedItems.Count)
                                    SZVM.Stor_rem.Add(new SlugebZapiskaStorRemViewModal(SZVM.Id));
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
                                SZVM.Izgot.Add(new SlugebZapiskaIzgotViewModal(SZVM.Id));
                                break;
                            case "table_remont":
                                SZVM.Remont.Add(new SlugebZapiskaRemontViewModal(SZVM.Id));
                                break;
                            case "table_priobr":
                                SZVM.Priobr.Add(new SlugebZapiskaPriobrViewModal(SZVM.Id));
                                break;
                            case "table_stor_rem":
                                SZVM.Stor_rem.Add(new SlugebZapiskaStorRemViewModal(SZVM.Id));
                                break;
                        }
                        IsSave = false;
                    }
                if (e.OriginalSource is TextBox)
                    if ((e.OriginalSource as TextBox).TemplatedParent!=null)
                    {
                        ComboBox tmp = (e.OriginalSource as TextBox).TemplatedParent as ComboBox;
                        tmp.SelectedItem = tmp.Text;
                    }
                if ((sender as DataGrid).Columns.IndexOf((sender as DataGrid).CurrentColumn) == 2
                        || (sender as DataGrid).Columns.IndexOf((sender as DataGrid).CurrentColumn) == 3)
                    (sender as DataGrid).CurrentColumn = (sender as DataGrid).Columns[0];
            }
        }
        private void ComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {       
            var tb = (TextBox)e.OriginalSource;
            tb.Select(tb.SelectionStart + tb.SelectionLength, 0);
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach (SlugebZapiskaIzgotViewModal row in SZVM.Izgot)
                if (row.IsChange)
                {
                    IsSave = false;
                    break;
                }
            foreach (SlugebZapiskaPriobrViewModal row in SZVM.Priobr)
                if (row.IsChange)
                {
                    IsSave = false;
                    break;
                }
            foreach (SlugebZapiskaRemontViewModal row in SZVM.Remont)
                if (row.IsChange)
                {
                    IsSave = false;
                    break;
                }
            foreach (SlugebZapiskaStorRemViewModal row in SZVM.Stor_rem)
                if (row.IsChange)
                {
                    IsSave = false;
                    break;
                }
            if (SZVM.IsChange) IsSave = false;
            if (!IsSave)
            {
                MessageBoxResult result = MessageBox.Show("Заявка не сохранена. Сохранить?", "Предупреждение", MessageBoxButton.YesNoCancel);
                if (result == MessageBoxResult.Yes) Save_Execute(null, null);
                if (result == MessageBoxResult.Cancel) e.Cancel = true;
            }
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            if (e.OriginalSource is TextBox)
                if ((e.OriginalSource as TextBox).TemplatedParent is ComboBox)
                {
                    ComboBox tmp = (e.OriginalSource as TextBox).TemplatedParent as ComboBox;
                    switch (e.Key)
                    {
                        case Key.Down:
                            e.Handled = true;
                            if (tmp.DataContext is SlugebZapiskaViewModal) (tmp.DataContext as SlugebZapiskaViewModal).IsNavigate = true;
                            if (tmp.DataContext is SlugebZapiskaIzgotViewModal) (tmp.DataContext as SlugebZapiskaIzgotViewModal).IsNavigate = true;
                            if (tmp.DataContext is SlugebZapiskaPriobrViewModal) (tmp.DataContext as SlugebZapiskaPriobrViewModal).IsNavigate = true;
                            if (tmp.DataContext is SlugebZapiskaRemontViewModal) (tmp.DataContext as SlugebZapiskaRemontViewModal).IsNavigate = true;
                            if (tmp.DataContext is SlugebZapiskaStorRemViewModal) (tmp.DataContext as SlugebZapiskaStorRemViewModal).IsNavigate = true;
                            if (tmp.SelectedIndex < tmp.Items.Count) tmp.SelectedIndex++;
                            break;
                        case Key.Up:
                            e.Handled = true;
                            if (tmp.DataContext is SlugebZapiskaViewModal) (tmp.DataContext as SlugebZapiskaViewModal).IsNavigate = true;
                            if (tmp.DataContext is SlugebZapiskaIzgotViewModal) (tmp.DataContext as SlugebZapiskaIzgotViewModal).IsNavigate = true;
                            if (tmp.DataContext is SlugebZapiskaPriobrViewModal) (tmp.DataContext as SlugebZapiskaPriobrViewModal).IsNavigate = true;
                            if (tmp.DataContext is SlugebZapiskaRemontViewModal) (tmp.DataContext as SlugebZapiskaRemontViewModal).IsNavigate = true;
                            if (tmp.DataContext is SlugebZapiskaStorRemViewModal) (tmp.DataContext as SlugebZapiskaStorRemViewModal).IsNavigate = true;
                            if (tmp.SelectedIndex > 0) tmp.SelectedIndex--;
                            break;
                        default:
                            if (tmp.DataContext is SlugebZapiskaViewModal) (tmp.DataContext as SlugebZapiskaViewModal).IsNavigate = false;
                            if (tmp.DataContext is SlugebZapiskaIzgotViewModal) (tmp.DataContext as SlugebZapiskaIzgotViewModal).IsNavigate = false;
                            if (tmp.DataContext is SlugebZapiskaPriobrViewModal) (tmp.DataContext as SlugebZapiskaPriobrViewModal).IsNavigate = false;
                            if (tmp.DataContext is SlugebZapiskaRemontViewModal) (tmp.DataContext as SlugebZapiskaRemontViewModal).IsNavigate = false;
                            if (tmp.DataContext is SlugebZapiskaStorRemViewModal) (tmp.DataContext as SlugebZapiskaStorRemViewModal).IsNavigate = false;
                            base.OnPreviewKeyDown(e);
                            break;
                    }
                }
        }
    }
}
