using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections.ObjectModel;
using Cards_of_defectation.ViewModal;
using Cards_of_defectation.ОУП.ViewModal;

namespace Cards_of_defectation.Classes
{
    class Converter
    {
        public static void ListToTable(List<Row_in_plan_rabot> Rows, DataTable DT)
        {
            foreach (DataRow row in DT.Rows)
                if (Rows.Find(x => x.Nom_sz == row["nom_sz"].ToString()) == null) row.Delete();
            foreach (Row_in_plan_rabot row in Rows)
            {
                DataRow[] tmp = DT.Select("nom_sz = '" + row.Nom_sz + "'");
                if (tmp.Length != 0) FillRowInTable(row, tmp.First());
                else
                {
                    DT.Rows.Add(DT.NewRow());
                    FillRowInTable(row, DT.Rows[DT.Rows.Count - 1]);
                }
            }
        }
        public static object TableToList(DataTable DT)
        {
            if (DT.TableName == "rz_plan_rabot")
            {
                List<Row_in_plan_rabot> Rows = new List<Row_in_plan_rabot>();
                foreach (DataRow DR in DT.Rows) Rows.Add(FillRowFromTable(DR, new Row_in_plan_rabot()));
                return Rows;
            }
            else
            {
                List<Row_in_kart_defect> Rows = new List<Row_in_kart_defect>();
                foreach (DataRow row in DT.Rows) Rows.Add(FillRowFromTable(row, new Row_in_kart_defect()));
                return Rows;
            }
        }
        static DataRow FillRowInTable(Row_in_plan_rabot row, DataRow DR)
        {
            DR["nom_sz"] = row.Nom_sz;
            if (row.Ser_nom!= null) DR["ser_nom"] = row.Ser_nom;
            if (row.Voin_chast != null) DR["voin_chast"] = row.Voin_chast;
            if (row.Nom_zak != null) DR["nom_zak"] = row.Nom_zak;
            if (row.Srok_rem != null) DR["srok_rem"] = row.Srok_rem;
            if (row.Nom_kont != null) DR["nom_kont"] = row.Nom_kont;
            if (row.Prim != null) DR["prim"] = row.Prim;          
            return DR;
        }
        static Row_in_plan_rabot FillRowFromTable(DataRow DR, Row_in_plan_rabot row)
        {
            row.Nom_sz = DR["nom_sz"].ToString();
            if (DR["ser_nom"] != DBNull.Value) row.Ser_nom = DR["ser_nom"].ToString();
            if (DR["voin_chast"] != DBNull.Value) row.Voin_chast = DR["voin_chast"].ToString();
            if (DR["nom_zak"] != DBNull.Value) row.Nom_zak = DR["nom_zak"].ToString();
            if (DR["srok_rem"] != DBNull.Value) row.Srok_rem = Convert.ToDateTime(DR["srok_rem"]).ToShortDateString();
            if (DR["nom_kont"] != DBNull.Value) row.Nom_kont = DR["nom_kont"].ToString();
            if (DR["prim"] != DBNull.Value) row.Prim = DR["prim"].ToString();
            return row;
        }

        public static void ListToTable(List<Row_in_kart_defect> Rows, DataTable DT)
        {
            foreach (DataRow row in DT.Rows)
                if (Rows.Find(x => x.Id == Convert.ToInt32(row["Id"])) == null) DeleteRow(Convert.ToInt32(row["id"]));
            foreach (Row_in_kart_defect row in Rows)
            {
                if (row.Id != 0)
                    FillRowInTable(row, DT.Select("id = "+row.Id)[0]);
                else
                {
                    row.Id = Convert.ToInt32(Server.GetServer.DataBase("uit").ExecuteCommand("SELECT NEXT VALUE FOR dbo.CountBy1")[0]);
                    DT.Rows.Add(DT.NewRow());
                    FillRowInTable(row, DT.Rows[DT.Rows.Count-1]);
                }
            }
        }
        
        static void DeleteRow(int pid)
        {
            List<object> tmp = Server.GetServer.DataBase("uit").ExecuteCommand("select id from rz_kart_defect where par=" 
                + pid);
            if (tmp.Count != 0)
                foreach (int id in tmp) DeleteRow(id);
            Server.GetServer.DataBase("uit").ExecuteCommand("delete from rz_kart_defect where id=" + pid);
        }

        static DataRow FillRowInTable(Row_in_kart_defect row, DataRow DR)
        {
            DR["id"] = row.Id;
            if (row.Par != 0) DR["par"] = row.Par;
            DR["nom_sz"] = row.Nom_sz;
            if (row.Cherch != null) DR["cherch"] = row.Cherch;
            if (row.N_nomer != null) DR["n_nomer"] = row.N_nomer;
            if (row.Naim != null) DR["naim"] = row.Naim;
            DR["kolvo"] = row.Kolvo;
            if (row.Nom_ceh != 0) DR["nom_ceh"] = row.Nom_ceh;
            if (row.Opis_def != -1) DR["opis_def"] = row.Opis_def;
            if (row.Opis_def_komment != null) DR["opis_def_komment"] = row.Opis_def_komment;
            if (row.Prich != -1) DR["prich"] = row.Prich;
            if (row.Prich_komment != null) DR["prich_komment"] = row.Prich_komment;
            if (row.Met_opr != -1) DR["met_opr"] = row.Met_opr;
            if (row.Met_opr_komment != null) DR["met_opr_komment"] = row.Met_opr_komment;
            if (row.Teh_treb != -1) DR["teh_treb"] = row.Teh_treb;
            if (row.Teh_treb_komment != null) DR["teh_treb_komment"] = row.Teh_treb_komment;
            if (row.Spos_ustr!= -1) DR["spos_ustr"] = row.Spos_ustr;
            if (row.Spos_ustr_komment != null) DR["spos_ustr_komment"] = row.Spos_ustr_komment;                   
            if (row.Data_def != null) DR["data_def"] = Convert.ToDateTime(row.Data_def);
            if (row.Izgotov != 0) DR["izgotov"] = row.Izgotov;
            if (row.Prim != null) DR["prim"] = row.Prim;
            DR["is_faster"] = row.IsFaster;
            return DR;
        }
        static Row_in_kart_defect FillRowFromTable(DataRow DR, Row_in_kart_defect row)
        {
            row.Id = Convert.ToInt32(DR["id"]);
            if (DR["par"] != DBNull.Value) row.Par = Convert.ToInt32(DR["par"]);
            row.Nom_sz = DR["nom_sz"].ToString();
            if (DR["cherch"] != DBNull.Value) row.Cherch = DR["cherch"].ToString();
            if (DR["n_nomer"] != DBNull.Value) row.N_nomer = DR["n_nomer"].ToString();
            if (DR["naim"] != DBNull.Value) row.Naim = DR["naim"].ToString();
            row.Kolvo = float.Parse(DR["kolvo"].ToString());
            if (DR["nom_ceh"] != DBNull.Value) row.Nom_ceh = Convert.ToInt32(DR["nom_ceh"]);
            if (DR["nom_kart"] != DBNull.Value) row.Nom_kart = Convert.ToInt32(DR["nom_kart"]);
            if (DR["opis_def"] != DBNull.Value) row.Opis_def = Convert.ToInt32(DR["opis_def"]);
            if (DR["opis_def_komment"] != DBNull.Value) row.Opis_def_komment = DR["opis_def_komment"].ToString();
            if (DR["prich"] != DBNull.Value) row.Prich = Convert.ToInt32(DR["prich"]);
            if (DR["prich_komment"] != DBNull.Value) row.Prich_komment = DR["prich_komment"].ToString();
            if (DR["met_opr"] != DBNull.Value) row.Met_opr = Convert.ToInt32(DR["met_opr"]);
            if (DR["met_opr_komment"] != DBNull.Value) row.Met_opr_komment = DR["met_opr_komment"].ToString();
            if (DR["teh_treb"] != DBNull.Value) row.Teh_treb = Convert.ToInt32(DR["teh_treb"]);
            if (DR["teh_treb_komment"] != DBNull.Value) row.Teh_treb_komment = DR["teh_treb_komment"].ToString();
            if (DR["spos_ustr"] != DBNull.Value) row.Spos_ustr = Convert.ToInt32(DR["spos_ustr"]);
            if (DR["spos_ustr_komment"] != DBNull.Value) row.Spos_ustr_komment = DR["spos_ustr_komment"].ToString();
            if (DR["data_post"] != DBNull.Value) row.Data_post = DR["data_post"].ToString().Substring(0,10);
            if (DR["data_def"] != DBNull.Value) row.Data_def = DR["data_def"].ToString().Substring(0,10);
            if (DR["izgotov"] != DBNull.Value) row.Izgotov = Convert.ToInt32(DR["izgotov"]);
            if (DR["prim"] != DBNull.Value) row.Prim = DR["prim"].ToString();
            row.IsFaster = Convert.ToBoolean(DR["is_faster"]);
            return row;
        }

        public static ObservableCollection<RowPlanViewModal> ToViewModal(List<Row_in_plan_rabot> Rows)
        {
            ObservableCollection<RowPlanViewModal> result = new ObservableCollection<RowPlanViewModal>();
            foreach (Row_in_plan_rabot row in Rows) result.Add(new RowPlanViewModal(row));
            return result;
        }
        public static ObservableCollection<RowDefectViewModal> ToViewModal(List<Row_in_kart_defect> Rows)
        {
            ObservableCollection<RowDefectViewModal> result = new ObservableCollection<RowDefectViewModal>();
            foreach (Row_in_kart_defect row in Rows) result.Add(new RowDefectViewModal(row));
            return result;
        }
        public static ObservableCollection<ShopAlertViewModal> ToViewModalShop(List<Row_in_kart_defect> Rows)
        {
            ObservableCollection<ShopAlertViewModal> result = new ObservableCollection<ShopAlertViewModal>();
            foreach (Row_in_kart_defect row in Rows) result.Add(new ShopAlertViewModal(row));
            return result;
        }

        public static List<Row_in_plan_rabot> ToSave(ObservableCollection<RowPlanViewModal> Rows)
        {
            List<Row_in_plan_rabot> result = new List<Row_in_plan_rabot>();
            foreach (RowPlanViewModal row in Rows) result.Add(row.Save);
            return result;
        }
        public static List<Row_in_kart_defect> ToSave(ObservableCollection<RowDefectViewModal> Rows)
        {
            List<Row_in_kart_defect> result = new List<Row_in_kart_defect>();
            foreach (RowDefectViewModal row in Rows) result.Add(row.Save);
            return result;
        }
    }
}
