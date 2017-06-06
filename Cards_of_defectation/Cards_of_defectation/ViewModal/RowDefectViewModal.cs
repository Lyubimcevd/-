using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Cards_of_defectation.Classes;
using System.Windows;

namespace Cards_of_defectation.ViewModal
{
    public class RowDefectViewModal : INotifyPropertyChanged
    {
        RowDefectViewModal parent;
        Row_in_kart_defect row;
        string cherch_for_search;
        bool is_change = false;

        public event PropertyChangedEventHandler PropertyChanged;

        public RowDefectViewModal(string pCherch, RowDefectViewModal prow)
        {
            row = new Row_in_kart_defect();
            row.Nom_sz = prow.Nom_sz;
            row.Par = prow.Id;
            row.Cherch = pCherch;
            parent = prow;
        }
        public RowDefectViewModal(Row_in_kart_defect prow)
        {
            row = prow;
            if (row.Par != 0) parent = Converter.ToViewModal(Server.GetServer.DataBase("uit")
                .Table("select * from rz_kart_defect where id = " + row.Par)
                .LoadFromServer() as List<Row_in_kart_defect>)[0];
        }
        public RowDefectViewModal(RowDefectViewModal prow)
        {
            row = new Row_in_kart_defect(prow.Save);
            parent = prow.GetParent();
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
                        List<object> tmp = Server.GetServer.DataBase("uit")
                        .ExecuteCommand("select ks from spkd1 where ltrim(rtrim(nc))='"
                        + row.Cherch + "' and ltrim(rtrim(nk))='" + parent.Cherch + "'");
                        if (tmp.Count != 0) row.Kolvo = Convert.ToInt32(tmp[0]);
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
                if (IsKolvoCorrect(value))
                {
                    row.Kolvo = value;
                    is_change = true;
                }
                else
                {
                    row.Kolvo = 0;
                    MessageBox.Show("Введённое количество больше количества по применимости", "Ошибка");
                }
                OnPropertyChanged("Kolvo");
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
                if (Spos_ustr == "Дефектация"&&Nom_ceh!=0)
                    return Ceh + "/" + row.Nom_kart.ToString();
                else return null;
            }
        }
        public string Opis_def
        {
            get
            {
                return References.GetReferences.GetNaim("rz_opis_def", row.Opis_def);
            }

            set
            {
                row.Opis_def = References.GetReferences.GetId("rz_opis_def", value);
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
                return References.GetReferences.GetNaim("rz_prichina", row.Prich);
            }

            set
            {
                row.Prich = References.GetReferences.GetId("rz_prichina", value);
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
                return References.GetReferences.GetNaim("rz_met_opr", row.Met_opr);      
            }

            set
            {
                row.Met_opr = References.GetReferences.GetId("rz_met_opr", value);
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
                return References.GetReferences.GetNaim("rz_teh_treb", row.Teh_treb);
            }

            set
            {
                row.Teh_treb = References.GetReferences.GetId("rz_teh_treb", value);
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
                return References.GetReferences.GetNaim("rz_spos_ustr",row.Spos_ustr);
            }

            set
            {
                row.Spos_ustr = References.GetReferences.GetId("rz_spos_ustr", value);
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
                    List<object> tmp = Server.GetServer.DataBase("cvodka")
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
                else return Convert.ToInt32(Server.GetServer.DataBase("uit")
                    .ExecuteCommand("select nom_zak from rz_plan_rabot where Nom_sz = '" + Nom_sz+"'")[0]);
            }
        }
        public string Prior
        {
            get
            {                            
                List<object> tmp = Server.GetServer.DataBase("cvodka")
                    .ExecuteCommand("select pr from nazpr where zakspis = " + Nom_zak);
                if (tmp.Count != 0) return tmp[0].ToString();
                else return null;              
            }
        }
        public string Ser_nom
        {
            get
            {
                string ser_nom_izd;
                if (parent != null) ser_nom_izd = parent.Ser_nom;
                else ser_nom_izd = Server.GetServer.DataBase("uit")
                        .ExecuteCommand("select ser_nom from rz_plan_rabot where nom_sz = '" + Nom_sz+"'")[0].ToString();
                return ser_nom_izd;
            }
        }
        public string Cehs_path
        {
            get
            {  
                string ceh_path = "";
                List<object> tmp = Server.GetServer.DataBase("cvodka")
                    .ExecuteCommand("select distinct ci from cvodka.dbo.stabil where nc ='" + Cherch_for_search + "'");
                if (Server.GetServer.DataBase("cvodka")
                    .ExecuteCommand("select distinct ci from cvodka.dbo.stabil where nc ='" + Cherch_for_search + "'")
                    .Count != 0)
                {
                    foreach (string ceh in Ceh_list) ceh_path += ceh + " -> ";
                    ceh_path = "( " + ceh_path.Substring(0, ceh_path.Length - 4) + " )  ";
                }
                return ceh_path;
            }
        }
        public List<string> Opis_def_list
        {
            get
            {
                return References.GetReferences.ReferenceByName("rz_opis_def");
            }
        }
        public List<string> Prichina_list
        {
            get
            {
                return References.GetReferences.ReferenceByName("rz_prichina");
            }
        }
        public List<string> Met_opr_list
        {
            get
            {
                return References.GetReferences.ReferenceByName("rz_met_opr");
            }
        }
        public List<string> Teh_treb_list
        {
            get
            {
                return References.GetReferences.ReferenceByName("rz_teh_treb");
            }
        }
        public List<string> Spos_ustr_list
        {
            get
            {
                return References.GetReferences.ReferenceByName("rz_spos_ustr");
            }
        }
        public List<string> Ceh_list
        {
            get
            {
                List<string> ceh_list = new List<string>();
                List<object> tmp = Server.GetServer.DataBase("cvodka")
                .ExecuteCommand("select cex from iz_ci_v_cex where ci in "
                +"(select distinct ci from stabil where nc ='"+ Cherch_for_search + "')");
                if (tmp.Count == 0)
                    tmp = Server.GetServer.DataBase("cvodka").ExecuteCommand("select nc from podr1");
                foreach (string ceh in tmp) ceh_list.Add(ceh);
                if (ceh_list.Last() == "005") ceh_list.RemoveAt(ceh_list.Count - 1);
                return ceh_list;
            }
        }
        public string Ceh
        {
            get
            {
                if (row.Nom_ceh == 0) row.Nom_ceh = References.GetReferences.GetIdCeh(Ceh_list.Last());
                return References.GetReferences.GetNaimCeh(row.Nom_ceh);
            }
            set
            {
                Nom_ceh = References.GetReferences.GetIdCeh(value);
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
        public RowDefectViewModal GetParent()
        {          
            return parent;
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

        bool IsKolvoCorrect(float kolvo)
        {
            if (parent != null)
            {
                List<object> tmp = Server.GetServer.DataBase("uit")
                .ExecuteCommand("select ks from spkd1 where ltrim(rtrim(nc))='"
                + row.Cherch + "' and ltrim(rtrim(nk))='" + parent.Cherch + "'");
                if (tmp.Count != 0)
                {
                    if (kolvo > parent.Kolvo * Convert.ToInt32(tmp[0])) return false;
                    else return true;
                }
                else return true;
            }
            else return true;
        }          
    }
}
