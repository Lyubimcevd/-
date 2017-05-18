using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Cards_of_defectation.Classes
{
    public class Row_in_kart_defect
    {
        int met_opr, opis_def, prichina, spos_ustr, teh_treb, id, nom_kart,
            nom_zay, par, nom_ceh;
        string obozn_det, met_opr_komment, opis_def_komment, prichina_komment, spos_ustr_komment, teh_treb_komment,
            data_def, data_post, zavod_izgot,n_nomer,prim,naim;
        float kolvo;
  
        public Row_in_kart_defect()
        {
            nom_ceh = -1;
        }
        public Row_in_kart_defect(Row_in_plan_rabot row)
        {
            nom_zay = row.Nom_zay;
            obozn_det = Server.InitServer().DataBase("test1")
                .ExecuteCommand("select Чертёж from type_cherch where [Заводской номер изделия] = '"
                + Server.InitServer().DataBase("test1")
                .ExecuteCommand("select [Заводской номер изделия] from nom_type where [Номер изделия] = '"
                + row.Ser_nom_izd + "'")[0].ToString() + "'")[0].ToString();              
            nom_ceh = -1;
        }
        
        public int Nom_zay
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
        public string Obozn_det
        {
            get
            {   
                return obozn_det;
            }
            set
            {
                obozn_det = value;
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
        public int Prichina
        {
            get
            {
                return prichina;
            }

            set
            {
                prichina = value;
            }
        }
        public string Prichina_komment
        {
            get
            {
                return prichina_komment;
            }

            set
            {
                prichina_komment = value;
            }
        }
        public int Met_opr
        {
            get
            {
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
        public string Zavod_izgot
        {
            get
            {
                return zavod_izgot;
            }
            set
            {
                zavod_izgot = value;
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
    }
}
