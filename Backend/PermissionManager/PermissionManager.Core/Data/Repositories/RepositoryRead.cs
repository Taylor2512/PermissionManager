using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;

using Microsoft.Extensions.Logging;

using PermissionManager.Core.Data.Repositories.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PermissionManager.Core.Data.Repositories
{
    public class RepositoryRead<T> : IRepositoryRead<T> where T : class
    {
        public string IndexName;
        protected readonly ElasticsearchClient _client;
        private readonly ILogger<ElasticsearchRepository<T>> _logger;

        public RepositoryRead(ElasticsearchClient client, ILogger<ElasticsearchRepository<T>> logger)
        {
            _client = client;
            _logger = logger;
            IndexName = typeof(T).Name.ToLower() + "s";
        }

        public string GetIndexName()
        {
            return IndexName;
        }

        public async Task<T?> GetById(int id)
        {

            var documento = await _client.GetAsync<T>(id, idx => idx.Index(IndexName));

            if (!documento.IsValidResponse)
            {
                _logger.LogError(documento.DebugInformation); // Log error
                throw new Exception(documento.DebugInformation);
            }
            return documento.Source;
        }
        public async Task<IEnumerable<T>> SearchAsync<TValue>(Expression<Func<T, TValue>?>? fieldExpression = null, object? value = null, int? size = 10)
        {
            try
            {
                if (size == 0 || size == null)
                {
                    var searchResponse = await _client.SearchAsync<T>(s => querySearch(fieldExpression, value, s));
                    return searchResponse.Documents;

                }
                else
                {
                    var searchResponse = await _client.SearchAsync<T>(s => querySearch(fieldExpression, value, s).Size(size));
                    return searchResponse.Documents;

                }
                // Construir la consulta

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while searching documents.");
                throw;
            }
        }

        private SearchRequestDescriptor<T> querySearch<TValue>(Expression<Func<T, TValue>>? fieldExpression, object value, SearchRequestDescriptor<T> s)
        {
            return s.Index(IndexName).Query(q =>
            {
                if (value != null)
                {
                    q.Match(m =>
                    {
                        if (fieldExpression != null)
                        { m.Field(fieldExpression).Query(value); }
                    });
                }
            });
        }

        public async Task<bool> CreateBulkAsync(List<T> documents)
        {
            try
            {
                BulkResponse bulk = await _client.BulkAsync(b => b
                            .Index(IndexName)
                            .IndexMany(documents)
                        );

                if (!bulk.IsValidResponse)
                {
                    _logger.LogError(bulk.DebugInformation); // Log error
                    throw new Exception(bulk.DebugInformation);
                }

                _logger.LogInformation("bulk: " + bulk.Items.Count()); // Log information

                return bulk.IsSuccess();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating bulk documents."); // Log error with exception
                throw;
            }
        }

        public async Task<bool> CreateOrUpdateAsync(T document)
        {
            try
            {
                IndexResponse indexResponse = await _client.IndexAsync(document, idx => idx.Index(IndexName));

                if (!indexResponse.IsValidResponse)
                {
                    _logger.LogError(indexResponse.DebugInformation); // Log error
                    throw new Exception(indexResponse.DebugInformation);
                }

                return indexResponse.IsSuccess();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while Create Or Update documents."); // Log error with exception
                throw;
            }
        }

        public async Task<bool> DeleteAsync(T document)
        {
            try
            {
                DeleteResponse deleteResponse = await _client.DeleteAsync<T>(document, idx => idx.Index(IndexName));

                if (!deleteResponse.IsValidResponse)
                {
                    _logger.LogError(deleteResponse.DebugInformation); // Log error
                    throw new Exception(deleteResponse.DebugInformation);
                }

                return deleteResponse.IsSuccess();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting documents."); // Log error with exception
                throw;
            }
        }

        public async Task<long> GetTotalAsync()
        {
            try
            {
                CountResponse count = await _client.CountAsync<T>(s => s.Indices(IndexName));

                return count.Count;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting total documents."); // Log error with exception
                throw;
            }
        }
    }
}
