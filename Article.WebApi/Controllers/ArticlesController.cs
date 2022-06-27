using Article.Core;
using Microsoft.AspNetCore.Mvc;

namespace Article.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleServices _articleServices;
        public ArticlesController(IArticleServices articleServices)
        {
            _articleServices = articleServices;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string? searchQuery = null)
        {
            return Ok(_articleServices.GetArticles(searchQuery));
        }

        [HttpGet("{id}", Name = "GetBook")]
        public IActionResult Get([FromRoute] Guid id)
        {
            var article = _articleServices.GetArticle(id);
            if (article == null)
                return NotFound();

            return Ok(article);
        }

        [HttpPost]
        public IActionResult Post([FromBody] UpsertArticle article)
        {
            var postArticle = new ArticleEntity();
            postArticle.Title = article.Title;
            postArticle.Author = article.Author;
            postArticle.Body = article.Body;
            _articleServices.AddArticle(postArticle);
            return CreatedAtRoute("GetBook", new { id = postArticle.Id }, postArticle);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            try
            {
                _articleServices.DeleteArticle(id);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Not Found")
                    return NotFound();
            }
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] Guid id, [FromBody] UpsertArticle updateArticle)
        {
            var article = _articleServices.UpdateArticle(id, updateArticle);
            if (article == null)
                return NotFound();

            return Ok(article);
        }
    }
}
