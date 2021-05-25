using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Fuddit.Models;
using Fuddit.Data;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.Utils;
using Phonix;


namespace Fuddit.Controllers
{
    public class SearchController : Controller
    {
        private readonly ILogger<SearchController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _um;
        readonly Metaphone _metaphone = new Metaphone();

        public SearchController(ILogger<SearchController> logger, ApplicationDbContext db,
            UserManager<ApplicationUser> um)
        {
            _logger = logger;
            _db = db;
            _um = um;
        }
        
        
        public async Task<IActionResult> Search(string searchString)
        {
            var searchposts = from m in _db.Posts
                select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                searchposts = searchposts.Where(s =>
                    s.Title.ToLower().Contains(searchString.ToLower()) || s.Tags.ToLower().Contains(searchString.ToLower()) || s.Body.ToLower().Contains(searchString.ToLower()));
            }

            return View(await searchposts.ToListAsync());
        }
        [Authorize]
         public IActionResult Like(int id)
        {
            if (!_db.Votes.Any(a => a.User.Id == _um.GetUserAsync(User).Result.Id && a.Post.Id == id))
            {
                var auth = new Votes
                {
                    User = _um.GetUserAsync(User).Result, Post = _db.Posts.ToList().Find(u => u.Id == id), 
                    _like = 1
                };
            
            
                Post update = _db.Posts.ToList().Find(u => u.Id == id);
                update.Like += 1;
                Console.WriteLine("Added one like to post");
                
                
                _db.Votes.Add(auth);
                _db.SaveChanges();
                
            }
            // Switching from dislike to like
            if (_db.Votes.Any(a => a.User.Id == _um.GetUserAsync(User).Result.Id && a.Post.Id == id && a._dislike ==1))
            {
                var current = _db.Votes
                    .FirstOrDefault(a => a.User.Id == _um.GetUserAsync(User).Result.Id && a.Post.Id == id);

                if (current._dislike == 1 && current._like == 0)
                {
                    current._like = 1;
                    var telling = current._dislike + current._like;
                    Post update = _db.Posts.ToList().Find(u => u.Id == id);
                    update.Like += telling;
                    current._dislike = 0;
                    _db.SaveChanges();
                }

                Console.WriteLine("Changed dislike to like");
            }
                
            return RedirectToAction(nameof(Search));
        }
        [Authorize]
        public IActionResult Dislike(int id, Votes vote)
        {
            if (!_db.Votes.Any(a => a.User.Id == _um.GetUserAsync(User).Result.Id && a.Post.Id == id))
            {
                
                var auth = new Votes
                {
                    User = _um.GetUserAsync(User).Result, Post = _db.Posts.ToList().Find(u => u.Id == id), 
                    _dislike = 1
                };
            
            
                Post update = _db.Posts.ToList().Find(u => u.Id == id);
                
                update.Like -= 1;
                Console.WriteLine("Removed one like to post");
                
                
                _db.Votes.Add(auth);
                _db.SaveChanges();
            }
            // Switching from like to dislike
            if (_db.Votes.Any(a => a.User.Id == _um.GetUserAsync(User).Result.Id && a.Post.Id == id && a._like ==1))
            {
                var current = _db.Votes
                    .FirstOrDefault(a => a.User.Id == _um.GetUserAsync(User).Result.Id && a.Post.Id == id);

                if (current._dislike == 0 && current._like == 1)
                {
                    
                    current._dislike = 1;
                    var telling = current._dislike + current._like;
                    Post update = _db.Posts.ToList().Find(u => u.Id == id);
                    update.Like -= telling;
                    current._like = 0;
                    _db.SaveChanges();
                }

                Console.WriteLine("Changed like to dislike");
            }
            return RedirectToAction(nameof(Search));  
        }

    }
    
}