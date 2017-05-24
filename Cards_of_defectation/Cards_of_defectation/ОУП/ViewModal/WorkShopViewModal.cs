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
        string nom_sz;

        public WorkShopViewModal(int pnom_ceh,string pnom_sz)
        {
            nom_ceh = pnom_ceh;
            nom_sz = pnom_sz;
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
                Log.Init.Info("Формирование всего/выполнено в work_shop");
                string result = Server.InitServer().DataBase("uit")
                    .ExecuteCommand("select count(*) from rz_kart_defect where nom_ceh = "
                    + nom_ceh + " and nom_sz = '" + nom_sz+ "' group by nom_ceh")[0].ToString() + " / ";
                List<object> tmp = Server.InitServer().DataBase("uit")
                    .ExecuteCommand("select count(*) from rz_kart_defect where nom_ceh = "
                    + nom_ceh + " and data_def is not null and nom_sz = '"+nom_sz+"' group by nom_sz");
                if (tmp.Count == 0) result += "0";
                else result += tmp[0].ToString();
                Log.Init.Info("Формирование завершено. Результат "+result);
                return result;
            }
        }
    }
}
