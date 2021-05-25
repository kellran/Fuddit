using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Fuddit.Models;
using Fuddit.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Fuddit.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<PostController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _um;

        public UserController(ILogger<PostController> logger, ApplicationDbContext db, UserManager<ApplicationUser> um)
        {
            _logger = logger;
            _db = db;
            _um = um;
        }
        
        
        [Route("users/{nickname}", Name = "UserView")]
        public IActionResult UserView(string nickname, ApplicationUser user)
        {
            user = _db.Users.SingleOrDefault(u => u.Nickname == nickname);
            return View(user);
        }
        
        [Authorize]
        public async Task<IActionResult> Test()
        {
            // the instruction below this comment will help to get the Id of the current authenticated user. 
            // Make sure you derive your controller from Microsoft.AspNetCore.Mvc.Controller

            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}