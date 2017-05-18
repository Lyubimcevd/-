using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Cards_of_defectation.Classes
{
    public class RowDefectViewModal : INotifyPropertyChanged
    {
        RowDefectViewModal parent;
        Row_in_kart_defect row;
        List<string> ceh_list;
        string obozn_det_for_search;
        bool is_change;

        public event PropertyChangedEventHandler PropertyChanged;

        public RowDefectViewModal(string pobozn_det, RowDefectViewModal prow)
        {
            row = new Row_in_kart_defect();
            row.Nom_zay = prow.Nom_zay;
            row.Par = prow.Id;
            row.Obozn_det = pobozn_det;
            parent = prow;
            is_change = false;
        }
        public RowDefectViewModal(Row_in_kart_defect prow)
        {
            row = prow;
            is_change = false;
        }

        public int Nom_zay
        {
            get
            {
                return row.Nom_zay;
            }
        }
        public string Obozn_det
        {
            get
            {
                return row.Obozn_det;
            }
        }
        public float Kolvo
        {
            get
            {
                if (row.Kolvo == 0)
                {
                    if (parent != null)
                    {
                        List<object> tmp = Server.InitServer().DataBase("uit")
                        .ExecuteCommand("select ks from spkd1 where ltrim(rtrim(nc))='"
                        + row.Obozn_det + "' and ltrim(rtrim(nk))='" + parent.Obozn_det + "'");
                        if (tmp.Count!=0) row.Kolvo = Convert.ToInt32(tmp[0]);
                        return parent.Kolvo * row.Kolvo;
                    }
                    else
                    {
                        row.Kolvo = 1;
                        return row.Kolvo;
                    }
                }
                else return row.Kolvo;
            }
            set
            {
                row.Kolvo = value;
                is_change = true;
            }
        }
        public string Nom_kart
        {
            get
            {
                if (row.Nom_ceh != -1) return References.InitReferences().Cehs[row.Nom_ceh] + "/" + row.Nom_kart.ToString();
                else return row.Nom_kart.ToString();
            }
        }
        public int Opis_def
        {
            get
            {
                return row.Opis_def;
            }

            set
            {
                row.Opis_def = value;
                is_change = true;
                OnPropertyChanged("Opis_def");
            }
        }
        public string Opis_def_komment
        {
            get
            {
                return row.Opis_def_komment;
            }

            set
            {
                row.Opis_def_komment = value;
                is_change = true;
            }
        }
        public int Prichina
        {
            get
            {
                return row.Prichina;
            }

            set
            {
                row.Prichina = value;
                is_change = true;
                OnPropertyChanged("Prichina");
            }
        }
        public string Prichina_komment
        {
            get
            {
                return row.Prichina_komment;
            }

            set
            {
                row.Prichina_komment = value;
                is_change = true;
            }
        }
        public int Met_opr
        {
            get
            {
                return row.Met_opr;
            }

            set
            {
                row.Met_opr = value;
                is_change = true;
                OnPropertyChanged("Met_opr");
            }
        }
        public string Met_opr_komment
        {
            get
            {
                return row.Met_opr_komment;
            }

            set
            {
                row.Met_opr_komment = value;
                is_change = true;
            }
        }
        public int Teh_treb
        {
            get
            {
                return row.Teh_treb;
            }

            set
            {
                row.Teh_treb = value;
                is_change = true;
                OnPropertyChanged("Teh_treb");
            }
        }
        public string Teh_treb_komment
        {
            get
            {
                return row.Teh_treb_komment;
            }

            set
            {
                row.Teh_treb_komment = value;
                is_change = true;
            }
        }
        public int Spos_ustr
        {
            get
            {
                return row.Spos_ustr;
            }

            set
            {
                row.Spos_ustr = value;
                is_change = true;
                OnPropertyChanged("Spos_ustr");
            }
        }
        public string Spos_ustr_komment
        {
            get
            {
                return row.Spos_ustr_komment;
            }

            set
            {
                row.Spos_ustr_komment = value;
                is_change = true;
            }
        }
        public int Nom_ceh
        {
            get
            {
                return row.Nom_ceh;
            }

            set
            {
                row.Nom_ceh = value;
                is_change = true;
                OnPropertyChanged("Nom_ceh");
            }
        }
        public int Par
        {
            get
            {
                return row.Par;
            }
            set
            {
                row.Par = value;
                is_change = true;
            }
        }
        public int Id
        {
            get
            {
                return row.Id;
            }
        }
        public string Data_post
        {
            get
            {
                return row.Data_post;
            }

            set
            {
                row.Data_post = value;
            }
        }
        public string Data_def
        {
            get
            {
                return row.Data_def;
            }

            set
            {
                row.Data_def = value;
            }
        }

        public string Naim_det
        {
            get
            {
                if (row.Naim == null)
                {
                    List<object> tmp = Server.InitServer().DataBase("cvodka")
                    .ExecuteCommand("select naim from naim where nom = '" + Obozn_det_for_search + "'");
                    if (tmp.Count != 0) row.Naim = tmp[0].ToString().TrimEnd();
                }
                return row.Naim;
            }
        }
        public int Nom_zak
        {
            get
            {              
                if (parent != null) return parent.Nom_zak;
                else return Convert.ToInt32(Server.InitServer().DataBase("test1")
                    .ExecuteCommand("select nom_zak from plan_rabot where nom_zay =" + Nom_zay)[0]);
            }
        }
        public int Prior
        {
            get
            {
                if (parent != null) return parent.Prior;
                else
                {
                    List<object> tmp = Server.InitServer().DataBase("test1")
                    .ExecuteCommand("select [Приоритет] from rz_prior where [РЗ] = " + Nom_zak);
                    if (tmp.Count != 0) return Convert.ToInt32(tmp[0]);
                    else return 0;
                } 
               
            }
        }
        public string Ser_nom_izd
        {
            get
            {
                string ser_nom_izd;
                if (parent != null) ser_nom_izd = parent.Ser_nom_izd;
                else ser_nom_izd = Server.InitServer().DataBase("test1")
                        .ExecuteCommand("select ser_nom_izd from plan_rabot where nom_zay =" + Nom_zay)[0].ToString();
                return ser_nom_izd;
            }
        }
        public string Cehs_path
        {
            get
            {  
                string ceh_path = "";
                if (Ceh_list != References.InitReferences().Cehs)
                {
                    foreach (string ceh in ceh_list) ceh_path += ceh + " -> ";
                    ceh_path = "( " + ceh_path.Substring(0, ceh_path.Length - 4) + " )  ";
                }
                return ceh_path;
            }
        }
        public List<string> Opis_def_list
        {
            get
            {
                return References.InitReferences().Defects;
            }
        }
        public List<string> Prichina_list
        {
            get
            {
                return References.InitReferences().Causes_of_defect;
            }
        }
        public List<string> Met_opr_list
        {
            get
            {
                return References.InitReferences().Determination_method_of_defect;
            }
        }
        public List<string> Teh_treb_list
        {
            get
            {
                return References.InitReferences().Technical_requirements;
            }
        }
        public List<string> Spos_ustr_list
        {
            get
            {
                return References.InitReferences().Elimination_method;
            }
        }
        public List<string> Ceh_list
        {
            get
            {
                if (ceh_list == null)
                {
                    List<object> tmp = Server.InitServer().DataBase("cvodka")
                                       .ExecuteCommand("select distinct ci from stabil where nc = '"
                                       + Obozn_det_for_search + "'");
                    if (tmp.Count != 0)
                    {
                        ceh_list = new List<string>();
                        foreach (string ceh in tmp) ceh_list.Add(ceh);
                        if (ceh_list.Last() == "005") ceh_list.RemoveAt(ceh_list.Count - 1);
                    }
                    else ceh_list = References.InitReferences().Cehs;
                }
                return ceh_list;
            }
        }
        public string Ceh
        {
            get
            {
                if (row.Nom_ceh == -1)
                    if (ceh_list != References.InitReferences().Cehs)
                    {
                        row.Nom_ceh = References.InitReferences().Cehs.IndexOf(ceh_list.Last());
                        return ceh_list.Last();
                    }
                    else return "";
                else return References.InitReferences().Cehs[Nom_ceh];
            }
            set
            {
                Nom_ceh = References.InitReferences().Cehs.IndexOf(value);
                is_change = true;
            }
        }
        public string Obozn_det_for_search
        {
            get
            {
                if (Obozn_det != null)
                {
                    if (obozn_det_for_search == null)
                    {
                        int i = 0;
                        while (Char.IsLetter(Obozn_det, i) && (Obozn_det.Length > i)) i++;
                        if (i != 0) obozn_det_for_search = Obozn_det.PadLeft(Obozn_det.Length + 4 - i);
                        else obozn_det_for_search = Obozn_det;
                    }
                }
                else obozn_det_for_search = "";
                return obozn_det_for_search;
            }
        }
        public bool IsChange
        {
            get
            {
                return is_change;
            }
        }
        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        public Row_in_kart_defect Save
        {
            get
            {
                is_change = false;
                return row;
            }
        }
    }
}
