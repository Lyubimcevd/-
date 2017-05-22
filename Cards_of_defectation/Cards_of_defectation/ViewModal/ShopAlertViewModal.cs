using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Cards_of_defectation.Classes
{
    class ShopAlertViewModal : INotifyPropertyChanged
    {
        Row_in_kart_defect parent_row;
        string color;

        public event PropertyChangedEventHandler PropertyChanged;

        public ShopAlertViewModal(Row_in_kart_defect row)
        {
            parent_row = row;
            color = "White";
        }
        public string Nom_zay
        {
            get
            {
                return parent_row.Nom_zay;
            }
        }
        public string Obozn_det
        {
            get
            {
                return parent_row.Obozn_det;
            }
        }
        public string Data_post
        {
            get
            {
                return parent_row.Data_post;
            }
        }
        public string Data_def
        {
            get
            {
                return parent_row.Data_def;
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
        public int Id
        {
            get
            {
                return parent_row.Id;
            }
        }
        public int Prior
        {
            get
            {
                List<object> tmp = Server.InitServer().DataBase("test1")
                    .ExecuteCommand("select [Приоритет] from rz_prior where [РЗ] = " + Nom_zak);
                if (tmp.Count != 0) return Convert.ToInt32(tmp[0]);
                else return 0;
            }
        }
        public int Nom_zak
        {
            get
            {
                return Convert.ToInt32(Server.InitServer().DataBase("test1")
                    .ExecuteCommand("select nom_zak from plan_rabot where nom_zay =" + Nom_zay)[0]);
            }
        }

        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
