using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Article.Core
{
	public class DbClient : IDbClient
    {
        private readonly IMongoCollection<ArticleEntity> _articles;

        public DbClient(IOptions<ArticleDbConfig> articlesDbConfig)
        {
            var client = new MongoClient(articlesDbConfig.Value.Connection_String);
            var database = client.GetDatabase(articlesDbConfig.Value.Database_Name);
            _articles = database.GetCollection<ArticleEntity>(articlesDbConfig.Value.Articles_Collection_Name);
        }

        public IMongoCollection<ArticleEntity> GetBooksCollection() => _articles;
    }
}
