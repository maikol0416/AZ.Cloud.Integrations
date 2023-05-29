using System.Linq.Expressions;
using Azure.Data.Tables;
using Azure;

namespace BlobStorage.Interface
{
    public interface ITableStorage<T> where T : class, ITableEntity, new()
    {
        Task<Response> InsertData(T entity); Task UpdateData(T entity);
        Task<List<T>> QueryData(Expression<Func<T, bool>> expression);
    }
}
