using Elastic.Clients.Elasticsearch.QueryDsl;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PermissionManager.Core.Data.Repositories.Interfaces
{
    public interface IRepositoryRead<T> where T :class
    {
        Task<IEnumerable<T>> SearchAsync<TValue>(Expression<Func<T, TValue>?>? fieldExpression = null, object? value = null, int? size = 10);

        Task<T?> GetById(int id);

        public Task<long> GetTotalAsync();

        public Task<Boolean> CreateBulkAsync(List<T> documents);

        public Task<Boolean> CreateOrUpdateAsync(T document);

        public Task<Boolean> DeleteAsync(T document);

        public string GetIndexName();
    }
}
