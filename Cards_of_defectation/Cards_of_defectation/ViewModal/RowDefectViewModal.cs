using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Cards_of_defectation.Classes;

namespace Cards_of_defectation.ViewModal
{
    public class RowDefectViewModal : INotifyPropertyChanged
    {
        RowDefectViewModal parent;
        Row_in_kart_defect row;
        List<string> ceh_list;
        string cherch_for_search;
        bool is_change;

        public event PropertyChangedEventHandler PropertyChanged;

        public RowDefectViewModal(string pCherch, RowDefectViewModal prow)
        {
            row = new Row_in_kart_defect();
            row.Nom_sz = prow.Nom_sz;
            row.Par = prow.Id;
            row.Cherch = pCherch;
            parent = prow;
            is_change = false;
        }
        public RowDefectViewModal(Row_in_kart_defect prow)
        {
            row = prow;
            is_change = false;
        }

        public int Id
        {
            get
            {
                return row.Id;
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
        public string Nom_sz
        {
            get
            {
                return row.Nom_sz;
            }
        }
        public string Cherch
        {
            get
            {
                return row.Cherch;
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
                        + row.Cherch + "' and ltrim(rtrim(nk))='" + parent.Cherch + "'");
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
        public string Nom_kart
        {
            get
            {
                if (row.Nom_ceh != -1) return References.InitReferences().Cehs[row.Nom_ceh] + "/" + row.Nom_kart.ToString();
                else return row.Nom_kart.ToString();
            }
        }
        public string Opis_def
        {
            get
            {
                return Server.InitServer().DataBase("uit")
                    .ExecuteCommand("select opis_def from rz_opis_def where id = " + row.Opis_def)[0].ToString();
            }

            set
            {
                row.Opis_def = Convert.ToInt32(Server.InitServer().DataBase("uit")
                    .ExecuteCommand("select id from rz_opis_def where opis_def = '" + value+"'")[0]);
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
        public string Prichina
        {
            get
            {
                return Server.InitServer().DataBase("uit")
                    .ExecuteCommand("select prich from rz_prichina where id = " + row.Prich)[0].ToString();
            }

            set
            {
                row.Prich = Convert.ToInt32(Server.InitServer().DataBase("uit")
                    .ExecuteCommand("select id from rz_prichina where prich = '" + value+"'")[0]);
                is_change = true;
                OnPropertyChanged("Prichina");
            }
        }
        public string Prichina_komment
        {
            get
            {
                return row.Prich_komment;
            }

            set
            {
                row.Prich_komment = value;
                is_change = true;
            }
        }
        public string Met_opr
        {
            get
            {
                return Server.InitServer().DataBase("uit")
                    .ExecuteCommand("select met_opr from rz_met_opred where id = " + row.Met_opr)[0].ToString();           
            }

            set
            {
                row.Met_opr = Convert.ToInt32(Server.InitServer().DataBase("uit")
                    .ExecuteCommand("select id from rz_met_opred where met_opr = '" + value+"'")[0]);
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
        public string Teh_treb
        {
            get
            {
                return Server.InitServer().DataBase("uit")
                    .ExecuteCommand("select teh_treb from rz_teh_treb where id = " + row.Teh_treb)[0].ToString();
            }

            set
            {
                row.Teh_treb = Convert.ToInt32(Server.InitServer().DataBase("uit")
                    .ExecuteCommand("select id from rz_teh_treb where teh_treb = '" + value+"'")[0]);
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
        public string Spos_ustr
        {
            get
            {
                return Server.InitServer().DataBase("uit")
                    .ExecuteCommand("select spos_ustr from rz_spos_ustr where id = " + row.Spos_ustr)[0].ToString();
            }

            set
            {
                row.Spos_ustr = Convert.ToInt32(Server.InitServer().DataBase("uit")
                    .ExecuteCommand("select id from rz_spos_ustr where spos_ustr = '" + value+"'")[0]);
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
                    .ExecuteCommand("select naim from naim where nom = '" + Cherch_for_search + "'");
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
                else return Convert.ToInt32(Server.InitServer().DataBase("uit")
                    .ExecuteCommand("select nom_zak from rz_plan_rabot where Nom_sz =" + Nom_sz)[0]);
            }
        }
        public int Prior
        {
            get
            {
                if (parent != null) return parent.Prior;
                else
                {
                    List<object> tmp = Server.InitServer().DataBase("uit")
                    .ExecuteCommand("select prior from rz_nom_zak_prior where nom_zak = " + Nom_zak);
                    if (tmp.Count != 0) return Convert.ToInt32(tmp[0]);
                    else return 0;
                } 
               
            }
        }
        public string Ser_nom
        {
            get
            {
                string ser_nom_izd;
                if (parent != null) ser_nom_izd = parent.Ser_nom;
                else ser_nom_izd = Server.InitServer().DataBase("uit")
                        .ExecuteCommand("select ser_nom from rz_plan_rabot where nom_sz =" + Nom_sz)[0].ToString();
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
                                       + Cherch_for_search + "'");
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
                if (row.Nom_ceh == 0)
                    if (ceh_list != References.InitReferences().Cehs)
                    {
                        row.Nom_ceh = Convert.ToInt32(Server.InitServer().DataBase("cvodka")
                            .ExecuteCommand("select id from podr1 where nc = " + ceh_list.Last())[0]);
                        return ceh_list.Last();
                    }
                    else return "";
                else return References.InitReferences().Cehs[Nom_ceh];
            }
            set
            {
                Nom_ceh = Convert.ToInt32(Server.InitServer().DataBase("cvodka")
                    .ExecuteCommand("select id from podr1 where nc = " + value)[0]);
                is_change = true;
            }
        }
        public string Cherch_for_search
        {
            get
            {
                if (Cherch != null)
                {
                    if (cherch_for_search == null)
                    {
                        int i = 0;
                        while (Char.IsLetter(Cherch, i) && (Cherch.Length > i)) i++;
                        if (i != 0) cherch_for_search = Cherch.PadLeft(Cherch.Length + 4 - i);
                        else cherch_for_search = Cherch;
                    }
                }
                else cherch_for_search = "";
                return cherch_for_search;
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
