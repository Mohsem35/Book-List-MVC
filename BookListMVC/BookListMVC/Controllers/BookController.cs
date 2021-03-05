using BookListMVC.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookListMVC.Controllers
{
    public class BookController : Controller
    {
        private readonly BookDbContext _db;
        public BookController(BookDbContext db)
        {
            _db = db;
        }
        public ActionResult Index()
        {
            return View(_db.Books.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book bookObj)                        //object needed for create new book
        {
            if (ModelState.IsValid)
            {
                _db.Books.Add(bookObj);
                _db.SaveChanges();
                return RedirectToAction("Index", "Book");
            }
            return View(bookObj);

        }

        [HttpGet]
        public ActionResult Edit(int? id)                                        //"question mark", cause int can't be null
        {
            if (id == null)                                                              //1st chack "Id" null
            {
                return NotFound();
            }
            var book = _db.Books.Where(x => x.BookId == id).FirstOrDefault();
            if (book == null)                                                          //2nd check "book" null
            {
                return NotFound();
            }
            return View(book);                                       //if "book" found, then we can preview the book. So it returns "book"
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Book bookObj)                                                      //object needed
        {
            Book b = _db.Books.Where(s => s.BookId == bookObj.BookId).FirstOrDefault();       //finds matching between model and object "Id"
            b.Name  = bookObj.Name;
            b.Author = bookObj.Author;
            b.Pages = bookObj.Pages;
            _db.SaveChanges();
            return RedirectToAction("Index", "Book");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var del = _db.Books.Where(s => s.BookId == id).FirstOrDefault();
            _db.Books.Remove(del);
            _db.SaveChanges();
            return RedirectToAction("Index", "Book");
        }

    }
}
