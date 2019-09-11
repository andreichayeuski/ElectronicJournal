using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARED.Common.Utils.Threading
{

    public sealed class TaskFromAsyncEventPatternInvocation<TEventHandler, TArgs, TResult>
        where TArgs : AsyncCompletedEventArgs
    {
        private readonly Guid _operationToken;
        private readonly Task<TResult> _task;

        private bool _isInvoked;

        internal TaskFromAsyncEventPatternInvocation(
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

            _operationToken = Guid.NewGuid();

            _task =
                TaskConversion.CreateTaskFromAsyncEvent(
                    _operationToken,
                    eventHandlerConversion,
                    addEventHandler,
                    removeEventHandler,
                    getOperationResult);
        }

        public Task<TResult> Invoke(
            Action<object> asyncOperation)
        {
            if (asyncOperation == null)
            {
                throw new ArgumentNullException("asyncOperation");
            }

            MarkAsInvoked();
            asyncOperation(_operationToken);

            return _task;
        }

        public Task<TResult> Invoke<TArg1>(
            Action<TArg1, object> asyncOperation,
            TArg1 arg1)
        {
            if (asyncOperation == null)
            {
                throw new ArgumentNullException("asyncOperation");
            }

            MarkAsInvoked();
            asyncOperation(arg1, _operationToken);

            return _task;
        }

        public Task<TResult> Invoke<TArg1, TArg2>(
            Action<TArg1, TArg2, object> asyncOperation,
            TArg1 arg1, TArg2 arg2)
        {
            if (asyncOperation == null)
            {
                throw new ArgumentNullException("asyncOperation");
            }

            MarkAsInvoked();
            asyncOperation(arg1, arg2, _operationToken);

            return _task;
        }

        public Task<TResult> Invoke<TArg1, TArg2, TArg3>(
            Action<TArg1, TArg2, TArg3, object> asyncOperation,
            TArg1 arg1, TArg2 arg2, TArg3 arg3)
        {
            if (asyncOperation == null)
            {
                throw new ArgumentNullException("asyncOperation");
            }

            MarkAsInvoked();
            asyncOperation(arg1, arg2, arg3, _operationToken);

            return _task;
        }

        public Task<TResult> Invoke<TArg1, TArg2, TArg3, TArg4>(
            Action<TArg1, TArg2, TArg3, TArg4, object> asyncOperation,
            TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4)
        {
            if (asyncOperation == null)
            {
                throw new ArgumentNullException("asyncOperation");
            }

            MarkAsInvoked();
            asyncOperation(arg1, arg2, arg3, arg4, _operationToken);

            return _task;
        }

        public Task<TResult>
            Invoke<TArg1, TArg2, TArg3, TArg4, TArg5>(
            Action<TArg1, TArg2, TArg3, TArg4, TArg5,
                   object> asyncOperation,
            TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5)
        {
            if (asyncOperation == null)
            {
                throw new ArgumentNullException("asyncOperation");
            }

            MarkAsInvoked();
            asyncOperation(
                arg1, arg2, arg3, arg4, arg5,
                _operationToken);

            return _task;
        }

        public Task<TResult>
            Invoke<TArg1, TArg2, TArg3, TArg4, TArg5,
                   TArg6>(
            Action<TArg1, TArg2, TArg3, TArg4, TArg5,
                   TArg6, object> asyncOperation,
            TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5,
            TArg6 arg6)
        {
            if (asyncOperation == null)
            {
                throw new ArgumentNullException("asyncOperation");
            }

            MarkAsInvoked();
            asyncOperation(
                arg1, arg2, arg3, arg4, arg5,
                arg6, _operationToken);

            return _task;
        }

        public Task<TResult>
            Invoke<TArg1, TArg2, TArg3, TArg4, TArg5,
                   TArg6, TArg7>(
            Action<TArg1, TArg2, TArg3, TArg4, TArg5,
                   TArg6, TArg7, object> asyncOperation,
            TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5,
            TArg6 arg6, TArg7 arg7)
        {
            if (asyncOperation == null)
            {
                throw new ArgumentNullException("asyncOperation");
            }

            MarkAsInvoked();
            asyncOperation(
                arg1, arg2, arg3, arg4, arg5,
                arg6, arg7, _operationToken);

            return _task;
        }

        public Task<TResult>
            Invoke<TArg1, TArg2, TArg3, TArg4, TArg5,
                   TArg6, TArg7, TArg8>(
            Action<TArg1, TArg2, TArg3, TArg4, TArg5,
                   TArg6, TArg7, TArg8, object> asyncOperation,
            TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5,
            TArg6 arg6, TArg7 arg7, TArg8 arg8)
        {
            if (asyncOperation == null)
            {
                throw new ArgumentNullException("asyncOperation");
            }

            MarkAsInvoked();
            asyncOperation(
                arg1, arg2, arg3, arg4, arg5,
                arg6, arg7, arg8, _operationToken);

            return _task;
        }

        public Task<TResult>
            Invoke<TArg1, TArg2, TArg3, TArg4, TArg5,
                   TArg6, TArg7, TArg8, TArg9>(
            Action<TArg1, TArg2, TArg3, TArg4, TArg5,
                   TArg6, TArg7, TArg8, TArg9, object> asyncOperation,
            TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5,
            TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9)
        {
            if (asyncOperation == null)
            {
                throw new ArgumentNullException("asyncOperation");
            }

            MarkAsInvoked();
            asyncOperation(
                arg1, arg2, arg3, arg4, arg5,
                arg6, arg7, arg8, arg9,
                _operationToken);

            return _task;
        }

        public Task<TResult>
            Invoke<TArg1, TArg2, TArg3, TArg4, TArg5,
                   TArg6, TArg7, TArg8, TArg9, TArg10>(
            Action<TArg1, TArg2, TArg3, TArg4, TArg5,
                   TArg6, TArg7, TArg8, TArg9, TArg10,
                   object> asyncOperation,
            TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5,
            TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10)
        {
            if (asyncOperation == null)
            {
                throw new ArgumentNullException("asyncOperation");
            }

            MarkAsInvoked();
            asyncOperation(
                arg1, arg2, arg3, arg4, arg5,
                arg6, arg7, arg8, arg9, arg10,
                _operationToken);

            return _task;
        }

        public Task<TResult>
            Invoke<TArg1, TArg2, TArg3, TArg4, TArg5,
                   TArg6, TArg7, TArg8, TArg9, TArg10,
                   TArg11>(
            Action<TArg1, TArg2, TArg3, TArg4, TArg5,
                   TArg6, TArg7, TArg8, TArg9, TArg10,
                   TArg11, object> asyncOperation,
            TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5,
            TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10,
            TArg11 arg11)
        {
            if (asyncOperation == null)
            {
                throw new ArgumentNullException("asyncOperation");
            }

            MarkAsInvoked();
            asyncOperation(
                arg1, arg2, arg3, arg4, arg5,
                arg6, arg7, arg8, arg9, arg10,
                arg11, _operationToken);

            return _task;
        }

        public Task<TResult>
            Invoke<TArg1, TArg2, TArg3, TArg4, TArg5,
                   TArg6, TArg7, TArg8, TArg9, TArg10,
                   TArg11, TArg12>(
            Action<TArg1, TArg2, TArg3, TArg4, TArg5,
                   TArg6, TArg7, TArg8, TArg9, TArg10,
                   TArg11, TArg12, object> asyncOperation,
            TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5,
            TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10,
            TArg11 arg11, TArg12 arg12)
        {
            if (asyncOperation == null)
            {
                throw new ArgumentNullException("asyncOperation");
            }

            MarkAsInvoked();
            asyncOperation(
                arg1, arg2, arg3, arg4, arg5,
                arg6, arg7, arg8, arg9, arg10,
                arg11, arg12, _operationToken);

            return _task;
        }

        public Task<TResult>
            Invoke<TArg1, TArg2, TArg3, TArg4, TArg5,
                   TArg6, TArg7, TArg8, TArg9, TArg10,
                   TArg11, TArg12, TArg13>(
            Action<TArg1, TArg2, TArg3, TArg4, TArg5,
                   TArg6, TArg7, TArg8, TArg9, TArg10,
                   TArg11, TArg12, TArg13, object> asyncOperation,
            TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5,
            TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10,
            TArg11 arg11, TArg12 arg12, TArg13 arg13)
        {
            if (asyncOperation == null)
            {
                throw new ArgumentNullException("asyncOperation");
            }

            MarkAsInvoked();
            asyncOperation(
                arg1, arg2, arg3, arg4, arg5,
                arg6, arg7, arg8, arg9, arg10,
                arg11, arg12, arg13, _operationToken);

            return _task;
        }

        public Task<TResult>
            Invoke<TArg1, TArg2, TArg3, TArg4, TArg5,
                   TArg6, TArg7, TArg8, TArg9, TArg10,
                   TArg11, TArg12, TArg13, TArg14>(
            Action<TArg1, TArg2, TArg3, TArg4, TArg5,
                TArg6, TArg7, TArg8, TArg9, TArg10,
                TArg11, TArg12, TArg13, TArg14, object> asyncOperation,
            TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5,
            TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10,
            TArg11 arg11, TArg12 arg12, TArg13 arg13, TArg14 arg14)
        {
            if (asyncOperation == null)
            {
                throw new ArgumentNullException("asyncOperation");
            }

            MarkAsInvoked();
            asyncOperation(
                arg1, arg2, arg3, arg4, arg5,
                arg6, arg7, arg8, arg9, arg10,
                arg11, arg12, arg13, arg14, _operationToken);

            return _task;
        }

        public Task<TResult>
            Invoke<TArg1, TArg2, TArg3, TArg4, TArg5,
                   TArg6, TArg7, TArg8, TArg9, TArg10,
                   TArg11, TArg12, TArg13, TArg14, TArg15>(
            Action<TArg1, TArg2, TArg3, TArg4, TArg5,
                   TArg6, TArg7, TArg8, TArg9, TArg10,
                   TArg11, TArg12, TArg13, TArg14, TArg15, object> asyncOperation,
            TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5,
            TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10,
            TArg11 arg11, TArg12 arg12, TArg13 arg13, TArg14 arg14, TArg15 arg15)
        {
            if (asyncOperation == null)
            {
                throw new ArgumentNullException("asyncOperation");
            }

            MarkAsInvoked();
            asyncOperation(
                arg1, arg2, arg3, arg4, arg5,
                arg6, arg7, arg8, arg9, arg10,
                arg11, arg12, arg13, arg14, arg15, _operationToken);

            return _task;
        }

        public Task<TResult>
            Invoke<TArg1, TArg2, TArg3, TArg4, TArg5,
                   TArg6, TArg7, TArg8, TArg9, TArg10,
                   TArg11, TArg12, TArg13, TArg14, TArg15,
                   TArg16>(
            Action<TArg1, TArg2, TArg3, TArg4, TArg5,
                   TArg6, TArg7, TArg8, TArg9, TArg10,
                   TArg11, TArg12, TArg13, TArg14, TArg15,
                   TArg16, object> asyncOperation,
            TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5,
            TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10,
            TArg11 arg11, TArg12 arg12, TArg13 arg13, TArg14 arg14, TArg15 arg15,
            TArg16 arg16)
        {
            if (asyncOperation == null)
            {
                throw new ArgumentNullException("asyncOperation");
            }

            MarkAsInvoked();
            asyncOperation(
                arg1, arg2, arg3, arg4, arg5,
                arg6, arg7, arg8, arg9, arg10,
                arg11, arg12, arg13, arg14, arg15,
                arg16, _operationToken);

            return _task;
        }

        public Task<TResult>
            Invoke<TArg1, TArg2, TArg3, TArg4, TArg5,
                   TArg6, TArg7, TArg8, TArg9, TArg10,
                   TArg11, TArg12, TArg13, TArg14, TArg15,
                   TArg16, TArg17>(
            Action<TArg1, TArg2, TArg3, TArg4, TArg5,
                   TArg6, TArg7, TArg8, TArg9, TArg10,
                   TArg11, TArg12, TArg13, TArg14, TArg15,
                   TArg16, TArg17, object> asyncOperation,
            TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5,
            TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10,
            TArg11 arg11, TArg12 arg12, TArg13 arg13, TArg14 arg14, TArg15 arg15,
            TArg16 arg16, TArg17 arg17)
        {
            if (asyncOperation == null)
            {
                throw new ArgumentNullException("asyncOperation");
            }

            MarkAsInvoked();
            asyncOperation(
                arg1, arg2, arg3, arg4, arg5,
                arg6, arg7, arg8, arg9, arg10,
                arg11, arg12, arg13, arg14, arg15,
                arg16, arg17, _operationToken);

            return _task;
        }

        public Task<TResult>
            Invoke<TArg1, TArg2, TArg3, TArg4, TArg5,
                   TArg6, TArg7, TArg8, TArg9, TArg10,
                   TArg11, TArg12, TArg13, TArg14, TArg15,
                   TArg16, TArg17, TArg18>(
            Action<TArg1, TArg2, TArg3, TArg4, TArg5,
                   TArg6, TArg7, TArg8, TArg9, TArg10,
                   TArg11, TArg12, TArg13, TArg14, TArg15,
                   TArg16, TArg17, TArg18, object> asyncOperation,
            TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5,
            TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10,
            TArg11 arg11, TArg12 arg12, TArg13 arg13, TArg14 arg14, TArg15 arg15,
            TArg16 arg16, TArg17 arg17, TArg18 arg18)
        {
            if (asyncOperation == null)
            {
                throw new ArgumentNullException("asyncOperation");
            }

            MarkAsInvoked();
            asyncOperation(
                arg1, arg2, arg3, arg4, arg5,
                arg6, arg7, arg8, arg9, arg10,
                arg11, arg12, arg13, arg14, arg15,
                arg16, arg17, arg18, _operationToken);

            return _task;
        }

        public Task<TResult>
            Invoke<TArg1, TArg2, TArg3, TArg4, TArg5,
                   TArg6, TArg7, TArg8, TArg9, TArg10,
                   TArg11, TArg12, TArg13, TArg14, TArg15,
                   TArg16, TArg17, TArg18, TArg19>(
            Action<TArg1, TArg2, TArg3, TArg4, TArg5,
                   TArg6, TArg7, TArg8, TArg9, TArg10,
                   TArg11, TArg12, TArg13, TArg14, TArg15,
                   TArg16, TArg17, TArg18, TArg19, object> asyncOperation,
            TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5,
            TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10,
            TArg11 arg11, TArg12 arg12, TArg13 arg13, TArg14 arg14, TArg15 arg15,
            TArg16 arg16, TArg17 arg17, TArg18 arg18, TArg19 arg19)
        {
            if (asyncOperation == null)
            {
                throw new ArgumentNullException("asyncOperation");
            }

            MarkAsInvoked();
            asyncOperation(
                arg1, arg2, arg3, arg4, arg5,
                arg6, arg7, arg8, arg9, arg10,
                arg11, arg12, arg13, arg14, arg15,
                arg16, arg17, arg18, arg19, _operationToken);

            return _task;
        }

        public Task<TResult>
            Invoke<TArg1, TArg2, TArg3, TArg4, TArg5,
                   TArg6, TArg7, TArg8, TArg9, TArg10,
                   TArg11, TArg12, TArg13, TArg14, TArg15,
                   TArg16, TArg17, TArg18, TArg19, TArg20>(
            Action<TArg1, TArg2, TArg3, TArg4, TArg5,
                   TArg6, TArg7, TArg8, TArg9, TArg10,
                   TArg11, TArg12, TArg13, TArg14, TArg15,
                   TArg16, TArg17, TArg18, TArg19, TArg20, object> asyncOperation,
            TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5,
            TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10,
            TArg11 arg11, TArg12 arg12, TArg13 arg13, TArg14 arg14, TArg15 arg15,
            TArg16 arg16, TArg17 arg17, TArg18 arg18, TArg19 arg19, TArg20 arg20)
        {
            if (asyncOperation == null)
            {
                throw new ArgumentNullException("asyncOperation");
            }

            MarkAsInvoked();
            asyncOperation(
                arg1, arg2, arg3, arg4, arg5,
                arg6, arg7, arg8, arg9, arg10,
                arg11, arg12, arg13, arg14, arg15,
                arg16, arg17, arg18, arg19, arg20,
                _operationToken);

            return _task;
        }

        public Task<TResult>
            Invoke<TArg1, TArg2, TArg3, TArg4, TArg5,
                   TArg6, TArg7, TArg8, TArg9, TArg10,
                   TArg11, TArg12, TArg13, TArg14, TArg15,
                   TArg16, TArg17, TArg18, TArg19, TArg20,
                   TArg21>(
            Action<TArg1, TArg2, TArg3, TArg4, TArg5,
                   TArg6, TArg7, TArg8, TArg9, TArg10,
                   TArg11, TArg12, TArg13, TArg14, TArg15,
                   TArg16, TArg17, TArg18, TArg19, TArg20,
                   TArg21, object> asyncOperation,
            TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5,
            TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10,
            TArg11 arg11, TArg12 arg12, TArg13 arg13, TArg14 arg14, TArg15 arg15,
            TArg16 arg16, TArg17 arg17, TArg18 arg18, TArg19 arg19, TArg20 arg20,
            TArg21 arg21)
        {
            if (asyncOperation == null)
            {
                throw new ArgumentNullException("asyncOperation");
            }

            MarkAsInvoked();
            asyncOperation(
                arg1, arg2, arg3, arg4, arg5,
                arg6, arg7, arg8, arg9, arg10,
                arg11, arg12, arg13, arg14, arg15,
                arg16, arg17, arg18, arg19, arg20,
                arg21, _operationToken);

            return _task;
        }

        public Task<TResult>
            Invoke<TArg1, TArg2, TArg3, TArg4, TArg5,
                   TArg6, TArg7, TArg8, TArg9, TArg10,
                   TArg11, TArg12, TArg13, TArg14, TArg15,
                   TArg16, TArg17, TArg18, TArg19, TArg20,
                   TArg21, TArg22>(
            Action<TArg1, TArg2, TArg3, TArg4, TArg5,
                   TArg6, TArg7, TArg8, TArg9, TArg10,
                   TArg11, TArg12, TArg13, TArg14, TArg15,
                   TArg16, TArg17, TArg18, TArg19, TArg20,
                   TArg21, TArg22, object> asyncOperation,
            TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5,
            TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10,
            TArg11 arg11, TArg12 arg12, TArg13 arg13, TArg14 arg14, TArg15 arg15,
            TArg16 arg16, TArg17 arg17, TArg18 arg18, TArg19 arg19, TArg20 arg20,
            TArg21 arg21, TArg22 arg22)
        {
            if (asyncOperation == null)
            {
                throw new ArgumentNullException("asyncOperation");
            }

            MarkAsInvoked();
            asyncOperation(
                arg1, arg2, arg3, arg4, arg5,
                arg6, arg7, arg8, arg9, arg10,
                arg11, arg12, arg13, arg14, arg15,
                arg16, arg17, arg18, arg19, arg20,
                arg21, arg22, _operationToken);

            return _task;
        }

        public Task<TResult>
            Invoke<TArg1, TArg2, TArg3, TArg4, TArg5,
                   TArg6, TArg7, TArg8, TArg9, TArg10,
                   TArg11, TArg12, TArg13, TArg14, TArg15,
                   TArg16, TArg17, TArg18, TArg19, TArg20,
                   TArg21, TArg22, TArg23>(
            Action<TArg1, TArg2, TArg3, TArg4, TArg5,
                   TArg6, TArg7, TArg8, TArg9, TArg10,
                   TArg11, TArg12, TArg13, TArg14, TArg15,
                   TArg16, TArg17, TArg18, TArg19, TArg20,
                   TArg21, TArg22, TArg23, object> asyncOperation,
            TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5,
            TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10,
            TArg11 arg11, TArg12 arg12, TArg13 arg13, TArg14 arg14, TArg15 arg15,
            TArg16 arg16, TArg17 arg17, TArg18 arg18, TArg19 arg19, TArg20 arg20,
            TArg21 arg21, TArg22 arg22, TArg23 arg23)
        {
            if (asyncOperation == null)
            {
                throw new ArgumentNullException("asyncOperation");
            }

            MarkAsInvoked();
            asyncOperation(
                arg1, arg2, arg3, arg4, arg5,
                arg6, arg7, arg8, arg9, arg10,
                arg11, arg12, arg13, arg14, arg15,
                arg16, arg17, arg18, arg19, arg20,
                arg21, arg22, arg23, _operationToken);

            return _task;
        }

        public Task<TResult>
            Invoke<TArg1, TArg2, TArg3, TArg4, TArg5,
                   TArg6, TArg7, TArg8, TArg9, TArg10,
                   TArg11, TArg12, TArg13, TArg14, TArg15,
                   TArg16, TArg17, TArg18, TArg19, TArg20,
                   TArg21, TArg22, TArg23, TArg24>(
            Action<TArg1, TArg2, TArg3, TArg4, TArg5,
                   TArg6, TArg7, TArg8, TArg9, TArg10,
                   TArg11, TArg12, TArg13, TArg14, TArg15,
                   TArg16, TArg17, TArg18, TArg19, TArg20,
                   TArg21, TArg22, TArg23, TArg24, object> asyncOperation,
            TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5,
            TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10,
            TArg11 arg11, TArg12 arg12, TArg13 arg13, TArg14 arg14, TArg15 arg15,
            TArg16 arg16, TArg17 arg17, TArg18 arg18, TArg19 arg19, TArg20 arg20,
            TArg21 arg21, TArg22 arg22, TArg23 arg23, TArg24 arg24)
        {
            if (asyncOperation == null)
            {
                throw new ArgumentNullException("asyncOperation");
            }

            MarkAsInvoked();
            asyncOperation(
                arg1, arg2, arg3, arg4, arg5,
                arg6, arg7, arg8, arg9, arg10,
                arg11, arg12, arg13, arg14, arg15,
                arg16, arg17, arg18, arg19, arg20,
                arg21, arg22, arg23, arg24, _operationToken);

            return _task;
        }

        public Task<TResult>
            Invoke<TArg1, TArg2, TArg3, TArg4, TArg5,
                   TArg6, TArg7, TArg8, TArg9, TArg10,
                   TArg11, TArg12, TArg13, TArg14, TArg15,
                   TArg16, TArg17, TArg18, TArg19, TArg20,
                   TArg21, TArg22, TArg23, TArg24, TArg25>(
            Action<TArg1, TArg2, TArg3, TArg4, TArg5,
                   TArg6, TArg7, TArg8, TArg9, TArg10,
                   TArg11, TArg12, TArg13, TArg14, TArg15,
                   TArg16, TArg17, TArg18, TArg19, TArg20,
                   TArg21, TArg22, TArg23, TArg24, TArg25,
                   object> asyncOperation,
            TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5,
            TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10,
            TArg11 arg11, TArg12 arg12, TArg13 arg13, TArg14 arg14, TArg15 arg15,
            TArg16 arg16, TArg17 arg17, TArg18 arg18, TArg19 arg19, TArg20 arg20,
            TArg21 arg21, TArg22 arg22, TArg23 arg23, TArg24 arg24, TArg25 arg25)
        {
            if (asyncOperation == null)
            {
                throw new ArgumentNullException("asyncOperation");
            }

            MarkAsInvoked();
            asyncOperation(
                arg1, arg2, arg3, arg4, arg5,
                arg6, arg7, arg8, arg9, arg10,
                arg11, arg12, arg13, arg14, arg15,
                arg16, arg17, arg18, arg19, arg20,
                arg21, arg22, arg23, arg24, arg25,
                _operationToken);

            return _task;
        }

        public Task<TResult>
            Invoke<TArg1, TArg2, TArg3, TArg4, TArg5,
                   TArg6, TArg7, TArg8, TArg9, TArg10,
                   TArg11, TArg12, TArg13, TArg14, TArg15,
                   TArg16, TArg17, TArg18, TArg19, TArg20,
                   TArg21, TArg22, TArg23, TArg24, TArg25,
                   TArg26>(
            Action<TArg1, TArg2, TArg3, TArg4, TArg5,
                   TArg6, TArg7, TArg8, TArg9, TArg10,
                   TArg11, TArg12, TArg13, TArg14, TArg15,
                   TArg16, TArg17, TArg18, TArg19, TArg20,
                   TArg21, TArg22, TArg23, TArg24, TArg25,
                   TArg26, object> asyncOperation,
            TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5,
            TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10,
            TArg11 arg11, TArg12 arg12, TArg13 arg13, TArg14 arg14, TArg15 arg15,
            TArg16 arg16, TArg17 arg17, TArg18 arg18, TArg19 arg19, TArg20 arg20,
            TArg21 arg21, TArg22 arg22, TArg23 arg23, TArg24 arg24, TArg25 arg25,
            TArg26 arg26)
        {
            if (asyncOperation == null)
            {
                throw new ArgumentNullException("asyncOperation");
            }

            MarkAsInvoked();
            asyncOperation(
                arg1, arg2, arg3, arg4, arg5,
                arg6, arg7, arg8, arg9, arg10,
                arg11, arg12, arg13, arg14, arg15,
                arg16, arg17, arg18, arg19, arg20,
                arg21, arg22, arg23, arg24, arg25,
                arg26, _operationToken);

            return _task;
        }

        public Task<TResult>
            Invoke<TArg1, TArg2, TArg3, TArg4, TArg5,
                   TArg6, TArg7, TArg8, TArg9, TArg10,
                   TArg11, TArg12, TArg13, TArg14, TArg15,
                   TArg16, TArg17, TArg18, TArg19, TArg20,
                   TArg21, TArg22, TArg23, TArg24, TArg25,
                   TArg26, TArg27>(
            Action<TArg1, TArg2, TArg3, TArg4, TArg5,
                   TArg6, TArg7, TArg8, TArg9, TArg10,
                   TArg11, TArg12, TArg13, TArg14, TArg15,
                   TArg16, TArg17, TArg18, TArg19, TArg20,
                   TArg21, TArg22, TArg23, TArg24, TArg25,
                   TArg26, TArg27, object> asyncOperation,
            TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5,
            TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10,
            TArg11 arg11, TArg12 arg12, TArg13 arg13, TArg14 arg14, TArg15 arg15,
            TArg16 arg16, TArg17 arg17, TArg18 arg18, TArg19 arg19, TArg20 arg20,
            TArg21 arg21, TArg22 arg22, TArg23 arg23, TArg24 arg24, TArg25 arg25,
            TArg26 arg26, TArg27 arg27)
        {
            if (asyncOperation == null)
            {
                throw new ArgumentNullException("asyncOperation");
            }

            MarkAsInvoked();
            asyncOperation(
                arg1, arg2, arg3, arg4, arg5,
                arg6, arg7, arg8, arg9, arg10,
                arg11, arg12, arg13, arg14, arg15,
                arg16, arg17, arg18, arg19, arg20,
                arg21, arg22, arg23, arg24, arg25,
                arg26, arg27, _operationToken);

            return _task;
        }

        public Task<TResult>
            Invoke<TArg1, TArg2, TArg3, TArg4, TArg5,
                   TArg6, TArg7, TArg8, TArg9, TArg10,
                   TArg11, TArg12, TArg13, TArg14, TArg15,
                   TArg16, TArg17, TArg18, TArg19, TArg20,
                   TArg21, TArg22, TArg23, TArg24, TArg25,
                   TArg26, TArg27, TArg28>(
            Action<TArg1, TArg2, TArg3, TArg4, TArg5,
                   TArg6, TArg7, TArg8, TArg9, TArg10,
                   TArg11, TArg12, TArg13, TArg14, TArg15,
                   TArg16, TArg17, TArg18, TArg19, TArg20,
                   TArg21, TArg22, TArg23, TArg24, TArg25,
                   TArg26, TArg27, TArg28, object> asyncOperation,
            TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5,
            TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10,
            TArg11 arg11, TArg12 arg12, TArg13 arg13, TArg14 arg14, TArg15 arg15,
            TArg16 arg16, TArg17 arg17, TArg18 arg18, TArg19 arg19, TArg20 arg20,
            TArg21 arg21, TArg22 arg22, TArg23 arg23, TArg24 arg24, TArg25 arg25,
            TArg26 arg26, TArg27 arg27, TArg28 arg28)
        {
            if (asyncOperation == null)
            {
                throw new ArgumentNullException("asyncOperation");
            }

            MarkAsInvoked();
            asyncOperation(
                arg1, arg2, arg3, arg4, arg5,
                arg6, arg7, arg8, arg9, arg10,
                arg11, arg12, arg13, arg14, arg15,
                arg16, arg17, arg18, arg19, arg20,
                arg21, arg22, arg23, arg24, arg25,
                arg26, arg27, arg28, _operationToken);

            return _task;
        }

        public Task<TResult>
            Invoke<TArg1, TArg2, TArg3, TArg4, TArg5,
                   TArg6, TArg7, TArg8, TArg9, TArg10,
                   TArg11, TArg12, TArg13, TArg14, TArg15,
                   TArg16, TArg17, TArg18, TArg19, TArg20,
                   TArg21, TArg22, TArg23, TArg24, TArg25,
                   TArg26, TArg27, TArg28, TArg29>(
            Action<TArg1, TArg2, TArg3, TArg4, TArg5,
                   TArg6, TArg7, TArg8, TArg9, TArg10,
                   TArg11, TArg12, TArg13, TArg14, TArg15,
                   TArg16, TArg17, TArg18, TArg19, TArg20,
                   TArg21, TArg22, TArg23, TArg24, TArg25,
                   TArg26, TArg27, TArg28, TArg29, object> asyncOperation,
            TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5,
            TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10,
            TArg11 arg11, TArg12 arg12, TArg13 arg13, TArg14 arg14, TArg15 arg15,
            TArg16 arg16, TArg17 arg17, TArg18 arg18, TArg19 arg19, TArg20 arg20,
            TArg21 arg21, TArg22 arg22, TArg23 arg23, TArg24 arg24, TArg25 arg25,
            TArg26 arg26, TArg27 arg27, TArg28 arg28, TArg29 arg29)
        {
            if (asyncOperation == null)
            {
                throw new ArgumentNullException("asyncOperation");
            }

            MarkAsInvoked();
            asyncOperation(
                arg1, arg2, arg3, arg4, arg5,
                arg6, arg7, arg8, arg9, arg10,
                arg11, arg12, arg13, arg14, arg15,
                arg16, arg17, arg18, arg19, arg20,
                arg21, arg22, arg23, arg24, arg25,
                arg26, arg27, arg28, arg29, _operationToken);

            return _task;
        }

        public Task<TResult>
            Invoke<TArg1, TArg2, TArg3, TArg4, TArg5,
                   TArg6, TArg7, TArg8, TArg9, TArg10,
                   TArg11, TArg12, TArg13, TArg14, TArg15,
                   TArg16, TArg17, TArg18, TArg19, TArg20,
                   TArg21, TArg22, TArg23, TArg24, TArg25,
                   TArg26, TArg27, TArg28, TArg29, TArg30>(
            Action<TArg1, TArg2, TArg3, TArg4, TArg5,
                   TArg6, TArg7, TArg8, TArg9, TArg10,
                   TArg11, TArg12, TArg13, TArg14, TArg15,
                   TArg16, TArg17, TArg18, TArg19, TArg20,
                   TArg21, TArg22, TArg23, TArg24, TArg25,
                   TArg26, TArg27, TArg28, TArg29, TArg30,
                   object> asyncOperation,
            TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5,
            TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10,
            TArg11 arg11, TArg12 arg12, TArg13 arg13, TArg14 arg14, TArg15 arg15,
            TArg16 arg16, TArg17 arg17, TArg18 arg18, TArg19 arg19, TArg20 arg20,
            TArg21 arg21, TArg22 arg22, TArg23 arg23, TArg24 arg24, TArg25 arg25,
            TArg26 arg26, TArg27 arg27, TArg28 arg28, TArg29 arg29, TArg30 arg30)
        {
            if (asyncOperation == null)
            {
                throw new ArgumentNullException("asyncOperation");
            }

            MarkAsInvoked();
            asyncOperation(
                arg1, arg2, arg3, arg4, arg5,
                arg6, arg7, arg8, arg9, arg10,
                arg11, arg12, arg13, arg14, arg15,
                arg16, arg17, arg18, arg19, arg20,
                arg21, arg22, arg23, arg24, arg25,
                arg26, arg27, arg28, arg29, arg30,
                _operationToken);

            return _task;
        }

        public Task<TResult>
            Invoke<TArg1, TArg2, TArg3, TArg4, TArg5,
                   TArg6, TArg7, TArg8, TArg9, TArg10,
                   TArg11, TArg12, TArg13, TArg14, TArg15,
                   TArg16, TArg17, TArg18, TArg19, TArg20,
                   TArg21, TArg22, TArg23, TArg24, TArg25,
                   TArg26, TArg27, TArg28, TArg29, TArg30,
                   TArg31>(
            Action<TArg1, TArg2, TArg3, TArg4, TArg5,
                   TArg6, TArg7, TArg8, TArg9, TArg10,
                   TArg11, TArg12, TArg13, TArg14, TArg15,
                   TArg16, TArg17, TArg18, TArg19, TArg20,
                   TArg21, TArg22, TArg23, TArg24, TArg25,
                   TArg26, TArg27, TArg28, TArg29, TArg30,
                   TArg31, object> asyncOperation,
            TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5,
            TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10,
            TArg11 arg11, TArg12 arg12, TArg13 arg13, TArg14 arg14, TArg15 arg15,
            TArg16 arg16, TArg17 arg17, TArg18 arg18, TArg19 arg19, TArg20 arg20,
            TArg21 arg21, TArg22 arg22, TArg23 arg23, TArg24 arg24, TArg25 arg25,
            TArg26 arg26, TArg27 arg27, TArg28 arg28, TArg29 arg29, TArg30 arg30,
            TArg31 arg31)
        {
            if (asyncOperation == null)
            {
                throw new ArgumentNullException("asyncOperation");
            }

            MarkAsInvoked();
            asyncOperation(
                arg1, arg2, arg3, arg4, arg5,
                arg6, arg7, arg8, arg9, arg10,
                arg11, arg12, arg13, arg14, arg15,
                arg16, arg17, arg18, arg19, arg20,
                arg21, arg22, arg23, arg24, arg25,
                arg26, arg27, arg28, arg29, arg30,
                arg31, _operationToken);

            return _task;
        }

        public Task<TResult>
           Invoke<TArg1, TArg2, TArg3, TArg4, TArg5,
                  TArg6, TArg7, TArg8, TArg9, TArg10,
                  TArg11, TArg12, TArg13, TArg14, TArg15,
                  TArg16, TArg17, TArg18, TArg19, TArg20,
                  TArg21, TArg22, TArg23, TArg24, TArg25,
                  TArg26, TArg27, TArg28, TArg29, TArg30,
                  TArg31, TArg32, TArg33, TArg34, TArg35, TArg36, TArg37>(
           Action<TArg1, TArg2, TArg3, TArg4, TArg5,
                  TArg6, TArg7, TArg8, TArg9, TArg10,
                  TArg11, TArg12, TArg13, TArg14, TArg15,
                  TArg16, TArg17, TArg18, TArg19, TArg20,
                  TArg21, TArg22, TArg23, TArg24, TArg25,
                  TArg26, TArg27, TArg28, TArg29, TArg30,
                  TArg31, TArg32, TArg33, TArg34, TArg35, TArg36, TArg37, object> asyncOperation,
           TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5,
           TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10,
           TArg11 arg11, TArg12 arg12, TArg13 arg13, TArg14 arg14, TArg15 arg15,
           TArg16 arg16, TArg17 arg17, TArg18 arg18, TArg19 arg19, TArg20 arg20,
           TArg21 arg21, TArg22 arg22, TArg23 arg23, TArg24 arg24, TArg25 arg25,
           TArg26 arg26, TArg27 arg27, TArg28 arg28, TArg29 arg29, TArg30 arg30,
           TArg31 arg31, TArg32 arg32, TArg33 arg33, TArg34 arg34, TArg35 arg35,
           TArg36 arg36, TArg37 arg37)
        {
            if (asyncOperation == null)
            {
                throw new ArgumentNullException("asyncOperation");
            }

            MarkAsInvoked();
            asyncOperation(
                arg1, arg2, arg3, arg4, arg5,
                arg6, arg7, arg8, arg9, arg10,
                arg11, arg12, arg13, arg14, arg15,
                arg16, arg17, arg18, arg19, arg20,
                arg21, arg22, arg23, arg24, arg25,
                arg26, arg27, arg28, arg29, arg30,
                arg31, arg32, arg33, arg34, arg35,
                arg36, arg37, _operationToken);

            return _task;
        }

        public Task<TResult>
           Invoke<TArg1, TArg2, TArg3, TArg4, TArg5,
                  TArg6, TArg7, TArg8, TArg9, TArg10,
                  TArg11, TArg12, TArg13, TArg14, TArg15,
                  TArg16, TArg17, TArg18, TArg19, TArg20,
                  TArg21, TArg22, TArg23, TArg24, TArg25,
                  TArg26, TArg27, TArg28, TArg29, TArg30,
                  TArg31, TArg32, TArg33, TArg34, TArg35, TArg36, TArg37, TArg38>(
           Action<TArg1, TArg2, TArg3, TArg4, TArg5,
                  TArg6, TArg7, TArg8, TArg9, TArg10,
                  TArg11, TArg12, TArg13, TArg14, TArg15,
                  TArg16, TArg17, TArg18, TArg19, TArg20,
                  TArg21, TArg22, TArg23, TArg24, TArg25,
                  TArg26, TArg27, TArg28, TArg29, TArg30,
                  TArg31, TArg32, TArg33, TArg34, TArg35, TArg36, TArg37, TArg38, object> asyncOperation,
           TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5,
           TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10,
           TArg11 arg11, TArg12 arg12, TArg13 arg13, TArg14 arg14, TArg15 arg15,
           TArg16 arg16, TArg17 arg17, TArg18 arg18, TArg19 arg19, TArg20 arg20,
           TArg21 arg21, TArg22 arg22, TArg23 arg23, TArg24 arg24, TArg25 arg25,
           TArg26 arg26, TArg27 arg27, TArg28 arg28, TArg29 arg29, TArg30 arg30,
           TArg31 arg31, TArg32 arg32, TArg33 arg33, TArg34 arg34, TArg35 arg35,
           TArg36 arg36, TArg37 arg37, TArg38 arg38)
        {
            if (asyncOperation == null)
            {
                throw new ArgumentNullException("asyncOperation");
            }

            MarkAsInvoked();
            asyncOperation(
                arg1, arg2, arg3, arg4, arg5,
                arg6, arg7, arg8, arg9, arg10,
                arg11, arg12, arg13, arg14, arg15,
                arg16, arg17, arg18, arg19, arg20,
                arg21, arg22, arg23, arg24, arg25,
                arg26, arg27, arg28, arg29, arg30,
                arg31, arg32, arg33, arg34, arg35,
                arg36, arg37, arg38, _operationToken);

            return _task;
        }

        public Task<TResult>
          Invoke<TArg1, TArg2, TArg3, TArg4, TArg5,
                 TArg6, TArg7, TArg8, TArg9, TArg10,
                 TArg11, TArg12, TArg13, TArg14, TArg15,
                 TArg16, TArg17, TArg18, TArg19, TArg20,
                 TArg21, TArg22, TArg23, TArg24, TArg25,
                 TArg26, TArg27, TArg28, TArg29, TArg30,
                 TArg31, TArg32, TArg33, TArg34, TArg35, TArg36, TArg37, TArg38, TArg39>(
          Action<TArg1, TArg2, TArg3, TArg4, TArg5,
              TArg6, TArg7, TArg8, TArg9, TArg10,
              TArg11, TArg12, TArg13, TArg14, TArg15,
              TArg16, TArg17, TArg18, TArg19, TArg20,
              TArg21, TArg22, TArg23, TArg24, TArg25,
              TArg26, TArg27, TArg28, TArg29, TArg30,
              TArg31, TArg32, TArg33, TArg34, TArg35, TArg36, TArg37, TArg38, object> asyncOperation,
          TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5,
          TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10,
          TArg11 arg11, TArg12 arg12, TArg13 arg13, TArg14 arg14, TArg15 arg15,
          TArg16 arg16, TArg17 arg17, TArg18 arg18, TArg19 arg19, TArg20 arg20,
          TArg21 arg21, TArg22 arg22, TArg23 arg23, TArg24 arg24, TArg25 arg25,
          TArg26 arg26, TArg27 arg27, TArg28 arg28, TArg29 arg29, TArg30 arg30,
          TArg31 arg31, TArg32 arg32, TArg33 arg33, TArg34 arg34, TArg35 arg35,
          TArg36 arg36, TArg37 arg37, TArg38 arg38, TArg39 arg39)
        {
            if (asyncOperation == null)
            {
                throw new ArgumentNullException("asyncOperation");
            }

            MarkAsInvoked();
            asyncOperation(
                arg1, arg2, arg3, arg4, arg5,
                arg6, arg7, arg8, arg9, arg10,
                arg11, arg12, arg13, arg14, arg15,
                arg16, arg17, arg18, arg19, arg20,
                arg21, arg22, arg23, arg24, arg25,
                arg26, arg27, arg28, arg29, arg30,
                arg31, arg32, arg33, arg34, arg35,
                arg36, arg37, arg38, _operationToken);

            return _task;
        }

        private void MarkAsInvoked()
        {
            if (_isInvoked)
            {
                throw new InvalidOperationException(string.Format("One instance of '{0}' can't be invoked twice.",
                    typeof(TaskFromAsyncEventPatternInvocation<,,>).Name));
            }

            _isInvoked = true;
        }
    }
}
