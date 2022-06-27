using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Article.Core
{
	public class ArticleEntity
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Author { get; set; }
		public DateTime PublicationDate { get; set; }
		public string Body { get; set; }
	}

	public class UpsertArticle
	{
		[Required(ErrorMessage = "Title is Required")]
		[StringLength(50)]
		public string Title { get; set; }
		[Required(ErrorMessage = "Author is Required")]
		[StringLength(50)]
		public string Author { get; set; }
		[Required(ErrorMessage = "Body is Required")]
		[StringLength(1000)]
		public string Body { get; set; }
	}
}