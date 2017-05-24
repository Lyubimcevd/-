using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cards_of_defectation.Classes;

namespace Cards_of_defectation.ОУП.ViewModal
{
    public class RowPlanViewModal
    {
        Row_in_plan_rabot row;
        bool is_change = false;

        public RowPlanViewModal()
        {
            row = new Row_in_plan_rabot();
        }
        public RowPlanViewModal(Row_in_plan_rabot prow)
        {
            row = prow;
        }

        public string Nom_sz
        {
            get
            {
                return row.Nom_sz;
            }
        }
        public string Nom_sz_view
        {
            get
            {
                if (row.Nom_sz != null && row.Nom_sz == row.Ser_nom) return "";
                else return row.Nom_sz;
            }
        }
        public string Ser_nom
        {
            get
            {
                return row.Ser_nom;
            }
            set
            {
                row.Ser_nom = value;
                is_change = true;
            }
        }
        public string Voin_chast
        {
            get
            {
                return row.Voin_chast;
            }
            set
            {
                row.Voin_chast = value;
                is_change = true;
            }
        }
        public string Nom_zak
        {
            get
            {
                return row.Nom_zak;
            }
            set
            {
                row.Nom_zak = value;
                is_change = true;
            }
        }
        public string Srok_rem
        {
            get
            {
                return row.Srok_rem;
            }
            set
            {
                row.Srok_rem = value;
                is_change = true;
            }
        }    
        public string Prim
        {
            get
            {
                return row.Prim;
            }
            set
            {
                row.Prim = value;
                is_change = true;
            }
        }
        public string Kards
        {
            get
            {
                string result = "";
                if (Nom_sz != Ser_nom)
                {
                    Log.Init.Info("Формирование всего/выполнено в плане");
                    result = Server.InitServer().DataBase("uit")
                    .ExecuteCommand("select count(*) from rz_kart_defect where nom_sz = "
                    + Nom_sz + " group by nom_sz")[0].ToString() + " / ";
                    List<object> tmp = Server.InitServer().DataBase("uit")
                        .ExecuteCommand("select count(*) from rz_kart_defect where nom_sz = "
                        + Nom_sz + " and data_def is not null group by nom_sz");
                    if (tmp.Count == 0) result += "0";
                    else result += tmp[0].ToString();
                }
                Log.Init.Info("Формирование завершено. Результат "+result);
                return result;
            }
        }
        public string Prior
        {
            get
            {
                string result = null;
                if (row.Nom_zak != null)
                {
                    Log.Init.Info("Формирование приоритет в плане");
                    List<object> tmp = Server.InitServer().DataBase("cvodka")
                          .ExecuteCommand("select pr from nazpr where zakspis = " + row.Nom_zak);
                    if (tmp.Count != 0) result = tmp[0].ToString();
                }
                Log.Init.Info("Формирование завершено. Результат "+result);
                return result;
            }         
        }
        public bool IsChange
        {
            get
            {
                return is_change;
            }
        }
        public Row_in_plan_rabot Save
        {
            get
            {
                if (row.Nom_sz == null) row.Nom_sz = row.Ser_nom;
                is_change = false;
                return row;
            }
        }
    }
}
