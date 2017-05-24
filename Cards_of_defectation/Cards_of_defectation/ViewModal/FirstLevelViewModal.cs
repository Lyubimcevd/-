using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Windows.Input;
using Cards_of_defectation.Classes;
using Cards_of_defectation.ОУП.Windows;

namespace Cards_of_defectation.ViewModal
{
    public class SlugebZapiskaViewModal: INotifyPropertyChanged
    {
        string izdelie, text_for_filter_ser_nom, text_for_filter_kontract;
        int id,current_length_of_ser_nom_filter, current_length_of_kontract_filter;
        ObservableCollection<SlugebZapiskaIzgotViewModal> izgot;
        ObservableCollection<SlugebZapiskaRemontViewModal> remont;
        ObservableCollection<SlugebZapiskaPriobrViewModal> priobr;
        ObservableCollection<SlugebZapiskaStorRemViewModal> stor_rem;
        List<object> ser_nom_list,kontract_list;
        bool is_change, is_drop_down_ser_nom, is_drop_down_kontract;
        Row_in_plan_rabot save_row;

        public event PropertyChangedEventHandler PropertyChanged;

        public SlugebZapiskaViewModal(string pnom_sz)
        {
            save_row = (Server.InitServer().DataBase("uit").Table("select * from rz_plan_rabot where nom_sz = " 
                + pnom_sz).LoadFromServer() as List<Row_in_plan_rabot>)[0];
            id = MainOUP.GetIndexOfKartDefect(pnom_sz);
            text_for_filter_ser_nom = save_row.Ser_nom;
            text_for_filter_kontract = save_row.Nom_kont;

            izgot = new ObservableCollection<SlugebZapiskaIzgotViewModal>();
            ObservableCollection<RowDefectViewModal> tmp_list = Converter.ToViewModal(Server.InitServer()
                .DataBase("uit").Table("select * from rz_kart_defect where par = " + id + " and spos_ustr = 3")
                .LoadFromServer() as List<Row_in_kart_defect>);
            foreach (RowDefectViewModal row in tmp_list) izgot.Add(new SlugebZapiskaIzgotViewModal(row));
            if (izgot.Count == 0) izgot.Add(new SlugebZapiskaIzgotViewModal(id));

            remont = new ObservableCollection<SlugebZapiskaRemontViewModal>();
            tmp_list = Converter.ToViewModal(Server.InitServer().DataBase("uit")
                .Table("select * from rz_kart_defect where par = " + id + " and spos_ustr = 1")
                .LoadFromServer() as List<Row_in_kart_defect>);
            foreach (RowDefectViewModal row in tmp_list) remont.Add(new SlugebZapiskaRemontViewModal(row));
            if (remont.Count == 0) remont.Add(new SlugebZapiskaRemontViewModal(id));

            priobr = new ObservableCollection<SlugebZapiskaPriobrViewModal>();
            tmp_list = Converter.ToViewModal(Server.InitServer().DataBase("uit")
                .Table("select * from rz_kart_defect where par = " + id + " and spos_ustr = 4")
                .LoadFromServer() as List<Row_in_kart_defect>);
            foreach (RowDefectViewModal row in tmp_list) priobr.Add(new SlugebZapiskaPriobrViewModal(row));
            if (priobr.Count == 0) priobr.Add(new SlugebZapiskaPriobrViewModal(id));

            stor_rem = new ObservableCollection<SlugebZapiskaStorRemViewModal>();
            tmp_list = Converter.ToViewModal(Server.InitServer().DataBase("uit")
                .Table("select * from rz_kart_defect where par = " + id + " and spos_ustr = 2")
                .LoadFromServer() as List<Row_in_kart_defect>);
            foreach (RowDefectViewModal row in tmp_list) stor_rem.Add(new SlugebZapiskaStorRemViewModal(row));
            if (stor_rem.Count == 0) stor_rem.Add(new SlugebZapiskaStorRemViewModal(id));

            DefaultAction();
        }
        public SlugebZapiskaViewModal(string pnom_sz,string pser_nom)
        {
            save_row = new Row_in_plan_rabot();
            save_row.Nom_sz = pnom_sz;
            save_row.Ser_nom = pser_nom;
            text_for_filter_ser_nom = pser_nom;
            izgot = new ObservableCollection<SlugebZapiskaIzgotViewModal>();
            izgot.Add(new SlugebZapiskaIzgotViewModal());
            remont = new ObservableCollection<SlugebZapiskaRemontViewModal>();
            remont.Add(new SlugebZapiskaRemontViewModal());
            priobr = new ObservableCollection<SlugebZapiskaPriobrViewModal>();
            priobr.Add(new SlugebZapiskaPriobrViewModal());
            stor_rem = new ObservableCollection<SlugebZapiskaStorRemViewModal>();
            stor_rem.Add(new SlugebZapiskaStorRemViewModal());
            DefaultAction();
        }
        void DefaultAction()
        {
            kontract_list = Server.InitServer().DataBase("cvodka")
                      .ExecuteCommand("select distinct top 50 Ltrim(rtrim(t.kontr)) from "
                      + "(SELECT sp34360+'/'+descr as kontr FROM[IZ_1C].[sql].[dbo].[SC33852] where sp34360 <> '')"
                      + "as t");
            ser_nom_list = Server.InitServer().DataBase("uit")
                      .ExecuteCommand("select top 50 ser_nom from rz_ser_nom_naim");
            izdelie = Server.InitServer().DataBase("uit")
               .ExecuteCommand("select naim from rz_ser_nom_naim where ser_nom = '"
               + text_for_filter_ser_nom + "'")[0].ToString();
            is_change = false;
        }
        public string Text_for_filter_ser_nom
        {
            get
            {
                return text_for_filter_ser_nom;
            }
            set
            {              
                text_for_filter_ser_nom = value;               
                if (Ser_nom_list?.Count != 0 || current_length_of_ser_nom_filter > text_for_filter_ser_nom.Length||Ser_nom_list == null)
                    Ser_nom_list = Server.InitServer().DataBase("uit")
                        .ExecuteCommand("select top 50 ser_nom from rz_ser_nom_naim where ser_nom like '"
                                        + text_for_filter_ser_nom + "%'");
                if (Ser_nom_list.Count != 0) IsDropDownSer_nom = true;
                current_length_of_ser_nom_filter = text_for_filter_ser_nom.Length;
            }
        }
        public string Text_for_filter_kontract
        {
            get
            {
                return text_for_filter_kontract;
            }
            set
            {
                text_for_filter_kontract = value;          
                if (Kontract_list?.Count != 0 || current_length_of_kontract_filter > text_for_filter_kontract.Length||Kontract_list == null)
                    Kontract_list = Server.InitServer().DataBase("cvodka")
                        .ExecuteCommand("select distinct top 50 Ltrim(rtrim(t.kontr)) from "
                        + "(SELECT sp34360+'/'+descr as kontr FROM[IZ_1C].[sql].[dbo].[SC33852] where sp34360 <> '')"
                        + "as t where ltrim(t.kontr) like '" + text_for_filter_kontract + "%'");
                if (Kontract_list.Count != 0) IsDropDownKontract = true;
                current_length_of_kontract_filter = text_for_filter_kontract.Length;
            }
        }
        public string SelectedSer_nom
        {
            get
            {
                return save_row.Ser_nom;
            }
            set
            {
                save_row.Ser_nom = value;
                izdelie = Server.InitServer().DataBase("uit")
                   .ExecuteCommand("select naim from rz_ser_nom_naim where ser_nom = '"
                   + save_row.Ser_nom + "'")[0].ToString();
                OnPropertyChanged("Izdelie");
                OnPropertyChanged("SelectedSer_nom");
                is_change = true;
            }
        }
        public string SelectedKontract
        {
            get
            {
                return save_row.Nom_kont;
            }
            set
            {
                save_row.Nom_kont = value;
                is_change = true;
            }
        }
        public List<object> Ser_nom_list
        {
            get
            {
                return ser_nom_list;
            }
            set
            {
                ser_nom_list = value;
                OnPropertyChanged("Ser_nom_list");
            }
        }
        public List<object> Kontract_list
        {
            get
            {
                return kontract_list;
            }
            set
            {
                kontract_list = value;
                OnPropertyChanged("Kontract_list");
            }
        }
        public string Izdelie
        {
            get
            {
                return izdelie;
            }
        }
        public string Voin_chast
        {
            get
            {
                return save_row.Voin_chast;
            }
            set
            {
                save_row.Voin_chast = value;
                is_change = true;
            }
        }
        public string Nom_zak
        {
            get
            {
                return save_row.Nom_zak;
            }
            set
            {
                save_row.Nom_zak = value;
                is_change = true;
            }
        }
        public string Srok_otprav
        {
            get
            {
                return save_row.Srok_rem;
            }
            set
            {
                save_row.Srok_rem = value;
                is_change = true;
            }
        }
        public string Proizv_chast
        {
            get
            {
                return save_row.Prim;
            }
            set
            {
                save_row.Prim = value;
                is_change = true;
            }
        }
        public string Nom_sz
        {
            get
            {
                if (save_row.Nom_sz == null)
                    save_row.Nom_sz = Server.InitServer().DataBase("uit")
                        .ExecuteCommand("select nom_sz from rz_plan_rabot")[0].ToString();
                return save_row.Nom_sz;
            }
        }
        public bool IsChange
        {
            get
            {
                return is_change;
            }
        }
        public bool IsDropDownSer_nom
        {
            get
            {
                return is_drop_down_ser_nom;
            }
            set
            {
                is_drop_down_ser_nom = value;
                OnPropertyChanged("IsDropDownSer_nom");
            }
        }
        public bool IsDropDownKontract
        {
            get
            {
                return is_drop_down_kontract;
            }
            set
            {
                is_drop_down_kontract = value;
                OnPropertyChanged("IsDropDownKontract");
            }
        }
        public int Id
        {
            get
            {
                return id;
            }
        }
        public ObservableCollection<SlugebZapiskaIzgotViewModal> Izgot
        {
            get
            {            
                return izgot;
            }
        }
        public ObservableCollection<SlugebZapiskaRemontViewModal> Remont
        {
            get
            {
                return remont;
            }
        }
        public ObservableCollection<SlugebZapiskaPriobrViewModal> Priobr
        {
            get
            {
                return priobr;
            }
        }
        public ObservableCollection<SlugebZapiskaStorRemViewModal> Stor_rem
        {
            get
            {
                return stor_rem;
            }
        }

        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public List<Row_in_kart_defect> SaveInKartDefect()
        {  
            List<Row_in_kart_defect> result = new List<Row_in_kart_defect>();
            foreach (SlugebZapiskaIzgotViewModal row in izgot)
            {
                if (row.Save != null)
                {
                    if (row.Save.Nom_sz == null)
                    {
                        row.Save.Nom_sz = save_row.Nom_sz;
                        row.Save.Par = id;
                    }
                    result.Add(row.Save);
                }
            }
            foreach (SlugebZapiskaRemontViewModal row in remont)
            {
                if (row.Save != null)
                {
                    if (row.Save.Nom_sz == null)
                    {
                        row.Save.Nom_sz = save_row.Nom_sz;
                        row.Save.Par = id;
                    }
                    result.Add(row.Save);
                }
            }
            foreach (SlugebZapiskaPriobrViewModal row in priobr)
            {
                if (row.Save != null)
                {
                    if (row.Save.Nom_sz == null)
                    {
                        row.Save.Nom_sz = save_row.Nom_sz;
                        row.Save.Par = id;
                    }
                    result.Add(row.Save);
                }
            }
            foreach (SlugebZapiskaStorRemViewModal row in stor_rem)
            {
                if (row.Save != null)
                {
                    if (row.Save.Nom_sz == null)
                    {
                        row.Save.Nom_sz = save_row.Nom_sz;
                        row.Save.Par = id;
                    }
                    result.Add(row.Save);
                }
            }
            is_change = false;
            return result;
        }
        public List<Row_in_plan_rabot> SaveInPlanRabot()
        {
            if (Server.InitServer().DataBase("uit")
                .ExecuteCommand("select * from rz_plan_rabot where nom_sz = '" + save_row.Ser_nom + "'").Count == 0)
            {
                List<Row_in_plan_rabot> result = new List<Row_in_plan_rabot>();
                result.Add(save_row);
                id = MainOUP.GetIndexOfKartDefect(save_row.Nom_sz);
                return result;
            }
            else
            {
                Server.InitServer().DataBase("uit")
                    .ExecuteCommand("update rz_plan_rabot set nom_sz = '" + save_row.Nom_sz
                    + "' where nom_sz = '" + save_row.Ser_nom);
                return null;
            }
        }
    }
}
