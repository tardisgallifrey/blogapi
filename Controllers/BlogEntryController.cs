using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using blog;

namespace blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogEntryController : ControllerBase
    {
        private readonly BlogDbContext _context;

        public BlogEntryController(BlogDbContext context)
        {
            _context = context;
        }

        // GET: api/BlogEntry
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogEntry>>> GetBlogEntry()
        {
            return await _context.BlogEntry.ToListAsync();
        }

        // GET: api/BlogEntry/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogEntry>> GetBlogEntry(long id)
        {
            var blogEntry = await _context.BlogEntry.FindAsync(id);

            if (blogEntry == null)
            {
                return NotFound();
            }

            return blogEntry;
        }

        // PUT: api/BlogEntry/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlogEntry(long id, BlogEntry blogEntry)
        {
            if (id != blogEntry.BlogEntryId)
            {
                return BadRequest();
            }

            _context.Entry(blogEntry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogEntryExists(id))
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

        // POST: api/BlogEntry
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BlogEntry>> PostBlogEntry(BlogEntry blogEntry)
        {
            _context.BlogEntry.Add(blogEntry);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBlogEntry", new { id = blogEntry.BlogEntryId }, blogEntry);
        }

        // DELETE: api/BlogEntry/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogEntry(long id)
        {
            var blogEntry = await _context.BlogEntry.FindAsync(id);
            if (blogEntry == null)
            {
                return NotFound();
            }

            _context.BlogEntry.Remove(blogEntry);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BlogEntryExists(long id)
        {
            return _context.BlogEntry.Any(e => e.BlogEntryId == id);
        }
    }
}
