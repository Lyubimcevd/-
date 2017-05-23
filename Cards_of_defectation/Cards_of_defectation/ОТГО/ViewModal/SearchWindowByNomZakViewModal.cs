using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Cards_of_defectation.Classes;

namespace Cards_of_defectation.ОТГО.ViewModal
{
    class SearchWindowByNomZakViewModal: INotifyPropertyChanged
    { 
        string text_for_filter_nom_zak;
        List<object> nom_zak_list;
        int current_length_of_nom_zak_filter;
        bool is_drop_down_nom_zak;

        public event PropertyChangedEventHandler PropertyChanged;

        public List<object> Nom_zak_list
    {
            get
            {
                return nom_zak_list;
            }
            set
            {
                nom_zak_list = value;
                OnPropertyChanged("Nom_zak_list");
            }
        }
        public string Text_for_filter_nom_zak
    {
            get
            {
                return text_for_filter_nom_zak;
            }
            set
            {
                text_for_filter_nom_zak = value;
                if (Nom_zak_list?.Count != 0 || current_length_of_nom_zak_filter > text_for_filter_nom_zak.Length || Nom_zak_list == null)
                Nom_zak_list = Server.InitServer().DataBase("uit")
                        .ExecuteCommand("select top 50 rtrim(nom_zak) from rz_plan_rabot where nom_zak like '"
                                        + text_for_filter_nom_zak + "%'");
                IsDropDownNomZak = true;
                current_length_of_nom_zak_filter = text_for_filter_nom_zak.Length;
            }
        }
        public bool IsDropDownNomZak
        {
            get
            {
                return is_drop_down_nom_zak;
            }
            set
            {
                is_drop_down_nom_zak = value;
                if (Nom_zak_list.Count == 0) is_drop_down_nom_zak = false;
                OnPropertyChanged("IsDropDownNomZak");
            }
        }
        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
