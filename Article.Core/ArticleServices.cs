using MongoDB.Driver;

namespace Article.Core
{
	public class ArticleServices : IArticleServices
    {
        private readonly IMongoCollection<ArticleEntity> _articles;

        public ArticleServices(IDbClient dbClient)
        {
            _articles = dbClient.GetBooksCollection();
        }

        public ArticleEntity AddArticle(ArticleEntity article)
        {
            article.Id = Guid.NewGuid();
            article.PublicationDate = DateTime.Now;
            _articles.InsertOne(article);
            return article;
        }

        public void DeleteArticle(Guid id)
        {
            var getArticle = GetArticle(id);
            if (getArticle == null)
            {
                throw new Exception("Not Found");
            }
            _articles.DeleteOne(a => a.Id == id);
        }

        public ArticleEntity GetArticle(Guid id) => _articles.Find(a => a.Id == id).FirstOrDefault();


        public List<ArticleEntity> GetArticles(string? searchQuery)
        {
            if(searchQuery != null)
			{
                searchQuery = searchQuery.ToLower();
                return _articles.Find(a => a.Title.ToLower().Contains(searchQuery)  || 
                                           a.Author.ToLower().Contains(searchQuery) || 
                                           a.Body.ToLower().Contains(searchQuery)).ToList();
            }
            return _articles.Find(a => true).ToList();
        }

        public ArticleEntity UpdateArticle(Guid id, UpsertArticle updateArticle)
        {
            var article = GetArticle(id);
            if (article != null)
            {
                article.Title = updateArticle.Title;
                article.Author = updateArticle.Author;
                article.Body = updateArticle.Body;
                _articles.ReplaceOne(b => b.Id == id, article);
            }
            return article;
        }
    }
}
