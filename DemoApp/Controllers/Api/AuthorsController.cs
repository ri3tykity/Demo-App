using DemoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DemoApp.Controllers.Api
{
    public class AuthorsController : ApiController
    {
        DemoDbContext _demoDbContext;

        public AuthorsController()
        {
            _demoDbContext = new DemoDbContext();
        }
        // GET api/authors
        public IEnumerable<Author> GetAuthors()
        {
            List<Author> result = new List<Author>();
            var authors = _demoDbContext.Authors.Include("Books").ToList();
            foreach (var item in authors)
            {
                Author author = new Author()
                {
                    Id = item.Id,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    DateOfBirth = item.DateOfBirth,
                    Books = new List<Book>()
                };

                foreach (var book in item.Books)
                {
                    author.Books.Add(new Book() { Id=book.Id, Title=book.Title });
                }

                result.Add(author);
            }
            return result;
        }

        // GET api/authors/1
        public Author GetAuthor(int Id)
        {
            var author = _demoDbContext.Authors.Include("Books").SingleOrDefault(a => a.Id == Id);
            if (author == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Author result = new Author()
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                DateOfBirth = author.DateOfBirth,
                Books = new List<Book>()
            };

            foreach (var book in author.Books)
            {
                result.Books.Add(new Book() { Id = book.Id, Title = book.Title });
            }

            return result;
        }

        protected override void Dispose(bool disposing)
        {
            _demoDbContext.Dispose();
        }
    }
}
