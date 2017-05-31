using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Cards_of_defectation.Classes;

namespace Cards_of_defectation.ОТГО.ViewModal
{
    class SearchWindowSZViewModal: INotifyPropertyChanged
    {
        string text_for_filter_nom_sz;
        List<object> nom_sz_list;
        int current_length_of_nom_sz_filter;
        bool is_drop_down_nom_sz;

        public event PropertyChangedEventHandler PropertyChanged;

        public List<object> Nom_sz_list
        {
            get
            {
                return nom_sz_list;
            }
            set
            {
                nom_sz_list = value;
                OnPropertyChanged("Nom_sz_list");
            }
        }
        public string Text_for_filter_nom_sz
        {
            get
            {
                return text_for_filter_nom_sz;
            }
            set
            {
                text_for_filter_nom_sz = value;
                if (Nom_sz_list?.Count != 0 || current_length_of_nom_sz_filter > text_for_filter_nom_sz.Length || Nom_sz_list == null)
                    Nom_sz_list = Server.GetServer.DataBase("uit")
                        .ExecuteCommand("select top 50 rtrim(nom_sz) from rz_plan_rabot where nom_sz like '"
                                        + text_for_filter_nom_sz + "%'");
                IsDropDownNomZay = true;
                current_length_of_nom_sz_filter = text_for_filter_nom_sz.Length;
            }
        }
        public bool IsDropDownNomZay
        {
            get
            {
                return is_drop_down_nom_sz;
            }
            set
            {
                is_drop_down_nom_sz = value;
                if (Nom_sz_list.Count == 0) is_drop_down_nom_sz = false;
                OnPropertyChanged("IsDropDownNomZay");
            }
        }
        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
