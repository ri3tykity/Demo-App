using DemoApp.Models;
using DemoApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoApp.Controllers
{
    public class BookController : Controller
    {
        private DemoDbContext _demoDBContex;

        public BookController()
        {
            _demoDBContex = new DemoDbContext();
        }
        // GET: Book
        public ActionResult Index()
        {
            var books = _demoDBContex.Books.ToList();
            return View(books);
        }

        public ActionResult New()
        {
            var book = new FormBookViewModel();
            book.Authors = new List<SelectListItem>();
            var authors = _demoDBContex.Authors.ToList();
            foreach (var author in authors)
            {
                book.Authors.Add(new SelectListItem() { Value = author.Id.ToString(), Text = author.FirstName + " " + author.LastName });
            }
            return View("BookForm", book);
        }

        [HttpPost]
        public ActionResult Save(FormBookViewModel formBook)
        {
            if(formBook.Id == 0)
            {
                List<Author> authors = new List<Author>();
                foreach (var item in formBook.Authors)
                {
                    if (item.Selected)
                    {
                        int id = int.Parse(item.Value);
                        var author = _demoDBContex.Authors.Single(a => a.Id == id);
                        authors.Add(author);
                    }
                }
                var book = new Book() { Title = formBook.Title, Authors = authors };
                _demoDBContex.Books.Add(book);
            }                
            else
            {
                var dbBook = _demoDBContex.Books.Include("Authors").Single(b => b.Id == formBook.Id);
                dbBook.Title = formBook.Title;
                List<Author> authors = new List<Author>();
                foreach (var item in formBook.Authors)
                {
                    if (item.Selected)
                    {
                        int id = int.Parse(item.Value);
                        var author = _demoDBContex.Authors.Single(a => a.Id == id);
                        authors.Add(author);
                    }
                }
                dbBook.Authors = authors;
            }

            _demoDBContex.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int Id)
        {
            var book = _demoDBContex.Books.Include("Authors").SingleOrDefault(a => a.Id == Id);
            if (book == null)
            {
                return HttpNotFound();
            }


            var formBook = new FormBookViewModel();
            formBook.Id = book.Id;
            formBook.Title = book.Title;
            formBook.Authors = new List<SelectListItem>();
            var authors = _demoDBContex.Authors.ToList();
            foreach (var author in authors)
            {
                var selectedAuthor = book.Authors.SingleOrDefault(a => a.Id == author.Id) != null;
                formBook.Authors.Add(new SelectListItem() { Selected = selectedAuthor, Value = author.Id.ToString(), Text = author.FirstName + " " + author.LastName });
            }

            return View("BookForm", formBook);
        }

        public ActionResult Delete(int Id)
        {
            var book = _demoDBContex.Books.SingleOrDefault(a => a.Id == Id);
            if (book != null)
            {
                _demoDBContex.Books.Remove(book);
                _demoDBContex.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Message = "Unable to delete book";
            return View("Error");
        }

        protected override void Dispose(bool disposing)
        {
            _demoDBContex.Dispose();
        }
    }
}