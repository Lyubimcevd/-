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
            DR[0] = row.Nom_sz;
            if (row.Ser_nom!= null) DR[1] = row.Ser_nom;
            if (row.Voin_chast != null) DR[2] = row.Voin_chast;
            if (row.Nom_zak != null) DR[3] = row.Nom_zak;
            if (row.Srok_rem != null) DR[4] = row.Srok_rem;
            if (row.Nom_kont != null) DR[5] = row.Nom_kont;
            if (row.Prim != null) DR[6] = row.Prim;          
            return DR;
        }
        static Row_in_plan_rabot FillRowFromTable(DataRow DR, Row_in_plan_rabot row)
        {
            row.Nom_sz = DR[0].ToString();
            if (DR[1] != DBNull.Value) row.Ser_nom = DR[1].ToString();
            if (DR[2] != DBNull.Value) row.Voin_chast = DR[2].ToString();
            if (DR[3] != DBNull.Value) row.Nom_zak = DR[3].ToString();
            if (DR[4] != DBNull.Value) row.Srok_rem = Convert.ToDateTime(DR[4]).ToShortDateString();
            if (DR[5] != DBNull.Value) row.Nom_kont = DR[5].ToString();
            if (DR[6] != DBNull.Value) row.Prim = DR[6].ToString();
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
                    row.Id = Convert.ToInt32(Server.InitServer().DataBase("uit").ExecuteCommand("SELECT NEXT VALUE FOR dbo.CountBy1")[0]);
                    DT.Rows.Add(DT.NewRow());
                    FillRowInTable(row, DT.Rows[DT.Rows.Count-1]);
                }
            }
        }
        
        static void DeleteRow(int pid)
        {
            List<object> tmp = Server.InitServer().DataBase("uit").ExecuteCommand("select id from rz_kart_defect where par=" 
                + pid);
            if (tmp.Count != 0)
                foreach (int id in tmp) DeleteRow(id);
            Server.InitServer().DataBase("uit").ExecuteCommand("delete from rz_kart_defect where id=" + pid);
        }

        static DataRow FillRowInTable(Row_in_kart_defect row, DataRow DR)
        {
            DR[0] = row.Id;
            if (row.Par != 0) DR[1] = row.Par;
            DR[2] = row.Nom_sz;
            if (row.Cherch != null) DR[3] = row.Cherch;
            if (row.N_nomer != null) DR[4] = row.N_nomer;
            if (row.Naim != null) DR[5] = row.Naim;
            DR[6] = row.Kolvo;
            if (row.Nom_ceh != 0) DR[7] = row.Nom_ceh;
            if (row.Opis_def != -1) DR[9] = row.Opis_def;
            if (row.Opis_def_komment != null) DR[10] = row.Opis_def_komment;
            if (row.Prich != -1) DR[11] = row.Prich;
            if (row.Prich_komment != null) DR[12] = row.Prich_komment;
            if (row.Met_opr != -1) DR[13] = row.Met_opr;
            if (row.Met_opr_komment != null) DR[14] = row.Met_opr_komment;
            if (row.Teh_treb != -1) DR[15] = row.Teh_treb;
            if (row.Teh_treb_komment != null) DR[16] = row.Teh_treb_komment;
            if (row.Spos_ustr!= -1) DR[17] = row.Spos_ustr;
            if (row.Spos_ustr_komment != null) DR[18] = row.Spos_ustr_komment;                   
            if (row.Data_post != null) DR[19] = Convert.ToDateTime(row.Data_post);
            if (row.Data_def != null) DR[20] = Convert.ToDateTime(row.Data_def);
            if (row.Izgotov != 0) DR[21] = row.Izgotov;
            if (row.Prim != null) DR[22] = row.Prim;
            return DR;
        }
        static Row_in_kart_defect FillRowFromTable(DataRow DR, Row_in_kart_defect row)
        {
            row.Id = Convert.ToInt32(DR[0]);
            if (DR[1] != DBNull.Value) row.Par = Convert.ToInt32(DR[1]);
            row.Nom_sz = DR[2].ToString();
            if (DR[3] != DBNull.Value) row.Cherch = DR[3].ToString();
            if (DR[4] != DBNull.Value) row.N_nomer = DR[4].ToString();
            if (DR[5] != DBNull.Value) row.Naim = DR[5].ToString();
            row.Kolvo = float.Parse(DR[6].ToString());
            if (DR[7] != DBNull.Value) row.Nom_ceh = Convert.ToInt32(DR[7]);
            if (DR[8] != DBNull.Value) row.Nom_kart = Convert.ToInt32(DR[8]);
            if (DR[9] != DBNull.Value) row.Opis_def = Convert.ToInt32(DR[9]);
            if (DR[10] != DBNull.Value) row.Opis_def_komment = DR[10].ToString();
            if (DR[11] != DBNull.Value) row.Prich = Convert.ToInt32(DR[11]);
            if (DR[12] != DBNull.Value) row.Prich_komment = DR[12].ToString();
            if (DR[13] != DBNull.Value) row.Met_opr = Convert.ToInt32(DR[13]);
            if (DR[14] != DBNull.Value) row.Met_opr_komment = DR[14].ToString();
            if (DR[15] != DBNull.Value) row.Teh_treb = Convert.ToInt32(DR[15]);
            if (DR[16] != DBNull.Value) row.Teh_treb_komment = DR[16].ToString();
            if (DR[17] != DBNull.Value) row.Spos_ustr = Convert.ToInt32(DR[17]);
            if (DR[18] != DBNull.Value) row.Spos_ustr_komment = DR[18].ToString();
            if (DR[19] != DBNull.Value) row.Data_post = DR[19].ToString().Substring(0,10);
            if (DR[20] != DBNull.Value) row.Data_def = DR[20].ToString().Substring(0,10);
            if (DR[21] != DBNull.Value) row.Izgotov = Convert.ToInt32(DR[21]);
            if (DR[22] != DBNull.Value) row.Prim = DR[22].ToString();
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
