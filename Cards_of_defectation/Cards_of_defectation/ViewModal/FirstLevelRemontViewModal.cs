﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Cards_of_defectation.Classes;

namespace Cards_of_defectation.ViewModal
{
    public class SlugebZapiskaRemontViewModal: INotifyPropertyChanged
    {     
        List<object> cherch_list, naim_list;
        Row_in_kart_defect parent_row;
        string text_for_filter_cherch, text_for_filter_naim, color;
        bool is_drop_down_cherch, is_drop_down_naim, is_change, is_navigate;
        int current_length_of_cherch_filter, current_length_of_naim_filter;

        public event PropertyChangedEventHandler PropertyChanged;

        public SlugebZapiskaRemontViewModal()
        {
            parent_row = new Row_in_kart_defect();
            is_change = false;
        }
        public SlugebZapiskaRemontViewModal(int id)
        {
            parent_row = new Row_in_kart_defect();
            parent_row.Par = id;
            if (id != 0)
                parent_row.Nom_sz = Server.GetServer.DataBase("uit")
                       .ExecuteCommand("select nom_sz from rz_kart_defect where id = " + parent_row.Par)[0].ToString();
            is_change = false;
        }
        public SlugebZapiskaRemontViewModal(RowDefectViewModal row)
        {
            parent_row = row.Save;           
            text_for_filter_cherch = parent_row.Cherch;
            text_for_filter_naim = parent_row.Naim;
            is_change = false;
            IsRight();
        }
        public string SelectedCherch
        {
            get
            {
                return parent_row.Cherch;
            }
            set
            {
                if (!IsNavigate)
                {
                    parent_row.Cherch = value;
                    is_change = true;
                    if (SelectedNaim == null)
                    {
                        Naim_list = Server.GetServer.DataBase("cvodka")
                                .ExecuteCommand("select top 50 ltrim(rtrim(naim)) from naim where ltrim(rtrim(nom)) = '"
                                                + parent_row.Cherch + "'");
                        if (Naim_list.Count == 1)
                        {
                            SelectedNaim = Naim_list.First().ToString();
                            OnPropertyChanged("SelectedNaim");
                            IsDropDownNaim = false;
                        }
                        if (Naim_list.Count == 0) Naim_list = null;
                    }
                    else IsRight();
                }
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
                if (!IsNavigate)
                {
                    parent_row.Naim = value;
                    is_change = true;
                    if (SelectedCherch == null)
                    {
                        Cherch_list = Server.GetServer.DataBase("uit")
                          .ExecuteCommand("select distinct top 50 ltrim(rtrim(nc)) from table_nc1 where nc"
                                            + " in (select nom from cvodka.dbo.naim where ltrim(naim) = '" + parent_row.Naim + "')");
                        if (Cherch_list.Count == 1)
                        {
                            SelectedCherch = Cherch_list.First().ToString();
                            OnPropertyChanged("SelectedCherch");
                            IsDropDownCherch = false;
                        }
                        if (Cherch_list.Count == 0) Cherch_list = null;
                    }
                    else IsRight();
                }
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
        public string Text_for_filter_cherch
        {
            get
            {
                return text_for_filter_cherch;
            }
            set
            {
                text_for_filter_cherch = value;
                if (!IsNavigate)
                {
                    if (Cherch_list?.Count != 0 || current_length_of_cherch_filter > text_for_filter_cherch.Length || Cherch_list == null)
                        Cherch_list = Server.GetServer.DataBase("uit")
                            .ExecuteCommand("select distinct top 50 Ltrim(rtrim(nc)) from table_nc1 where ltrim(nc) like '"
                                            + text_for_filter_cherch + "%'");
                    if (Cherch_list.Count != 0) IsDropDownCherch = true;
                    current_length_of_cherch_filter = text_for_filter_cherch.Length;
                }
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
                if (!IsNavigate)
                {
                    if (Naim_list?.Count != 0 || current_length_of_naim_filter > text_for_filter_naim.Length || Naim_list == null)
                        Naim_list = Server.GetServer.DataBase("cvodka")
                            .ExecuteCommand("select distinct top 50 Ltrim(rtrim(naim)) from naim where naim like '%"
                                            + text_for_filter_naim + "%'");
                    if (Naim_list.Count != 0) IsDropDownNaim = true;
                    current_length_of_naim_filter = text_for_filter_naim.Length;
                }
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
        public bool IsNavigate
        {
            get
            {
                return is_navigate;
            }
            set
            {
                is_navigate = value;
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
                if (parent_row.Cherch == null) parent_row.Cherch = Text_for_filter_cherch;
                if (parent_row.Naim == null && parent_row.Cherch == null) return null;
                if (parent_row.Data_post == null)
                    parent_row.Data_post = DateTime.Now.ToShortDateString();
                parent_row.Spos_ustr = References.GetReferences.GetId("rz_spos_ustr","Дефектация");
                is_change = false;
                return parent_row;
            }
        }
        void IsRight()
        {
            if (parent_row.Cherch != null)
            {
                List<object> tmp = Server.GetServer.DataBase("cvodka")
                   .ExecuteCommand("select ltrim(rtrim(naim)) from naim where ltrim(nom) = '"
                   + parent_row.Cherch + "'");
                if (tmp.Count != 0)
                    if (tmp[0].ToString() != parent_row.Naim) Color = "Red";
                    else Color = null;
            }
        }
    }
}
