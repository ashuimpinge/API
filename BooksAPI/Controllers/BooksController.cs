using BooksAPI.Data;
using BooksAPI.Data.Entities;
using JsonPatch;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace BooksAPI.Controllers
{
    [RoutePrefix("api/books")]
    public class BooksController : ApiController
    {
        BooksContext _context = null;
        public BooksController()
        {
            _context = new BooksContext();
        }
        public async Task<IHttpActionResult> GetBooks()
        {
            var books = _context.Books.AsEnumerable();
            return Ok(books);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> GetBook(Guid id)
        {
            var book = _context.Books.Where(x => x.Id.Equals(id));
            return Ok(book); 
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> DeleteBook(Guid id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (book == null)
            {
                return NotFound();
            }
            else
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();

                return Ok();
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateBook(Book book)
        {
            book.CreateDate = DateTime.Now;
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return Ok(book);
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<IHttpActionResult> UpdateBook(Guid id, [FromBody]JsonPatchDocument<Book> patchData)
        {
            if (patchData == null)
            {
                return BadRequest();
            }

            var book = await _context.Books.FirstOrDefaultAsync(x => x.Id.Equals(id));
            book.UpdateDate = DateTime.Now;

            patchData.ApplyUpdatesTo(book);
            await _context.SaveChangesAsync();

            return Ok(book);
        }
    }
}
