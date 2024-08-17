using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;

using Microsoft.Extensions.Logging;

using PermissionManager.Core.Data.Repositories.Interfaces;

namespace PermissionManager.Core.Data.Repositories
{
    public class ElasticsearchRepository<T> : IElasticsearchRepository<T> where T : class
    {
        public string IndexName;
        public readonly ElasticsearchClient _client;
        private readonly ILogger<ElasticsearchRepository<T>> _logger;

        public ElasticsearchRepository(ElasticsearchClient client, ILogger<ElasticsearchRepository<T>> logger)
        {
            _client = client;
            _logger = logger;
            IndexName = typeof(T).Name.ToLower() + "s";
        }

        public string GetIndexName()
        {
            return IndexName;
        }

        // ? handle Paginate search
        public async Task<IEnumerable<T>> SearchAsync(Query query = null, int size = 10)
        {
            try
            {
                SearchRequest searchRequest = new SearchRequest(IndexName)
                {
                    Size = size,
                    Query = query
                };
                SearchResponse<T> SearchAsync = await _client.SearchAsync<T>(searchRequest);
                return SearchAsync.Documents;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all documents."); // Log error with exception
                throw;
            }
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