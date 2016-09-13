using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using StackOverflowClone.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace StackOverflowClone.Controllers
{
    
    public class QuestionController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public QuestionController (UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            return View(_db.Questions.Where(x => x.User.Id == currentUser.Id));
        }
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost, Authorize]
        public async Task<IActionResult> Create(Question question)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            question.User = currentUser;
            question.Score = 0;
            _db.Questions.Add(question);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            var thisQuestion = _db.Questions
                .Include(questions => questions.Responses).ThenInclude(responses => responses.User)
                .Include(questions => questions.User)
                .FirstOrDefault(questions => questions.Id == id);
                
            return View(thisQuestion);
        }
        public IActionResult increment(int id)
        {
            var thisQuestion = _db.Questions.FirstOrDefault(questions => questions.Id == id);
            thisQuestion.Score++;
            _db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult decrement(int id)
        {
            var thisQuestion = _db.Questions.FirstOrDefault(questions => questions.Id == id);
            thisQuestion.Score--;
            _db.SaveChanges();
            return RedirectToAction("Index","Home");
        }
    }
}
