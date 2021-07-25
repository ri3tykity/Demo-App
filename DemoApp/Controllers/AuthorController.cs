using DemoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoApp.Controllers
{
    public class AuthorController : Controller
    {
        private DemoDbContext _demoDBContex;

        public AuthorController()
        {
            _demoDBContex = new DemoDbContext();
        }
        // GET: Author
        public ActionResult Index()
        {
            var authors = _demoDBContex.Authors.Include("Books").ToList();
            return View(authors);
        }

        public ActionResult New()
        {
            var author = new Author();
            return View("AuthorForm", author);
        }

        [HttpPost]
        public ActionResult Save(Author author)
        {
            if (author.Id == 0)
                _demoDBContex.Authors.Add(author);
            else
            {
                var dbAuthor = _demoDBContex.Authors.Single(a => a.Id == author.Id);
                // Mapping
                dbAuthor.FirstName = author.FirstName;
                dbAuthor.LastName = author.LastName;
                dbAuthor.DateOfBirth = author.DateOfBirth;
            }
            try
            {
                _demoDBContex.SaveChanges();
            }
            catch (Exception ex)
            {
                ViewBag.Message = "All input field is required";
                return View("AuthorForm", author);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int Id)
        {
            var author = _demoDBContex.Authors.SingleOrDefault(a => a.Id == Id);
            if(author != null)
            {
                _demoDBContex.Authors.Remove(author);
                _demoDBContex.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Message = "Unable to delete author";
            return View("Error");
        }

        public ActionResult Edit(int Id)
        {
            var author = _demoDBContex.Authors.SingleOrDefault(a => a.Id == Id);
            if (author == null)
            {
                return HttpNotFound();
            }

            return View("AuthorForm", author);
        }

        protected override void Dispose(bool disposing)
        {
            _demoDBContex.Dispose();
        }
    }
}