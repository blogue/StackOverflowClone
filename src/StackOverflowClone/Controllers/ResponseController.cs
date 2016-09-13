using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using StackOverflowClone.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace StackOverflowClone.Controllers
{
    public class ResponseController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public ResponseController(UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }
        [Authorize]
        public IActionResult Create(int questionId)
        {
            Response newResponse = new Response(questionId);
            return View(newResponse);
        }
        [HttpPost, Authorize]
        public async Task<IActionResult> Create(Response response)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            response.User = currentUser;
            response.Score = 0;
            _db.Responses.Add(response);
            _db.SaveChanges();
            return RedirectToAction("Details", "Question", new { id = response.QuestionId });
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
