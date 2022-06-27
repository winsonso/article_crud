namespace Article.Core
{
	public interface IArticleServices
    {
        List<ArticleEntity> GetArticles(string? searchQuery);
        ArticleEntity GetArticle(Guid id);
        ArticleEntity AddArticle(ArticleEntity article);
        void DeleteArticle(Guid id);
        ArticleEntity UpdateArticle(Guid id, UpsertArticle updateArticle);
    }
}
