using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cards_of_defectation.Classes
{
    class Authorization
    {
        static Authorization aut;
        static bool is_ceh;
        Authorization() { }
        static public void InitAut(bool pis_ceh)
        {
            is_ceh = pis_ceh;
        }
        static public Authorization Get
        {
            get
            {
                if (aut == null) aut = new Authorization();
                return aut;
            }
        }
        public bool IsCeh
        {
            get
            {
                return is_ceh;
            }
        }
    }
}
