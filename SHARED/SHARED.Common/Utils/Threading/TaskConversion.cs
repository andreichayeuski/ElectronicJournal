using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace SHARED.Common.Utils.Threading
{
    public static class TaskConversion
    {
        public static TaskFromAsyncEventPatternBuilder<TArgs> FromEventPattern<TArgs>()
            where TArgs : AsyncCompletedEventArgs
        {
            return TaskFromAsyncEventPatternBuilder<TArgs>.Instance;
        }

        internal static Task<TResult> CreateTaskFromAsyncEvent<TEventHandler, TArgs, TResult>(
            Guid operationToken,
            Func<Action<object, TArgs>, TEventHandler> eventHandlerConversion,
            Action<TEventHandler> addEventHandler,
            Action<TEventHandler> removeEventHandler,
            Func<TArgs, TResult> getOperationResult)
            where TArgs : AsyncCompletedEventArgs
        {
            var taskSource = new TaskCompletionSource<TResult>();
            var task = taskSource.Task;

            var eventHandler =
                eventHandlerConversion((sender, args) =>
                {
                    if (!(args.UserState is Guid))
                    {
                        // Operation was invoked by someone else.
                        // We always provide Guid token on invocation.
                        return;
                    }

                    var completedOperationToken = (Guid)args.UserState;
                    if (operationToken != completedOperationToken)
                    {
                        // Token differs, i.e another operation was completed.
                        return;
                    }

                    if (args.Cancelled)
                    {
                        taskSource.SetCanceled();
                    }
                    else if (args.Error != null)
                    {
                        taskSource.SetException(args.Error);
                    }
                    else
                    {
                        taskSource.SetResult(getOperationResult(args));
                    }
                });

            addEventHandler(eventHandler);

            return task.ContinueWith(
                completed =>
                {
                    removeEventHandler(eventHandler);

                    if (completed.Exception != null)
                    {
                        // Completed task is created from TaskCompletionSource by us,
                        // so we are expecting the one exception wrapped by AggregatedException.
                        // Unwrapping it in that case and rethrowing. But, if AggregatedException 
                        // handles multiple ones, rethrowing it instead of inner.
                        var exception =
                            completed.Exception.InnerExceptions.Count == 1
                                ? completed.Exception.InnerException
                                : completed.Exception;

                        ExceptionDispatchInfo
                            .Capture(exception)
                            .Throw();
                    }

                    return completed.Result;
                });
        }
    }
}
