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
        string text_for_filter_nom_zay;
        List<object> nom_zay_list;
        int current_length_of_nom_zay_filter;
        bool is_drop_down_nom_zay;

        public event PropertyChangedEventHandler PropertyChanged;

        public List<object> Nom_zay_list
        {
            get
            {
                return nom_zay_list;
            }
            set
            {
                nom_zay_list = value;
                OnPropertyChanged("Nom_zay_list");
            }
        }
        public string Text_for_filter_nom_zay
        {
            get
            {
                return text_for_filter_nom_zay;
            }
            set
            {
                text_for_filter_nom_zay = value;
                if (Nom_zay_list?.Count != 0 || current_length_of_nom_zay_filter > text_for_filter_nom_zay.Length || Nom_zay_list == null)
                    Nom_zay_list = Server.InitServer().DataBase("test1")
                        .ExecuteCommand("select distinct top 50 rtrim(nom_zay) from plan_rabot where nom_zay like '"
                                        + text_for_filter_nom_zay + "%'");
                IsDropDownNomZay = true;
                current_length_of_nom_zay_filter = text_for_filter_nom_zay.Length;
            }
        }
        public bool IsDropDownNomZay
        {
            get
            {
                return is_drop_down_nom_zay;
            }
            set
            {
                is_drop_down_nom_zay = value;
                if (Nom_zay_list.Count == 0) is_drop_down_nom_zay = false;
                OnPropertyChanged("IsDropDownNomZay");
            }
        }
        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
