using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HorseClub.Data;
using HorseClub.Models;

namespace HorseClub.Controllers
{
    public class LoginController : Controller
    {
        private readonly HorseClubContext _context;

        public LoginController(HorseClubContext context)
        {
            _context = context;
        }

        // GET: Login
        public async Task<IActionResult> Index()
        {
            return View(await _context.Login.ToListAsync());
        }

        public IActionResult Erorr()
        {
            return View();
        }

        // POST: Login/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminLogin([Bind("Id,LoginUser,Password")] Login Login)
        {
            HttpContext.Session.Clear();
            // admin login details 
            if ((Login.LoginUser.Equals("HorseClub@admin.com") && Login.Password.Equals("HorseClub@admin.com") ))
            {
                    HttpContext.Session.SetString("LoginUser", Login.LoginUser.ToString());
                    return RedirectToAction("Index2", "Home");
            }
            else
            {
                return RedirectToAction(nameof(UserLogin));
            }            
        }
        
        public IActionResult UserLogin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserLogin([Bind("Id,LoginUser,Password")] Login Login)
        {
            HttpContext.Session.Clear();
            var LoginData = _context.Login.Where(m => m.LoginUser == Login.LoginUser && m.Password == Login.Password).FirstOrDefault();
            // checking credentails
            if ((Login.LoginUser.Equals("HorseClub@admin.com") && Login.Password.Equals("HorseClub@admin.com") )|| LoginData != null)
            {
                if (LoginData != null)
                {
                    HttpContext.Session.SetString("Id", LoginData.Id.ToString());
                    return RedirectToAction("Index2", "Home");
                }
                else
                {
                    HttpContext.Session.SetString("LoginUser", Login.LoginUser.ToString());
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction(nameof(UserLogin));
            }            
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LoginUser,Password")] Login Login)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Login);
                await _context.SaveChangesAsync();
                HttpContext.Session.SetString("Id", Login.Id.ToString());
                return RedirectToAction("Index", "Home");
            }
            return View(Login);
        }

        private bool LoginExists(int id)
        {
            return _context.Login.Any(e => e.Id == id);
        }
    }
}
