using System;
using System.Threading.Tasks;
using SHARED.Models;

namespace SHARED.Web.Core
{
    public interface IBaseViewModelService<T>:IDisposable where T : class
    {    
        /// <summary>
        /// Добавляет новую  или получает сущность
        /// </summary>
        T GetOrCreate(int id = 0);
        /// <summary>
        /// Добавляет новую, либо обновляет текущую сущность
        /// </summary>
        Task<OperationResult> Save(T model);

        Task<OperationResult> Delete(int id);
    }

    public interface IBaseViewModelService<T, in TFilterType> : IBaseViewModelService<T> where T : class
    {
        /// <summary>
        /// Добавляет новую  или получает сущность
        /// </summary>
        T GetOrCreate(TFilterType filter, int id = 0);
        OperationResult Delete(TFilterType filter, int id);
    }

    public abstract class BaseViewModelService<T> : IBaseViewModelService<T> where T : class, new()
    { 
        public virtual void Dispose()
        {
            // empty
        }

        public abstract T GetOrCreate(int id = 0);
        public abstract Task<OperationResult> Save(T model);
        public abstract Task<OperationResult> Delete(int id);
    }
    
    public abstract class BaseViewModelService<T, TFilterType> : BaseViewModelService<T>, IBaseViewModelService<T, TFilterType>
        where T : class, new()
    {
        public override T GetOrCreate(int id = 0)
        {
            throw new NotImplementedException();
        }
        public abstract T GetOrCreate(TFilterType filter, int id = 0);

        public override Task<OperationResult> Delete(int id = 0)
        {
            throw new NotImplementedException();
        }
        public abstract OperationResult Delete(TFilterType filter, int id);
    }

}