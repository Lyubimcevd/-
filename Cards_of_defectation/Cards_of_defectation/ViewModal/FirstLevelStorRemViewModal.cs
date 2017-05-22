using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Cards_of_defectation.Classes
{
    public class SlugebZapiskaStorRemViewModal: INotifyPropertyChanged
    {       
        List<object> cherch_list, naim_list,zav_izgot_list;
        Row_in_kart_defect parent_row;
        string text_for_filter_cherch, text_for_filter_naim, text_for_filter_zav_izgot, color;
        bool is_drop_down_cherch, is_drop_down_naim, is_drop_down_zav_izgot, is_change;
        int current_length_of_cherch_filter, current_length_of_naim_filter, current_length_of_zav_izgot_filter;

        public event PropertyChangedEventHandler PropertyChanged;

        public SlugebZapiskaStorRemViewModal()
        {
            parent_row = new Row_in_kart_defect();
            zav_izgot_list = Server.InitServer().DataBase("cvodka")
                        .ExecuteCommand("select distinct Ltrim(rtrim(zakazchi_naim)) from zakazchi_naim");
            is_change = false;
        }
        public SlugebZapiskaStorRemViewModal(int id)
        {
            parent_row = new Row_in_kart_defect();
            parent_row.Par = id;
            if (id != 0)
                parent_row.Nom_zay = Server.InitServer().DataBase("test1")
                       .ExecuteCommand("select nom_zay from kart_defect where id = " + parent_row.Par)[0].ToString();
            zav_izgot_list = Server.InitServer().DataBase("cvodka")
                        .ExecuteCommand("select distinct Ltrim(rtrim(zakazchi_naim)) from zakazchi_naim");
            is_change = false;
        }
        public SlugebZapiskaStorRemViewModal(RowDefectViewModal row)
        {
            parent_row = row.Save;
            text_for_filter_cherch = parent_row.Obozn_det;
            text_for_filter_naim = parent_row.Naim;
            text_for_filter_zav_izgot = parent_row.Zavod_izgot;
            zav_izgot_list = Server.InitServer().DataBase("cvodka")
                        .ExecuteCommand("select distinct Ltrim(rtrim(zakazchi_naim)) from zakazchi_naim");
            is_change = false;
            IsRight();
        }
        public string SelectedCherch
        {
            get
            {
                return parent_row.Obozn_det;
            }
            set
            {
                parent_row.Obozn_det = value;
                is_change = true;
                if (SelectedNaim == null)
                {
                    Naim_list = Server.InitServer().DataBase("cvodka")
                       .ExecuteCommand("select top 50 ltrim(rtrim(naim)) from naim where ltrim(nom) = '"
                                       + parent_row.Obozn_det + "'");
                    if (Naim_list.Count == 1)
                    {
                        SelectedNaim = Naim_list.First().ToString();
                        OnPropertyChanged("SelectedNaim");
                    }
                }
                else IsRight();
            }
        }
        public string SelectedNaim
        {
            get
            {
                return parent_row.Naim;
            }
            set
            {
                parent_row.Naim = value;
                is_change = true;
                if (SelectedCherch == null)
                {
                    Cherch_list = Server.InitServer().DataBase("uit")
                            .ExecuteCommand("select distinct top 50 ltrim(rtrim(nc)) from table_nc1 where nc"
                                        + " in (select nom from cvodka.dbo.naim where ltrim(naim) = '" + parent_row.Naim + "')");
                    if (Cherch_list.Count == 1)
                    {
                        SelectedCherch = Cherch_list.First().ToString();
                        OnPropertyChanged("SelectedCherch");
                    }
                }
                else IsRight();
            }
        }
        public string SelectedZav_izgot
        {
            get
            {
                return parent_row.Zavod_izgot;
            }
            set
            {
                parent_row.Zavod_izgot = value;
                is_change = true;
            }
        }
        public float Kolvo
        {
            get
            {
                return parent_row.Kolvo;
            }
            set
            {
                parent_row.Kolvo = value;
                is_change = true;
                OnPropertyChanged("ColorKolvo");
            }
        }
        public string Prim
        {
            get
            {
                return parent_row.Prim;
            }
            set
            {
                parent_row.Prim = value;
                is_change = true;
            }
        }
        public string Zavod_izgotov
        {
            get
            {
                return parent_row.Zavod_izgot;
            }
            set
            {
                parent_row.Zavod_izgot = value;
                is_change = true;
            }
        }
        public List<object> Cherch_list
        {
            get
            {
                return cherch_list;
            }
            set
            {
                cherch_list = value;
                OnPropertyChanged("Cherch_list");
            }
        }
        public List<object> Naim_list
        {
            get
            {
                return naim_list;
            }
            set
            {
                naim_list = value;
                OnPropertyChanged("Naim_list");
            }
        }
        public List<object> Zav_izgot_list
        {
            get
            {
                return zav_izgot_list;
            }
            set
            {
                zav_izgot_list = value;
                OnPropertyChanged("Zav_izgot_list");
            }
        }
        public string Text_for_filter_cherch
        {
            get
            {
                return text_for_filter_cherch;
            }
            set
            {
                text_for_filter_cherch = value;               
                if (Cherch_list?.Count != 0 || current_length_of_cherch_filter > text_for_filter_cherch.Length||Cherch_list == null)
                    Cherch_list = Server.InitServer().DataBase("uit")
                        .ExecuteCommand("select distinct top 50 Ltrim(rtrim(nc)) from table_nc1 where ltrim(nc) like '"
                                        + text_for_filter_cherch + "%'");
                if (Cherch_list.Count != 0) IsDropDownCherch = true;
                current_length_of_cherch_filter = text_for_filter_cherch.Length;
            }
        }
        public string Text_for_filter_naim
        {
            get
            {
                return text_for_filter_naim;
            }
            set
            {
                text_for_filter_naim = value;               
                if (Naim_list?.Count != 0 || current_length_of_naim_filter > text_for_filter_naim.Length||Naim_list == null)
                    Naim_list = Server.InitServer().DataBase("cvodka")
                        .ExecuteCommand("select distinct top 50 Ltrim(rtrim(naim)) from naim where ltrim(naim) like '"
                                        + text_for_filter_naim + "%'");
                if (Naim_list.Count != 0) IsDropDownNaim = true;
                current_length_of_naim_filter = text_for_filter_naim.Length;
            }
        }
        public string Text_for_filter_zav_izgot
        {
            get
            {
                return text_for_filter_zav_izgot;
            }
            set
            {
                text_for_filter_zav_izgot = value;
                if (Zav_izgot_list?.Count != 0 || current_length_of_zav_izgot_filter > text_for_filter_zav_izgot.Length || Zav_izgot_list == null)
                    Zav_izgot_list = Server.InitServer().DataBase("cvodka")
                        .ExecuteCommand("select distinct Ltrim(rtrim(zakazchi_naim)) from zakazchi_naim where ltrim(zakazchi_naim) like '%"
                                        + text_for_filter_zav_izgot + "%'");
                if (Zav_izgot_list.Count != 0) IsDropDownZav_izgot = true;
                current_length_of_zav_izgot_filter = text_for_filter_zav_izgot.Length;
            }
        }
        public bool IsDropDownCherch
        {
            get
            {
                return is_drop_down_cherch;
            }
            set
            {
                is_drop_down_cherch = value;
                OnPropertyChanged("IsDropDownCherch");
            }
        }
        public bool IsDropDownNaim
        {
            get
            {
                return is_drop_down_naim;
            }
            set
            {
                is_drop_down_naim = value;
                OnPropertyChanged("IsDropDownNaim");
            }
        }
        public bool IsDropDownZav_izgot
        {
            get
            {
                return is_drop_down_zav_izgot;
            }
            set
            {
                is_drop_down_zav_izgot = value;
                OnPropertyChanged("IsDropDownZav_izgot");
            }
        }
        public bool IsChange
        {
            get
            {
                return is_change;
            }
        }
        public string Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
                OnPropertyChanged("Color");
            }
        }
        public string ColorKolvo
        {
            get
            {
                if (Kolvo == 0) return "Red";
                else return "Black";
            }
        }

        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public Row_in_kart_defect Save
        {
            get
            {
                if (parent_row.Naim == null) parent_row.Naim = Text_for_filter_naim;
                if (parent_row.Obozn_det == null) parent_row.Obozn_det = Text_for_filter_cherch;
                if (parent_row.Naim == null && parent_row.Obozn_det == null) return null;
                if (parent_row.Data_post == null)
                    parent_row.Data_post = DateTime.Now.ToShortDateString();
                parent_row.Spos_ustr = 1;
                parent_row.Zavod_izgot = Zavod_izgotov;
                is_change = false;
                return parent_row;
            }
        }
        void IsRight()
        {
            if (parent_row.Obozn_det != null)
            {
                List<object> tmp = Server.InitServer().DataBase("cvodka")
                   .ExecuteCommand("select ltrim(rtrim(naim)) from naim where ltrim(nom) = '"
                   + parent_row.Obozn_det + "'");
                if (tmp.Count != 0)
                    if (tmp[0].ToString() != parent_row.Naim) Color = "Red";
                    else Color = null;
            }
        }
    }
}
