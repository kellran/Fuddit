﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Fuddit.Models;
using Fuddit.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Fuddit.Controllers
{
    public class PostController : Controller
    {
        private readonly ILogger<PostController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _um;

        public PostController(ILogger<PostController> logger, ApplicationDbContext db, UserManager<ApplicationUser> um)
        {
            _logger = logger;
            _db = db;
            _um = um;
        }

        [HttpGet]
        public IActionResult Index()
        {
            _logger.LogDebug("GET Guestbook/Index");
            ViewBag.Message = "This page was generated by the HTTP GET handler in Controllers/Index.cs.";
        
            // The view expects a model of type Author, so for GET just send an empty model
            return View(_db.Posts.Include(o => o.User).Include(p => p.Comments).ToList());
        }

        [HttpPost]
        public IActionResult Index(Post post)
        {
            _logger.LogDebug("POST Guestbook/Index");
            ViewBag.Message = "This page was generated by the HTTP POST handler in Controllers/Index.cs.";
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Received invalid model");
            }
            

            return View();
        }
        

        public async Task<IActionResult> Funny()
        {
            _db.Posts.Include(o => o.User).Include(p => p.Comments).ToList();
            string funnypost = "funny";
            
            var searchposts = from m in _db.Posts
                select m;

            if (!String.IsNullOrEmpty(funnypost))
            {
                searchposts = searchposts.Where(s =>
                    s.Category.ToLower().Contains(funnypost.ToLower()) || s.Tags.ToLower().Contains(funnypost.ToLower()));
            }

            return View(await searchposts.ToListAsync());
        }
        
        public async Task<IActionResult> Gaming()
        {
            _db.Posts.Include(o => o.User).Include(p => p.Comments).ToList();
            string funnypost = "gaming";
            
            var searchposts = from m in _db.Posts
                select m;

            if (!String.IsNullOrEmpty(funnypost))
            {
                searchposts = searchposts.Where(s =>
                    s.Category.ToLower().Contains(funnypost.ToLower()) || s.Tags.ToLower().Contains(funnypost.ToLower()));
            }

            return View(await searchposts.ToListAsync());
        }
        
        public async Task<IActionResult> News()
        {
            _db.Posts.Include(o => o.User).Include(p => p.Comments).ToList();
            string funnypost = "News";
            
            var searchposts = from m in _db.Posts
                select m;

            if (!String.IsNullOrEmpty(funnypost))
            {
                searchposts = searchposts.Where(s =>
                    s.Category.ToLower().Contains(funnypost.ToLower()) || s.Tags.ToLower().Contains(funnypost.ToLower()));
            }

            return View(await searchposts.ToListAsync());
        }
        
        [HttpGet]
        [Authorize]
        public IActionResult Add()
        {
            return View(new Post());
        }
        
        [HttpGet]
        [Authorize]
        public IActionResult Comment()
        {
            return View(new Comment());
        }
        
        
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("Id,Title,Body,Tags,Category,Picture,hyplink,Comments")] Post post)
        {
            if (ModelState.IsValid)
            {
                post.Like = 0;
                post.User = _um.GetUserAsync(User).Result;
                
                if (Request.Form.Files.Count > 0)
                {
                    IFormFile file = Request.Form.Files.FirstOrDefault();
                    using (var dataStream = new MemoryStream())
                    {
                        await file.CopyToAsync(dataStream);
                        post.Picture = dataStream.ToArray();
                    }
                }
                _db.Add(post);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        [Authorize]
        public IActionResult Like(int id, string redirect)
        {
            Console.WriteLine("The user liked the post in {0} of our site", redirect);
            if (!ModelState.IsValid) return RedirectToAction(redirect);

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
                return RedirectToAction(redirect);
                
            }
            
            // Remove like
            if(_db.Votes.Any(a => a.User.Id == _um.GetUserAsync(User).Result.Id && a.Post.Id == id && a._like == 1))
            {
                var current = _db.Votes
                    .FirstOrDefault(a => a.User.Id == _um.GetUserAsync(User).Result.Id && a.Post.Id == id);

                var telling = current._dislike + current._like;
                Post update = _db.Posts.ToList().Find(u => u.Id == id);
                update.Like -= telling;
                current._like = 0;

                var delete = _db.Votes.Where(a => a.User.Id == _um.GetUserAsync(User).Result.Id && a.Post.Id == id);
                _db.Votes.RemoveRange(delete);
                _db.SaveChanges();
               
                
                Console.WriteLine("Removed like");
            }
            
            // Switching from dislike to like
            if (_db.Votes.Any(a => a.User.Id == _um.GetUserAsync(User).Result.Id && a.Post.Id == id && a._dislike == 1))
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
            
            return RedirectToAction(redirect);
        }
        
        
        [Authorize]
        public IActionResult Dislike(int id, string redirect)
        {
            Console.WriteLine("The user disliked the post in {0} of our site", redirect);
            if (!ModelState.IsValid) return RedirectToAction(redirect);
            if (!_db.Votes.Any(a => a.User.Id == _um.GetUserAsync(User).Result.Id && a.Post.Id == id))
            {
                
                var auth = new Votes
                {
                    User = _um.GetUserAsync(User).Result, Post = _db.Posts.ToList().Find(u => u.Id == id), 
                    _dislike = 1
                };
            
            
                Post update = _db.Posts.ToList().Find(u => u.Id == id);
                
                update.Like -= 1;
                Console.WriteLine("Added one dislike to post");
                
                
                _db.Votes.Add(auth);
                _db.SaveChanges();
                return RedirectToAction(redirect);
            }
            
            if(_db.Votes.Any(a => a.User.Id == _um.GetUserAsync(User).Result.Id && a.Post.Id == id && a._dislike == 1))
            {
                // This lets us use the current post user. 
                var current = _db.Votes
                    .FirstOrDefault(a => a.User.Id == _um.GetUserAsync(User).Result.Id && a.Post.Id == id);
                
                
                var telling = current._dislike + current._like;
                Post update = _db.Posts.ToList().Find(u => u.Id == id);
                update.Like += telling;
                current._dislike = 0;
                
                var delete = _db.Votes.Where(a => a.User.Id == _um.GetUserAsync(User).Result.Id && a.Post.Id == id);
                _db.Votes.RemoveRange(delete);
                _db.SaveChanges();
                
                
                Console.WriteLine("Removed dislike"); 
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

            return RedirectToAction(redirect);  
        }
        
        
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            
            

            var post = await _db.Posts.FindAsync(id);

            if(post.UserId == _um.GetUserAsync(User).Result.Id)
                return View(post);
           
            
            return RedirectToAction(nameof(Index));
        }

        
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Comment([Bind("Message")] Comment comment, int postId)
        {
            if (ModelState.IsValid)
            {
                comment.User = _um.GetUserAsync(User).Result;
                comment.PostId = postId;
                _db.Add(comment);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(comment);
        }

        
        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Body,Tags,Category,Picture,hyplink")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Console.WriteLine("0");
                    post.User = _um.GetUserAsync(User).Result;
                    _db.Entry(post).State = EntityState.Modified;
                    if (post.Picture == null)
                    {
                        if (Request.Form.Files.Count > 0)
                        {
                            Console.WriteLine("1");
                            IFormFile file = Request.Form.Files.FirstOrDefault();
                            using (var dataStream = new MemoryStream())
                            {
                                await file.CopyToAsync(dataStream);
                                if (post.Picture != dataStream.ToArray())
                                {
                                    Console.WriteLine("2");
                                    post.Picture = dataStream.ToArray();
                                }
                                Console.WriteLine("3");
                            }
                        } 
                        
                    }
                    _db.Update(post);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                if (post == null)
                {
                    return RedirectToAction(nameof(Index));
                }

                return View(post);
            }
            return RedirectToAction(nameof(Index));
        }
        
        private bool PostExists(int id)
        {
            return _db.Posts.Any(e => e.Id == id);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}