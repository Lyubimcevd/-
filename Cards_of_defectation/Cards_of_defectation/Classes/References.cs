using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cards_of_defectation.Classes;
using System.Windows;
using System.Collections.ObjectModel;

namespace Cards_of_defectation.Classes
{
    class References
    {
        static References isone;
        List<string> causes_of_defect, determination_method_of_defect, technical_requirements, defects, elimination_method,
            cehs;
        ObservableCollection<RowDefectViewModal> composition, armsearch;
        private References() { }
        public static References InitReferences()
        {
            if (isone == null) isone = new References();
            return isone;
        }

        public ObservableCollection<RowDefectViewModal> InitComposition(RowDefectViewModal parent)
        {            
            composition = new ObservableCollection<RowDefectViewModal>();
            List<object> tmp = new List<object>();
            tmp = Server.InitServer().DataBase("uit")
                .ExecuteCommand("select ltrim(rtrim(nc)) from spkd1 where nk = '" 
                                + parent.Obozn_det_for_search + "' order by nc");
            foreach (string obozn in tmp) composition.Add(new RowDefectViewModal(obozn,parent));
            return composition;
        }
        public ObservableCollection<RowDefectViewModal> SearchAndLoad(RowDefectViewModal parent, string order)
        {
            if (armsearch == null) armsearch = new ObservableCollection<RowDefectViewModal>();
            bool is_here = false;
            foreach (RowDefectViewModal row in armsearch)
                if (row.Obozn_det == order)
                {
                    is_here = true;
                    MessageBox.Show("Чертёж уже добавлен в список");
                    break;
                }
            if (!is_here) armsearch.Add(new RowDefectViewModal(order, parent));
            return armsearch;
        }
        public List<string> Causes_of_defect
        {
            get
            {
                if (causes_of_defect == null)
                {
                    List<object> tmp = Server.InitServer().DataBase("test1").ExecuteCommand("select * from prichina");
                    causes_of_defect = new List<string>();
                    foreach (string el in tmp) causes_of_defect.Add(el);
                }
                return causes_of_defect;
            }
        }
        public List<string> Determination_method_of_defect
        {
            get
            {
                if (determination_method_of_defect == null)
                {
                    List<object> tmp = Server.InitServer().DataBase("test1").ExecuteCommand("select * from met_opred");
                    determination_method_of_defect = new List<string>();
                    foreach (string el in tmp) determination_method_of_defect.Add(el);
                }
                return determination_method_of_defect;
            }
        }
        public List<string> Technical_requirements
        {
            get
            {
                if (technical_requirements == null)
                {
                    List<object> tmp = Server.InitServer().DataBase("test1").ExecuteCommand("select * from teh_treb");
                    technical_requirements = new List<string>();
                    foreach (string el in tmp) technical_requirements.Add(el);
                }
                return technical_requirements;
            }
        }
        public List<string> Defects
        {
            get
            {
                if (defects == null)
                {
                    List<object> tmp = Server.InitServer().DataBase("test1").ExecuteCommand("select * from opis_defect");
                    defects = new List<string>();
                    foreach (string el in tmp) defects.Add(el);
                }
                return defects;
            }
        }
        public List<string> Elimination_method
        {
            get
            {
                if (elimination_method == null)
                {
                    List<object> tmp = Server.InitServer().DataBase("test1").ExecuteCommand("select * from spos_ustr");
                    elimination_method = new List<string>();
                    foreach (string el in tmp) elimination_method.Add(el);
                }
                return elimination_method;
            }
        }
        public List<string> Cehs
        {
            get
            {
                if (cehs == null)
                {
                    List<object> tmp = Server.InitServer().DataBase("test1").ExecuteCommand("select * from ceh");
                    cehs = new List<string>();
                    foreach (string el in tmp) cehs.Add(el);
                }
                return cehs;
            }
        }
        public bool IsSerInReference(string Ser_nom_izd)
        {
            List<object> tmp = Server.InitServer().DataBase("test1")
              .ExecuteCommand("select [Заводской номер изделия] from nom_type where [Номер изделия] = '"
                              + Ser_nom_izd + "'");
            if (tmp.Count != 0)
            {
                tmp = Server.InitServer().DataBase("test1")
                    .ExecuteCommand("select Чертёж from type_cherch where [Заводской номер изделия] = '"
                                    + tmp[0].ToString() + "'");
                if (tmp.Count != 0) return true;
                MessageBox.Show("В справочнике 'Тип изделия - чертёж' не задано соответствие изделия чертежу", "Ошибка");
                return false;
            }
            MessageBox.Show("В справочнике 'Номер изделия - тип изделия' не задано соответствие серийного номера изделия заводскому номеру", "Ошибка");
            return false;
        }
    }
}
