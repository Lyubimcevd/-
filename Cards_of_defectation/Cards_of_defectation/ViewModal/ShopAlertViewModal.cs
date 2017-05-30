using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Cards_of_defectation.Classes;

namespace Cards_of_defectation.ViewModal
{
    class ShopAlertViewModal : INotifyPropertyChanged
    {
        Row_in_kart_defect parent_row;
        string color;

        public event PropertyChangedEventHandler PropertyChanged;

        public ShopAlertViewModal(Row_in_kart_defect row)
        {
            parent_row = row;
            color = "white";
        }
        public string Nom_sz
        {
            get
            {
                return parent_row.Nom_sz;
            }
        }
        public string Cherch
        {
            get
            {
                return parent_row.Cherch;
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
                if (IsFaster) return "Red";
                else return color;
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
        public string Prior
        {
            get
            {
                List<object> tmp = Server.InitServer().DataBase("cvodka")
                        .ExecuteCommand("select pr from nazpr where zakspis = '" + Nom_zak +"'");
                if (tmp.Count != 0) return tmp[0].ToString();
                else return null;
            }
        }
        public int Nom_zak
        {
            get
            {
                return Convert.ToInt32(Server.InitServer().DataBase("uit")
                    .ExecuteCommand("select nom_zak from rz_plan_rabot where nom_sz ='" + Nom_sz +"'")[0]);
            }
        }
        public bool IsCeh
        {
            get
            {
                return Authorization.Get.IsCeh;
            }
        }
        public bool IsFaster
        {
            get
            {
               return parent_row.IsFaster;
            }
        }

        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
