
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mainproject_HealthCare.Data;
using Mainproject_HealthCare.Models;
using System.Security.Claims;
using Mainproject_HealthCare.Controllers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Mainproject_HealthCare.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly Mainproject_HealthCareContext _context;

        public DoctorsController(Mainproject_HealthCareContext context)
        {
            _context = context;
        }

        // GET: Doctors
        public async Task<IActionResult> Index()
        {
              return _context.Doctor != null ? 
                          View(await _context.Doctor.ToListAsync()) :
                          Problem("Entity set 'Mainproject_HealthCareContext.Doctor'  is null.");
        }

        // GET: Doctors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Doctor == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctor
                .FirstOrDefaultAsync(m => m.DoctorId == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // GET: Doctors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Doctors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DoctorId,Doctor_Name,Age,Gender,Specialty,Email,Password,phone_number,Address")] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(doctor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(doctor);
        }

        // GET: Doctors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Doctor == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctor.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }

        // POST: Doctors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DoctorId,Doctor_Name,Age,Gender,Specialty,Email,Password,phone_number,Address")] Doctor doctor)
        {
            if (id != doctor.DoctorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doctor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorExists(doctor.DoctorId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(doctor);
        }

        // GET: Doctors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Doctor == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctor
                .FirstOrDefaultAsync(m => m.DoctorId == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // POST: Doctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Doctor == null)
            {
                return Problem("Entity set 'Mainproject_HealthCareContext.Doctor'  is null.");
            }
            var doctor = await _context.Doctor.FindAsync(id);
            if (doctor != null)
            {
                _context.Doctor.Remove(doctor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoctorExists(int id)
        {
          return (_context.Doctor?.Any(e => e.DoctorId == id)).GetValueOrDefault();
        }





     








        //========================================================================================


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string Email, string Password)
        {
            if (ModelState.IsValid)
            {

                var patients = await _context.Doctor.FirstOrDefaultAsync(p => p.Email == Email && p.Password == Password);
                // Set a session variable to indicate that the user is logged in
                HttpContext.Session.SetString("DoctorLoggedIn", "true");
                if (patients != null)
                {
                    // Create and set authentication cookie
                    var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, Email),
                    new Claim(ClaimTypes.Role, "Doctor")
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

                }

            }
          
            return View();
        }

        [Authorize(Roles = "Doctor")]
       /* public IActionResult LoginSuccess()
        {
            return View();
        }*/

        public async Task<IActionResult> LoginSuccess()
        {
            if (HttpContext.Session.GetString("DoctorLoggedIn") == "true")
            {
                string? doctorEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                var doctor = await _context.Doctor.FirstOrDefaultAsync(d => d.Email == doctorEmail);

                if (doctor != null)
                {
                    // Retrieve all patients with matching problems
                    var patients = await _context.Patient.Where(p => p.Problem == doctor.Specialty).ToListAsync();
                    return View(patients);
                }
            }
            return RedirectToAction(nameof(Login));
        }


        [Authorize(Roles = "Doctor")]
        [HttpPost]
        public IActionResult Logout()
        {
            // Remove the "AdminLoggedIn" session variable
            HttpContext.Session.Remove("DoctorLoggedIn");

            // Sign out the authentication cookie
            var authScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            HttpContext.SignOutAsync(authScheme);

            return RedirectToAction(nameof(Login));
        }



    }
}
