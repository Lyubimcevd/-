using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Cards_of_defectation.Classes;

namespace Cards_of_defectation.ОТГО.ViewModal
{
    class SearchWindowBySerNomViewModal: INotifyPropertyChanged
    {
        string text_for_filter_ser_nom;
        List<object> ser_nom_list;
        int current_length_of_ser_nom_filter;
        bool is_drop_down_ser_nom;

        public event PropertyChangedEventHandler PropertyChanged;

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
        public string Text_for_filter_ser_nom
        {
            get
            {
                return text_for_filter_ser_nom;
            }
            set
            {
                text_for_filter_ser_nom = value;
                if (Ser_nom_list?.Count != 0 || current_length_of_ser_nom_filter > text_for_filter_ser_nom.Length || Ser_nom_list == null)
                    Ser_nom_list = Server.GetServer.DataBase("uit")
                            .ExecuteCommand("select top 50 ser_nom from rz_ser_nom_naim where ser_nom like '"
                                            + text_for_filter_ser_nom + "%'");  
                IsDropDownSerNom = true;
                current_length_of_ser_nom_filter = text_for_filter_ser_nom.Length;
            }
        }
        public bool IsDropDownSerNom
        {
            get
            {
                return is_drop_down_ser_nom;
            }
            set
            {
                is_drop_down_ser_nom = value;
                if (Ser_nom_list.Count == 0) is_drop_down_ser_nom = false;
                OnPropertyChanged("IsDropDownSerNom");
            }
        }
        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
