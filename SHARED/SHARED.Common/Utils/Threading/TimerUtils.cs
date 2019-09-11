using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SHARED.Common.Utils.Threading
{
    public static class TimerUtils
    {
        public static Timer Create(Action action, TimeSpan dueTime, TimeSpan period)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            var timer = new Timer(_ => action(), null, dueTime, period);
            return timer;
        }
    }
}
