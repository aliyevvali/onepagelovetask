using FinalPraktika2.DAL;
using FinalPraktika2.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FinalPraktika2.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class CommentController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _ev;

        public CommentController(AppDbContext context, IWebHostEnvironment ev)
        {
            _context = context;
            _ev = ev;
        }
        public IActionResult Index()
        {
            List<Comment> comments = _context.Comments.ToList();
            return View(comments);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost,ValidateAntiForgeryToken]
        public IActionResult Create(Comment comment)
        {
            if(comment.Photo != null)
            {
                string fileName = Guid.NewGuid().ToString() + comment.Photo.FileName;
                string path = Path.Combine(_ev.WebRootPath, "assets","imgs", "user");
                using (FileStream fs = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    comment.Photo.CopyTo(fs);
                }

                comment.Image = fileName;
            }
            

            _context.Comments.Add(comment);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            Comment comment = _context.Comments.Find(id);
            if (comment == null) return NotFound();
            return View(comment);
        }
        [HttpPost]
        public IActionResult Edit(Comment comment)
        {
            Comment exCom = _context.Comments.FirstOrDefault(x => x.Id == comment.Id);
            if (exCom == null) return NotFound();

           

            exCom.UserName = comment.UserName;
            exCom.Founder = comment.Founder;
            exCom.FeedBack = comment.FeedBack;
            exCom.Raiting = comment.Raiting;
            _context.SaveChanges();

            return RedirectToAction("index");
        }
        public IActionResult Delete(int? id)
        {
            if(id != null)
            {
                Comment comment = _context.Comments.Find(id);
                if(comment == null) return NotFound();
                if(comment.Image != null)
                {
                    DeleteFile(comment.Image);

                }
                _context.Comments.Remove(comment);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return BadRequest();
            
        }
        public void DeleteFile(string fileName)
        {
            string path = Path.Combine(_ev.WebRootPath, "assets", "imgs", "user");
            System.IO.File.Delete(Path.Combine(path, fileName));    
        }
    }
}
