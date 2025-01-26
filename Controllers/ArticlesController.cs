using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuokkaLabAPI.Data;
using QuokkaLabAPI.Models;
using QuokkaLabAPI.Models.DTOs.Requests;

namespace QuokkaLabAPI.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ArticlesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("all-articles")]
        public async Task<ActionResult<IEnumerable<BlogArticle>>> GetArticles()
        {
            return await _context.BlogArticles.ToListAsync();
        }

        [HttpGet("{id:int}")]
        [Route("article/id={id}")]
        public async Task<ActionResult<BlogArticle>> GetArticle(int id)
        {
            var article = await _context.BlogArticles.FindAsync(id);

            if (article == null)
            {
                return NotFound();
            }

            return article;
        }

        [HttpPost]
        [Route("CreateArticle")]
        [Authorize]
        public async Task<ActionResult<BlogArticle>> CreateArticle([FromBody] ArticleDto article)
        {
            var blogArticle = new BlogArticle()
            {
                Title = article.Title,
                Content = article.Content,
                AuthorId = article.AuthorId,
                PublishedDate = article.PublishedDate,
            };
            _context.BlogArticles.Add(blogArticle);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetArticle), new { id = blogArticle.Id }, blogArticle);
        }

        [HttpPut("{id}")]
        [Route("UpdateArticle/id={id}")]
        [Authorize]
        public async Task<IActionResult> UpdateArticle(int id, [FromBody] BlogArticle article)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            _context.Entry(article).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.BlogArticles.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Route("DeleteArticle/id={id}")]
        [Authorize]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var article = await _context.BlogArticles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            _context.BlogArticles.Remove(article);
            await _context.SaveChangesAsync();

            return new ObjectResult(new { Message = "Article deleted", currentDate = DateTime.UtcNow });
        }

    }
}
