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
        bool is_change;

        public RowPlanViewModal(string pnom_zay)
        {
            row = new Row_in_plan_rabot();
            row.Nom_zay = pnom_zay;
            is_change = false;
        }
        public RowPlanViewModal(Row_in_plan_rabot prow)
        {
            row = prow;
            is_change = false;
        }

        public string Nom_zay
        {
            get
            {
                return row.Nom_zay;
            }
            set
            {
                row.Nom_zay = value;
                is_change = true;
            }
        }
        public string Ser_nom_izd
        {
            get
            {
                return row.Ser_nom_izd;
            }
            set
            {
                row.Ser_nom_izd = value;
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
        public string Data_uved
        {
            get
            {
                return row.Data_uved;
            }
            set
            {
                row.Data_uved = value;
                is_change = true;
            }
        }
        public string Nom_uved
        {
            get
            {
                return row.Nom_uved;
            }
            set
            {
                row.Nom_uved = value;
                is_change = true;
            }
        }
        public string Srok_def
        {
            get
            {
                return row.Srok_def;
            }
            set
            {
                row.Srok_def = value;
                is_change = true;
            }
        }
        public string Srok_def_komment
        {
            get
            {
                return row.Srok_def_komment;
            }
            set
            {
                row.Srok_def_komment = value;
                is_change = true;
            }
        }
        public string Srok_dost
        {
            get
            {
                return row.Srok_dost;
            }
            set
            {
                row.Srok_dost = value;
                is_change = true;
            }
        }
        public string Srok_dost_komment
        {
            get
            {
                return row.Srok_dost_komment;
            }
            set
            {
                row.Srok_dost_komment = value;
                is_change = true;
            }
        }
        public string Srok_def_predpr
        {
            get
            {
                return row.Srok_def_predpr;
            }
            set
            {
                row.Srok_def_predpr = value;
                is_change = true;
            }
        }
        public string Srok_def_predpr_komment
        {
            get
            {
                return row.Srok_def_predpr_komment;
            }
            set
            {
                row.Srok_def_predpr_komment = value;
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
        public string Srok_rem_komment
        {
            get
            {
                return row.Srok_rem_komment;
            }
            set
            {
                row.Srok_rem_komment = value;
                is_change = true;
            }
        }
        public string Srok_rem_soisp
        {
            get
            {
                return row.Srok_rem_soisp;
            }
            set
            {
                row.Srok_rem_soisp = value;
                is_change = true;
            }
        }
        public string Soisp
        {
            get
            {
                return row.Soisp;
            }
            set
            {
                row.Soisp = value;
                is_change = true;
            }
        }
        public string Srok_vosstan
        {
            get
            {
                return row.Srok_vosstan;
            }
            set
            {
                row.Srok_vosstan = value;
                is_change = true;
            }
        }
        public string Srok_vosstan_komment
        {
            get
            {
                return row.Srok_vosstan_komment;
            }
            set
            {
                row.Srok_vosstan_komment = value;
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
        public int Nom_zak
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
        public int Type_rem
        {
            get
            {
                return row.Type_rem;
            }
            set
            {
                row.Type_rem = value;
                is_change = true;
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
                is_change = false;
                return row;
            }
        }
    }
}
