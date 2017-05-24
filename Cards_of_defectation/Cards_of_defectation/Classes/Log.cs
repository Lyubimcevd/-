using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;

namespace Cards_of_defectation.Classes
{
    public class Log
    {
        static Log log;
        static Logger loger;
        Log()
        {
            loger = LogManager.GetCurrentClassLogger();
        }
        static public Logger Init
        {
            get
            {
                if (log == null) log = new Log();
                return loger;
            }
        }
    }
}
