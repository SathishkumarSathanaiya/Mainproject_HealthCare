
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace Mainproject_HealthCare.Controllers
{
    public class AdminController : Controller
    {
        private const string AdminUsername = "Sathish123";
        private const string AdminPassword = "Sathish@123456";

        private readonly ILogger<AdminController> _logger;

        public AdminController(ILogger<AdminController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (username == AdminUsername && password == AdminPassword)
            {
                // Set a session variable to indicate that the user is logged in
                HttpContext.Session.SetString("AdminLoggedIn", "true");

                // Create and set authentication cookie
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, "Admin")
                };

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true
                };

                var authScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                HttpContext.SignInAsync(authScheme, new ClaimsPrincipal(new ClaimsIdentity(authClaims, authScheme)), authProperties);

                return RedirectToAction(nameof(LoginSuccess));
            }
            else
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View();
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult LoginSuccess()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Logout()
        {
            // Remove the "AdminLoggedIn" session variable
            HttpContext.Session.Remove("AdminLoggedIn");

            // Sign out the authentication cookie
            var authScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            HttpContext.SignOutAsync(authScheme);

            return RedirectToAction(nameof(Login));
        }

        public IActionResult Check()
        {
            return View();
        }
    }
}