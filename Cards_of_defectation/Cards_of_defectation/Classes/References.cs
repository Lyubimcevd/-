using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cards_of_defectation.Classes;
using System.Windows;
using System.Collections.ObjectModel;
using Cards_of_defectation.ViewModal;

namespace Cards_of_defectation.Classes
{
    class References
    {
        static References isone;
        ObservableCollection<RowDefectViewModal> composition, armsearch;
        private References() { }
        public static References GetReferences
        {
            get
            {
                if (isone == null) isone = new References();
                return isone;
            }           
        }

        public ObservableCollection<RowDefectViewModal> InitComposition(RowDefectViewModal parent)
        {            
            composition = new ObservableCollection<RowDefectViewModal>();
            List<object> tmp = new List<object>();
            tmp = Server.GetServer.DataBase("uit")
                .ExecuteCommand("select ltrim(rtrim(nc)) from spkd1 where nk = '" 
                                + parent.Cherch_for_search + "' order by nc");
            foreach (string obozn in tmp) composition.Add(new RowDefectViewModal(obozn,parent));
            return composition;
        }
        public ObservableCollection<RowDefectViewModal> SearchAndLoad(RowDefectViewModal parent, string order)
        {
            if (armsearch == null) armsearch = new ObservableCollection<RowDefectViewModal>();
            bool is_here = false;
            foreach (RowDefectViewModal row in armsearch)
                if (row.Cherch == order)
                {
                    is_here = true;
                    MessageBox.Show("Чертёж уже добавлен в список");
                    break;
                }
            if (!is_here) armsearch.Add(new RowDefectViewModal(order, parent));
            return armsearch;
        }
        public List<string> ReferenceByName(string table_name)
        {
            List<string> result = new List<string>();      
            List<object> tmp = Server.GetServer.DataBase("uit").ExecuteCommand("select naim from "+table_name);
            foreach (string el in tmp) result.Add(el);
            return result;
        }
        public bool IsSerInReference(string Ser_nom_izd)
        {
            List<object> tmp = Server.GetServer.DataBase("uit")
              .ExecuteCommand("select naim from rz_ser_nom_naim where ser_nom = '"
                              + Ser_nom_izd + "'");
            if (tmp.Count != 0)
            {
                tmp = Server.GetServer.DataBase("uit")
                    .ExecuteCommand("select cherch from rz_naim_cherch where naim = '"
                                    + tmp[0].ToString() + "'");
                if (tmp.Count != 0) return true;
                MessageBox.Show("В справочнике 'Тип изделия - чертёж' не задано соответствие изделия чертежу", "Ошибка");
                return false;
            }
            MessageBox.Show("В справочнике 'Номер изделия - тип изделия' не задано соответствие серийного номера изделия заводскому номеру", "Ошибка");
            return false;
        }
        public int GetId(string table_name,string naim)
        {
            return Convert.ToInt32(Server.GetServer.DataBase("uit")
                .ExecuteCommand("select id from " + table_name + " where naim = '" + naim + "'")[0]);
        }
        public string GetNaim(string table_naim,int id)
        {
            return Server.GetServer.DataBase("uit")
                .ExecuteCommand("select naim from " + table_naim + " where id =" + id)[0].ToString();
        }
        public int GetIdCeh(string ceh_naim)
        {          
            return Convert.ToInt32(Server.GetServer.DataBase("cvodka")
                    .ExecuteCommand("select id from podr1 where nc = '" + ceh_naim+"'")[0]);
        }
        public string GetNaimCeh(int id)
        {
            return Server.GetServer.DataBase("cvodka")
                    .ExecuteCommand("select nc from podr1 where id = " + id)[0].ToString();
        }
    }
}
