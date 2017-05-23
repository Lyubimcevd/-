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

        public RowPlanViewModal(string pnom_sz)
        {
            row = new Row_in_plan_rabot();
            row.Nom_sz = pnom_sz;
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
            set
            {
                row.Nom_sz = value;
                is_change = true;
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
                return "";
            }
        }
        public int Prior
        {
            get
            {
                return 0;
            }
            set
            {

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
