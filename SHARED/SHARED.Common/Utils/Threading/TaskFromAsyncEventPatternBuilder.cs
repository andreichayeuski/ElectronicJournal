using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARED.Common.Utils.Threading
{
    public sealed class TaskFromAsyncEventPatternBuilder<TArgs>
        where TArgs : AsyncCompletedEventArgs
    {
        internal static readonly TaskFromAsyncEventPatternBuilder<TArgs> Instance =
            new TaskFromAsyncEventPatternBuilder<TArgs>();

        private TaskFromAsyncEventPatternBuilder()
        {
            /* empty */
        }

        public TaskFromAsyncEventPatternInvocation<TEventHandler, TArgs, TResult> ForEvent<TEventHandler, TResult>(
            Func<Action<object, TArgs>, TEventHandler> eventHandlerConversion,
            Action<TEventHandler> addEventHandler,
            Action<TEventHandler> removeEventHandler,
            Func<TArgs, TResult> getOperationResult)
        {
            if (eventHandlerConversion == null)
            {
                throw new ArgumentNullException("eventHandlerConversion");
            }

            if (addEventHandler == null)
            {
                throw new ArgumentNullException("addEventHandler");
            }

            if (removeEventHandler == null)
            {
                throw new ArgumentNullException("removeEventHandler");
            }

            if (getOperationResult == null)
            {
                throw new ArgumentNullException("getOperationResult");
            }

            var result =
                new TaskFromAsyncEventPatternInvocation<TEventHandler, TArgs, TResult>(
                    eventHandlerConversion,
                    addEventHandler,
                    removeEventHandler,
                    getOperationResult);

            return result;
        }
    }
}
