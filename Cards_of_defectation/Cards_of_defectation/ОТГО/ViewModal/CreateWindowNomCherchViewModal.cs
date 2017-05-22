using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cards_of_defectation.Classes;
using System.ComponentModel;

namespace Cards_of_defectation.ОТГО.ViewModal
{
    class CreateWindowNomCherchViewModal : INotifyPropertyChanged
    {
        string text_for_filter_cherch;
        List<object> cherch_list;
        int current_length_of_cherch_filter;
        bool is_drop_down_cherch;

        public event PropertyChangedEventHandler PropertyChanged;

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
        public string Text_for_filter_cherch
        {
            get
            {
                return text_for_filter_cherch;
            }
            set
            {
                text_for_filter_cherch = value;
                if (Cherch_list?.Count != 0 || current_length_of_cherch_filter > text_for_filter_cherch.Length || Cherch_list == null)
                    Cherch_list = Server.InitServer().DataBase("uit")
                        .ExecuteCommand("select distinct top 50 Ltrim(rtrim(nc)) from table_nc1 where ltrim(nc) like '"
                                        + text_for_filter_cherch + "%'");
                IsDropDownCherch = true;
                current_length_of_cherch_filter = text_for_filter_cherch.Length;
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
                if (Cherch_list.Count == 0) is_drop_down_cherch = false;
                OnPropertyChanged("IsDropDownCherch");
            }
        }
        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
