using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections.ObjectModel;

namespace Cards_of_defectation.Classes
{
    class Converter
    {
        public static void ListToTable(List<Row_in_plan_rabot> Rows, DataTable DT)
        {
            foreach (DataRow row in DT.Rows)
                if (Rows.Find(x => x.Nom_zay == Convert.ToInt32(row["nom_zay"])) == null) row.Delete();
            foreach (Row_in_plan_rabot row in Rows)
            {
                DataRow[] tmp = DT.Select("nom_zay =" + row.Nom_zay.ToString());
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
            if (DT.TableName == "plan_rabot")
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
            DR[0] = row.Nom_zay;
            if (row.Ser_nom_izd!= null) DR[1] = row.Ser_nom_izd;
            if (row.Voin_chast != null) DR[2] = row.Voin_chast;
            if (row.Data_uved != null) DR[3] = row.Data_uved;
            if (row.Nom_uved != null) DR[4] = row.Nom_uved;
            if (row.Srok_def != null) DR[5] = row.Srok_def;
            if (row.Srok_def_komment != null) DR[6] = row.Srok_def_komment;
            if (row.Srok_dost != null) DR[7] = row.Srok_dost;
            if (row.Srok_dost_komment != null) DR[8] = row.Srok_dost_komment;
            if (row.Srok_def_predpr != null) DR[9] = row.Srok_def_predpr;
            if (row.Srok_def_predpr_komment != null) DR[10] = row.Srok_def_predpr_komment;
            if (row.Srok_rem != null) DR[11] = row.Srok_rem;
            if (row.Srok_rem_komment != null) DR[12] = row.Srok_rem_komment;
            if (row.Srok_rem_soisp != null) DR[13] = row.Srok_rem_soisp;
            if (row.Soisp != null) DR[14] = row.Soisp;
            if (row.Srok_vosstan != null) DR[15] = row.Srok_vosstan;
            if (row.Srok_vosstan_komment != null) DR[16] = row.Srok_vosstan_komment;
            if (row.Prim != null) DR[17] = row.Prim;
            if (row.Nom_zak != 0) DR[18] = row.Nom_zak;
            if (row.Type_rem != -1) DR[19] = row.Type_rem;
            if (row.Last_prim != null) DR[20] = row.Last_prim;
            if (row.Kontract != null) DR[21] = row.Kontract;
            return DR;
        }
        static Row_in_plan_rabot FillRowFromTable(DataRow DR, Row_in_plan_rabot row)
        {
            row.Nom_zay = Convert.ToInt32(DR[0]);
            if (DR[1] != DBNull.Value) row.Ser_nom_izd = DR[1].ToString();
            if (DR[2] != DBNull.Value) row.Voin_chast = DR[2].ToString();
            if (DR[3] != DBNull.Value) row.Data_uved = DR[3].ToString();
            if (DR[4] != DBNull.Value) row.Nom_uved = DR[4].ToString();
            if (DR[5] != DBNull.Value) row.Srok_def = Convert.ToDateTime(DR[5]).ToShortDateString();
            if (DR[6] != DBNull.Value) row.Srok_def_komment = DR[6].ToString();
            if (DR[7] != DBNull.Value) row.Srok_dost = Convert.ToDateTime(DR[7]).ToShortDateString();
            if (DR[8] != DBNull.Value) row.Srok_dost_komment = DR[8].ToString();
            if (DR[9] != DBNull.Value) row.Srok_def_predpr = Convert.ToDateTime(DR[9]).ToShortDateString();
            if (DR[10] != DBNull.Value) row.Srok_def_predpr_komment = DR[10].ToString();
            if (DR[11] != DBNull.Value) row.Srok_rem = Convert.ToDateTime(DR[11]).ToShortDateString();
            if (DR[12] != DBNull.Value) row.Srok_rem_komment = DR[12].ToString();
            if (DR[13] != DBNull.Value) row.Srok_rem_soisp = Convert.ToDateTime(DR[13]).ToShortDateString();
            if (DR[14] != DBNull.Value) row.Soisp = DR[14].ToString();
            if (DR[15] != DBNull.Value) row.Srok_vosstan = Convert.ToDateTime(DR[15]).ToShortDateString();
            if (DR[16] != DBNull.Value) row.Srok_vosstan_komment = DR[16].ToString();
            if (DR[17] != DBNull.Value) row.Prim = DR[17].ToString();
            if (DR[18] != DBNull.Value) row.Nom_zak = Convert.ToInt32(DR[18]);
            if (DR[19] != DBNull.Value) row.Type_rem = Convert.ToInt32(DR[19]);
            if (DR[20] != DBNull.Value) row.Last_prim = DR[20].ToString();
            if (DR[21] != DBNull.Value) row.Kontract = DR[21].ToString();
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
                    row.Id = Convert.ToInt32(Server.InitServer().DataBase("test1").ExecuteCommand("SELECT NEXT VALUE FOR dbo.CountBy1")[0]);
                    DT.Rows.Add(DT.NewRow());
                    FillRowInTable(row, DT.Rows[DT.Rows.Count-1]);
                }
            }
        }
        
        static void DeleteRow(int pid)
        {
            List<object> tmp = Server.InitServer().DataBase("test1").ExecuteCommand("select id from kart_defect where par=" 
                + pid);
            if (tmp.Count != 0)
                foreach (int id in tmp) DeleteRow(id);
            Server.InitServer().DataBase("test1").ExecuteCommand("delete from kart_defect where id=" + pid);
        }

        static DataRow FillRowInTable(Row_in_kart_defect row, DataRow DR)
        {
            DR[0] = row.Nom_zay;
            if (row.Obozn_det != null) DR[1] = row.Obozn_det;
            DR[2] = row.Kolvo;
            if (row.Opis_def != -1) DR[4] = row.Opis_def;
            if (row.Opis_def_komment != null) DR[5] = row.Opis_def_komment;
            if (row.Prichina != -1) DR[6] = row.Prichina;
            if (row.Prichina_komment != null) DR[7] = row.Prichina_komment;
            if (row.Met_opr != -1) DR[8] = row.Met_opr;
            if (row.Met_opr_komment != null) DR[9] = row.Met_opr_komment;
            if (row.Teh_treb != -1) DR[10] = row.Teh_treb;
            if (row.Teh_treb_komment != null) DR[11] = row.Teh_treb_komment;
            if (row.Spos_ustr!= -1) DR[12] = row.Spos_ustr;
            if (row.Spos_ustr_komment != null) DR[13] = row.Spos_ustr_komment;
            if (row.Nom_ceh != -1) DR[14] = row.Nom_ceh;
            if (row.Par != 0) DR[15] = row.Par;
            if (row.Id != 0) DR[16] = row.Id;
            if (row.Data_post != null) DR[17] = Convert.ToDateTime(row.Data_post);
            if (row.Data_def != null) DR[18] = Convert.ToDateTime(row.Data_def);
            if (row.N_nomer != null) DR[19] = row.N_nomer;
            if (row.Prim != null) DR[20] = row.Prim;
            if (row.Zavod_izgot != null) DR[21] = row.Zavod_izgot;
            if (row.Naim != null) DR[22] = row.Naim;
            return DR;
        }
        static Row_in_kart_defect FillRowFromTable(DataRow DR, Row_in_kart_defect row)
        {
            row.Nom_zay = Convert.ToInt32(DR[0]);
            if (DR[1] != DBNull.Value) row.Obozn_det = DR[1].ToString();
            row.Kolvo = float.Parse(DR[2].ToString());
            row.Nom_kart = Convert.ToInt32(DR[3]);
            if (DR[4] != DBNull.Value) row.Opis_def = Convert.ToInt32(DR[4]);
            if (DR[5] != DBNull.Value) row.Opis_def_komment = DR[5].ToString();
            if (DR[6] != DBNull.Value) row.Prichina = Convert.ToInt32(DR[6]);
            if (DR[7] != DBNull.Value) row.Prichina_komment = DR[7].ToString();
            if (DR[8] != DBNull.Value) row.Met_opr = Convert.ToInt32(DR[8]);
            if (DR[9] != DBNull.Value) row.Met_opr_komment = DR[9].ToString();
            if (DR[10] != DBNull.Value) row.Teh_treb = Convert.ToInt32(DR[10]);
            if (DR[11] != DBNull.Value) row.Teh_treb_komment = DR[11].ToString();
            if (DR[12] != DBNull.Value) row.Spos_ustr = Convert.ToInt32(DR[12]);
            if (DR[13] != DBNull.Value) row.Spos_ustr_komment = DR[13].ToString();
            if (DR[14] != DBNull.Value) row.Nom_ceh = Convert.ToInt32(DR[14]);
            if (DR[15] != DBNull.Value) row.Par = Convert.ToInt32(DR[15]);
            row.Id = Convert.ToInt32(DR[16]);
            if (DR[17] != DBNull.Value) row.Data_post = DR[17].ToString().Substring(0,10);
            if (DR[18] != DBNull.Value) row.Data_def = DR[18].ToString().Substring(0,10);
            if (DR[19] != DBNull.Value) row.N_nomer = DR[19].ToString();
            if (DR[20] != DBNull.Value) row.Prim = DR[20].ToString();
            if (DR[21] != DBNull.Value) row.Zavod_izgot = DR[21].ToString();
            if (DR[22] != DBNull.Value) row.Naim = DR[22].ToString();
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
