using System.Linq;
using System.Threading.Tasks;
using SHARED.Models.Pagination;

namespace SHARED.Models.Interfaces
{
    public interface IReturnGridData
    {
        IQueryable GetGridData(params object[] p);
    }

    public interface IReturnGridData<in TType>
    {
        IQueryable GetGridData(TType type);
    }

    public interface IReturnGridPagedData<in TType>
    {
        IQueryable GetGridData(TType type, SortOrder order, Page page);
    }

    public interface IReturnGridData<in TEnumType, in TSearchParameter> : IReturnGridData<TEnumType>
        where TEnumType : struct
        where TSearchParameter : class
    {
        IQueryable GetGridData(TEnumType type, TSearchParameter searchParameter);
    }

    public interface IReturnGridDataAsync
    {
        Task<IQueryable> GetGridData(params object[] p);
    }

    public interface IReturnGridDataAsync<in TType>
    {
        Task<IQueryable> GetGridData(TType type);
    }

    public interface IReturnGridPagedDataAsync<in TType>
    {
        Task<IQueryable> GetGridData(TType type, SortOrder order, Page page);
    }
}