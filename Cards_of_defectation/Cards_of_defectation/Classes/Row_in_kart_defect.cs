using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Cards_of_defectation.Classes
{
    public class Row_in_kart_defect
    {
        int met_opr, opis_def, prich, spos_ustr, teh_treb, id, nom_kart, par, nom_ceh, izgotov;
        string cherch, met_opr_komment, opis_def_komment, prich_komment, spos_ustr_komment, teh_treb_komment,
            data_def, data_post, n_nomer, prim, naim, nom_sz;
        float kolvo;
        
        public Row_in_kart_defect() { }
        public Row_in_kart_defect(Row_in_plan_rabot row)
        {
            nom_sz = row.Nom_sz;
            cherch = Server.InitServer().DataBase("uit")
                .ExecuteCommand("select cherch from rz_naim_cherch where naim = '"
                + Server.InitServer().DataBase("uit")
                .ExecuteCommand("select naim from rz_ser_nom_naim where ser_nom = '"
                + row.Ser_nom + "'")[0].ToString() + "'")[0].ToString();
        }
        public Row_in_kart_defect(string pnom_sz)
        {
            nom_sz = pnom_sz;
        }

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }
        public int Par
        {
            get
            {
                return par;
            }
            set
            {
                par = value;
            }
        }
        public string Nom_sz
        {
            get
            {
                return nom_sz;
            }

            set
            {
                nom_sz = value;
            }
        }
        public string Cherch
        {
            get
            {
                return cherch;
            }
            set
            {
                cherch = value;
            }
        }
        public string N_nomer
        {
            get
            {
                return n_nomer;
            }
            set
            {
                n_nomer = value;
            }
        }
        public string Naim
        {
            get
            {
                return naim;
            }
            set
            {
                naim = value;
            }
        }
        public float Kolvo
        {
            get
            {
                return kolvo;
            }
            set
            {
                kolvo = value;
            }
        }
        public int Nom_ceh
        {
            get
            {
                return nom_ceh;
            }

            set
            {
                nom_ceh = value;
            }
        }
        public int Nom_kart
        {
            get
            {
                return nom_kart;
            }
            set
            {
                nom_kart = value;
            }
        }
        public int Opis_def
        {
            get
            {
                if (opis_def == 0) opis_def = 1;
                return opis_def;
            }

            set
            {
                opis_def = value;           
            }
        }
        public string Opis_def_komment
        {
            get
            {
                return opis_def_komment;
            }

            set
            {
                opis_def_komment = value;
            }
        }
        public int Prich
        {
            get
            {
                if (prich == 0) prich = 1;
                return prich;
            }

            set
            {
                prich = value;
            }
        }
        public string Prich_komment
        {
            get
            {
                return prich_komment;
            }

            set
            {
                prich_komment = value;
            }
        }
        public int Met_opr
        {
            get
            {
                if (met_opr == 0) met_opr = 1;
                return met_opr;
            }

            set
            {
                met_opr = value;
            }
        }
        public string Met_opr_komment
        {
            get
            {
                return met_opr_komment;
            }

            set
            {
                met_opr_komment = value;
            }
        }
        public int Teh_treb
        {
            get
            {
                if (teh_treb == 0) teh_treb = 1;
                return teh_treb;
            }

            set
            {
                teh_treb = value;
            }
        }
        public string Teh_treb_komment
        {
            get
            {
                return teh_treb_komment;
            }

            set
            {
                teh_treb_komment = value;
            }
        }
        public int Spos_ustr
        {
            get
            {
                if (spos_ustr == 0) spos_ustr = 1;
                return spos_ustr;
            }

            set
            {
                spos_ustr = value;
            }
        }
        public string Spos_ustr_komment
        {
            get
            {
                return spos_ustr_komment;
            }

            set
            {
                spos_ustr_komment = value;
            }
        }      
        public string Data_post
        {
            get
            {
                return data_post;
            }

            set
            {
                data_post = value;
            }
        }
        public string Data_def
        {
            get
            {
                return data_def;
            }

            set
            {
                data_def = value;
            }
        }
        public int Izgotov
        {
            get
            {
                return izgotov;
            }
            set
            {
                izgotov = value;
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
    }
}
