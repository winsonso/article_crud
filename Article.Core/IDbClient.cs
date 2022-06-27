using MongoDB.Driver;

namespace Article.Core
{
	public interface IDbClient
    {
        IMongoCollection<ArticleEntity> GetBooksCollection();
    }
}
