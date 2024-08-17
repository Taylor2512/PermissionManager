using Elastic.Clients.Elasticsearch.QueryDsl;

namespace PermissionManager.Core.Data.Repositories.Interfaces
{
    public interface IElasticsearchRepository<T> where T : class
    {
        public Task<IEnumerable<T>> SearchAsync(
            Query query = null,
            int size = 10
            );

        public Task<long> GetTotalAsync();

        public Task<Boolean> CreateBulkAsync(List<T> documents);

        public Task<Boolean> CreateOrUpdateAsync(T document);

        public Task<Boolean> DeleteAsync(T document);

        public string GetIndexName();
    }
}