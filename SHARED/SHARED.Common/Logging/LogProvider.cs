using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SHARED.Models.Interfaces;

namespace SHARED.Common.Logging
{
    public class LogProvider
    {
        static LogProvider()
        {
            Current = new LogProvider();
        }

        private LogProvider() { }

        public static LogProvider Current { get; private set; }

        public ICRUDOperationsLogger Logger { get; set; }
    }
}
