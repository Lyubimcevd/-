using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Cards_of_defectation.Classes;

namespace Cards_of_defectation.ОТГО.ViewModal
{
    class CreateWindowNaimViewModal: INotifyPropertyChanged
    {
        string text_for_filter_naim,select_item;
        List<object> naim_list;
        int current_length_of_naim_filter;
        bool is_drop_down_naim;

        public event PropertyChangedEventHandler PropertyChanged;

        public CreateWindowNaimViewModal(List<object> tmp)
        {
            if (tmp.Count != 0) text_for_filter_naim = tmp[0].ToString();
        }
        public string SelectItem
        {
            get
            {
                return select_item;
            }
            set
            {
                select_item = value;
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
        public string Text_for_filter_naim
        {
            get
            {
                return text_for_filter_naim;
            }
            set
            {              
                text_for_filter_naim = value;
                if (Naim_list?.Count != 0 || current_length_of_naim_filter > text_for_filter_naim.Length || Naim_list == null)
                    Naim_list = Server.InitServer().DataBase("cvodka")
                        .ExecuteCommand("select distinct top 50 Ltrim(rtrim(naim)) from naim where naim like '%"
                                        + text_for_filter_naim + "%'");
                IsDropDownNaim = true;
                current_length_of_naim_filter = text_for_filter_naim.Length;
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
                if (Naim_list.Count == 0) is_drop_down_naim = false;
                OnPropertyChanged("IsDropDownNaim");
            }
        }
        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
