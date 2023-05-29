using System.Linq.Expressions;
using Azure.Data.Tables;
using Azure;
using Microsoft.Extensions.Configuration;
using BlobStorage.Interface;
namespace BlobStorage
{

    public class TableStorage<T> : ITableStorage<T> where T : class, ITableEntity, new()
    {
        private readonly TableClient _table;
        private readonly IConfiguration _configuration;
        public TableStorage(IConfiguration configuration)
        {
            _configuration = configuration;
            _table = ConectionStorage().GetAwaiter().GetResult();
        }
        public async Task<Response> InsertData(T entity)
        {
            return await _table.AddEntityAsync(entity);
        }
        public async Task UpdateData(T entity)
        {
            await _table.UpsertEntityAsync(entity);
        }
        public async Task<List<T>> QueryData(Expression<Func<T, bool>> expression)
        {
            List<T> result = new List<T>();
            AsyncPageable<T> queryResultsMaxPerPage = _table.QueryAsync<T>(expression, maxPerPage: 10);
            await foreach (var page in queryResultsMaxPerPage.AsPages())
            {
                result.AddRange(page.Values);
            }
            return result;
        }

        private async Task<TableClient> ConectionStorage()
        {
            var serviceClient = new TableServiceClient(_configuration["Storage:conectionString"]);
            var tableClient = serviceClient.GetTableClient(typeof(T).Name);
            return tableClient;
        }
    }
}