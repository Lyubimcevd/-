using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cards_of_defectation.Classes;

namespace Cards_of_defectation.ОУП.ViewModal
{
    class WorkShopViewModal
    {
        int nom_ceh;
        public WorkShopViewModal(int pnom_ceh)
        {
            nom_ceh = pnom_ceh;
        }
        public string Nom_ceh
        {
            get
            {
                return References.InitReferences().Cehs[nom_ceh - 1];
            }
        }
        public string Kart
        {
            get
            {
                string result = Server.InitServer().DataBase("uit")
                    .ExecuteCommand("select count(*) from rz_kart_defect where nom_ceh = "
                    + nom_ceh + " group by nom_ceh")[0].ToString() + " / ";
                List<object> tmp = Server.InitServer().DataBase("uit")
                    .ExecuteCommand("select count(*) from rz_kart_defect where nom_ceh = "
                    + nom_ceh + " and data_def is not null group by nom_sz");
                if (tmp.Count == 0) result += "0";
                else result += tmp[0].ToString();
                return result;
            }
        }
    }
}
