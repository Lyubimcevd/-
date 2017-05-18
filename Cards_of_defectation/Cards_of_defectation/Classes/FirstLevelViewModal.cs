using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Windows.Input;

namespace Cards_of_defectation.Classes
{
    public class FirstLevelViewModal: INotifyPropertyChanged
    {
        string izdelie, text_for_filter_ser_nom, text_for_filter_kontract;
        int id,current_length_of_ser_nom_filter, current_length_of_kontract_filter;
        ObservableCollection<FirstLevelIzgotViewModal> izgot;
        ObservableCollection<FirstLevelRemontViewModal> remont;
        ObservableCollection<FirstLevelPriobrViewModal> priobr;
        ObservableCollection<FirstLevelStorRemViewModal> stor_rem;
        List<object> ser_nom_list,kontract_list;
        bool is_change, is_drop_down_ser_nom, is_drop_down_kontract;
        Row_in_plan_rabot save_row;

        public event PropertyChangedEventHandler PropertyChanged;

        public FirstLevelViewModal(int pnom_zay)
        {
            kontract_list = Server.InitServer().DataBase("cvodka")
                       .ExecuteCommand("select distinct top 50 Ltrim(rtrim(t.kontr)) from "
                       + "(SELECT sp34360+'/'+descr as kontr FROM[IZ_1C].[sql].[dbo].[SC33852] where sp34360 <> '')"
                       + "as t");
            ser_nom_list = Server.InitServer().DataBase("test1")
                       .ExecuteCommand("select distinct top 50 [Номер изделия] from nom_type");
            if (Server.InitServer().DataBase("test1")
               .ExecuteCommand("select * from plan_rabot where nom_zay = " + pnom_zay).Count != 0)
            {
                save_row = (Server.InitServer().DataBase("test1").Table("select * from plan_rabot where nom_zay = " 
                    + pnom_zay).LoadFromServer() as List<Row_in_plan_rabot>)[0];
                id = Convert.ToInt32(Server.InitServer().DataBase("test1")
                    .ExecuteCommand("select id from kart_defect where nom_zay =" 
                    + save_row.Nom_zay + " and par is null")[0]);
                izdelie = Server.InitServer().DataBase("test1")
                    .ExecuteCommand("select [Заводской номер изделия] from nom_type where [Номер изделия] = '" 
                    + save_row.Ser_nom_izd + "'")[0].ToString();
                text_for_filter_ser_nom = save_row.Ser_nom_izd;
                text_for_filter_kontract = save_row.Kontract;

                izgot = new ObservableCollection<FirstLevelIzgotViewModal>();
                ObservableCollection<RowDefectViewModal> tmp_list = Converter.ToViewModal(Server.InitServer()
                    .DataBase("test1").Table("select * from kart_defect where par = " + id + " and spos_ustr = 2")
                    .LoadFromServer() as List<Row_in_kart_defect>);
                foreach (RowDefectViewModal row in tmp_list) izgot.Add(new FirstLevelIzgotViewModal(row));
                if (izgot.Count == 0) izgot.Add(new FirstLevelIzgotViewModal(id));

                remont = new ObservableCollection<FirstLevelRemontViewModal>();
                tmp_list = Converter.ToViewModal(Server.InitServer().DataBase("test1")
                    .Table("select * from kart_defect where par = " + id + " and spos_ustr = 0")
                    .LoadFromServer() as List<Row_in_kart_defect>);
                foreach (RowDefectViewModal row in tmp_list) remont.Add(new FirstLevelRemontViewModal(row));
                if (remont.Count == 0) remont.Add(new FirstLevelRemontViewModal(id));

                priobr = new ObservableCollection<FirstLevelPriobrViewModal>();
                tmp_list = Converter.ToViewModal(Server.InitServer().DataBase("test1")
                    .Table("select * from kart_defect where par = " + id + " and spos_ustr = 3")
                    .LoadFromServer() as List<Row_in_kart_defect>);
                foreach (RowDefectViewModal row in tmp_list) priobr.Add(new FirstLevelPriobrViewModal(row));
                if (priobr.Count == 0) priobr.Add(new FirstLevelPriobrViewModal(id));

                stor_rem = new ObservableCollection<FirstLevelStorRemViewModal>();
                tmp_list = Converter.ToViewModal(Server.InitServer().DataBase("test1")
                    .Table("select * from kart_defect where par = " + id + " and spos_ustr = 1")
                    .LoadFromServer() as List<Row_in_kart_defect>);
                foreach (RowDefectViewModal row in tmp_list) stor_rem.Add(new FirstLevelStorRemViewModal(row));
                if (stor_rem.Count == 0) stor_rem.Add(new FirstLevelStorRemViewModal(id));
            }                        
            else
            {
                save_row = new Row_in_plan_rabot();
                save_row.Nom_zay = pnom_zay;
                izgot = new ObservableCollection<FirstLevelIzgotViewModal>();
                izgot.Add(new FirstLevelIzgotViewModal());
                remont = new ObservableCollection<FirstLevelRemontViewModal>();
                remont.Add(new FirstLevelRemontViewModal());
                priobr = new ObservableCollection<FirstLevelPriobrViewModal>();
                priobr.Add(new FirstLevelPriobrViewModal());
                stor_rem = new ObservableCollection<FirstLevelStorRemViewModal>();
                stor_rem.Add(new FirstLevelStorRemViewModal());
            }         
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
                    Ser_nom_list = Server.InitServer().DataBase("test1")
                        .ExecuteCommand("select distinct top 50 [Номер изделия] from nom_type where [Номер изделия] like '"
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
                return save_row.Ser_nom_izd;
            }
            set
            {
                save_row.Ser_nom_izd = value;
                izdelie = Server.InitServer().DataBase("test1")
                   .ExecuteCommand("select [Заводской номер изделия] from nom_type where [Номер изделия] = '"
                   + save_row.Ser_nom_izd + "'")[0].ToString();
                OnPropertyChanged("Izdelie");
                OnPropertyChanged("SelectedSer_nom");
                is_change = true;
            }
        }
        public string SelectedKontract
        {
            get
            {
                return save_row.Kontract;
            }
            set
            {
                save_row.Kontract = value;
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
        public int Nom_zak
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
                return save_row.Srok_vosstan;
            }
            set
            {
                save_row.Srok_vosstan = value;
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
        public int Nom_zay
        {
            get
            {
                if (save_row.Nom_zay == 0)
                    save_row.Nom_zay = Convert.ToInt32(Server.InitServer().DataBase("test1")
                        .ExecuteCommand("select nom_zay from plan_rabot")[0]);
                return save_row.Nom_zay;
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
        public string Last_prim
        {
            get
            {
                return save_row.Last_prim;
            }
            set
            {
                save_row.Last_prim = value;
                is_change = true;
            }
        }
        public int Id
        {
            get
            {
                return id;
            }
        }
        public ObservableCollection<FirstLevelIzgotViewModal> Izgot
        {
            get
            {            
                return izgot;
            }
        }
        public ObservableCollection<FirstLevelRemontViewModal> Remont
        {
            get
            {
                return remont;
            }
        }
        public ObservableCollection<FirstLevelPriobrViewModal> Priobr
        {
            get
            {
                return priobr;
            }
        }
        public ObservableCollection<FirstLevelStorRemViewModal> Stor_rem
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
            foreach (FirstLevelIzgotViewModal row in izgot)
            {
                if (row.Save != null)
                {
                    if (row.Save.Nom_zay == 0)
                    {
                        row.Save.Nom_zay = save_row.Nom_zay;
                        row.Save.Par = id;
                    }
                    result.Add(row.Save);
                }
            }
            foreach (FirstLevelRemontViewModal row in remont)
            {
                if (row.Save != null)
                {
                    if (row.Save.Nom_zay == 0)
                    {
                        row.Save.Nom_zay = save_row.Nom_zay;
                        row.Save.Par = id;
                    }
                    result.Add(row.Save);
                }
            }
            foreach (FirstLevelPriobrViewModal row in priobr)
            {
                if (row.Save != null)
                {
                    if (row.Save.Nom_zay == 0)
                    {
                        row.Save.Nom_zay = save_row.Nom_zay;
                        row.Save.Par = id;
                    }
                    result.Add(row.Save);
                }
            }
            foreach (FirstLevelStorRemViewModal row in stor_rem)
            {
                if (row.Save != null)
                {
                    if (row.Save.Nom_zay == 0)
                    {
                        row.Save.Nom_zay = save_row.Nom_zay;
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
            List<Row_in_plan_rabot> result = new List<Row_in_plan_rabot>();
            result.Add(save_row);
            if (save_row.Ser_nom_izd == null) save_row.Ser_nom_izd = text_for_filter_ser_nom;
            if (Server.InitServer().DataBase("test1")
                .ExecuteCommand("select id from kart_defect where nom_zay = " + save_row.Nom_zay).Count == 0)
            {
                List<Row_in_kart_defect> save_list = new List<Row_in_kart_defect>();
                save_list.Add(new Row_in_kart_defect(save_row));
                if (save_list[0].Obozn_det != null)
                {
                    Server.InitServer().DataBase("test1")
                        .Table("select * from kart_defect where nom_zay = " + save_row.Nom_zay)
                        .UpdateServerData(save_list);
                    id = Convert.ToInt32(Server.InitServer().DataBase("test1")
                        .ExecuteCommand("select id from kart_defect where nom_zay = " + save_row.Nom_zay)[0]);
                }
            }
            return result;
        }
    }
}
