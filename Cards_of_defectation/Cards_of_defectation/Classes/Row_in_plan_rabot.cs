using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cards_of_defectation.Classes
{
    public class Row_in_plan_rabot
    {
        string ser_nom_izd, voin_chast, data_uved, nom_uved, srok_def, srok_def_komment, srok_dost,
            srok_dost_komment, srok_def_predpr, srok_def_predpr_komment, srok_rem, srok_rem_komment,
            srok_rem_soisp, soisp, srok_vosstan, srok_vosstan_komment, prim, last_prim,kontract,nom_zay;
        int nom_zak, type_rem;

        public Row_in_plan_rabot() { }

        public string Nom_zay
        {
            get
            {
                return nom_zay;
            }
            set
            {
                nom_zay = value;
            }
        }
        public string Ser_nom_izd
        {
            get
            {
                return ser_nom_izd;
            }
            set
            {
                ser_nom_izd = value;
            }
        }
        public string Voin_chast
        {
            get
            {
                return voin_chast;
            }
            set
            {
                voin_chast = value;
            }
        }
        public string Data_uved
        {
            get
            {
                return data_uved;
            }
            set
            {
                data_uved = value;
            }
        }
        public string Nom_uved
        {
            get
            {
                return nom_uved;
            }
            set
            {
                nom_uved = value;
            }
        }
        public string Srok_def
        {
            get
            {
                return srok_def;
            }
            set
            {
                srok_def = value;
            }
        }
        public string Srok_def_komment
        {
            get
            {
                return srok_def_komment;
            }
            set
            {
                srok_def_komment = value;
            }
        }
        public string Srok_dost
        {
            get
            {
                return srok_dost;
            }
            set
            {
                srok_dost = value;
            }
        }
        public string Srok_dost_komment
        {
            get
            {
                return srok_dost_komment;
            }
            set
            {
                srok_dost_komment = value;
            }
        }
        public string Srok_def_predpr
        {
            get
            {
                return srok_def_predpr;
            }
            set
            {
                srok_def_predpr = value;
            }
        }
        public string Srok_def_predpr_komment
        {
            get
            {
                return srok_def_predpr_komment;
            }
            set
            {
                srok_def_predpr_komment = value;
            }
        }
        public string Srok_rem
        {
            get
            {
                return srok_rem;
            }
            set
            {
                srok_rem = value;
            }
        }
        public string Srok_rem_komment
        {
            get
            {
                return srok_rem_komment;
            }
            set
            {
                srok_rem_komment = value;
            }
        }
        public string Srok_rem_soisp
        {
            get
            {
                return srok_rem_soisp;
            }
            set
            {
                srok_rem_soisp = value;
            }
        }
        public string Soisp
        {
            get
            {
                return soisp;
            }
            set
            {
                soisp = value;
            }
        }
        public string Srok_vosstan
        {
            get
            {
                return srok_vosstan;
            }
            set
            {
                srok_vosstan = value;
            }
        }
        public string Srok_vosstan_komment
        {
            get
            {
                return srok_vosstan_komment;
            }
            set
            {
                srok_vosstan_komment = value;
            }
        }
        public string Prim
        {
            get
            {
                return prim;
            }
            set
            {
                prim = value;
            }
        }
        public int Nom_zak
        {
            get
            {
                return nom_zak;
            }
            set
            {
                nom_zak = value;
            }
        }
        public int Type_rem
        {
            get
            {
                return type_rem;
            }
            set
            {
                type_rem = value;
            }
        }
        public string Last_prim
        {
            get
            {
                return last_prim;
            }
            set
            {
                last_prim = value;
            }
        }
        public string Kontract
        {
            get
            {
                return kontract;
            }
            set
            {
                kontract = value;
            }
        }
    }
}
