using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CinemaTicketBooking.Entities;
using Microsoft.AspNetCore.Authorization;
using CinemaTicketBooking.Models;
using Microsoft.AspNetCore.Identity;

namespace CinemaTicketBooking.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class ManageUsersController : Controller
    {
        private readonly CinemaTicketBookingContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ManageUsersController(CinemaTicketBookingContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: ManageUsers
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Index()
        {
            var listOfUsers = await _context.AspNetUsers
                .Include(i => i.AspNetUserRoles)
                .ThenInclude(it => it.Role)
                .Where(r => r.IsDeleted == false)
                .ToListAsync();
            listOfUsers.Reverse();

            return View(listOfUsers);
        }

        // GET: ManageUsers/Details/5
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aspNetUsers = await _context.AspNetUsers
                .SingleOrDefaultAsync(m => m.Id == id);
            if (aspNetUsers == null)
            {
                return NotFound();
            }

            return View(aspNetUsers);
        }

        // GET: ManageUsers/Create
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: ManageUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AspNetUsers aspNetUsers)
        {
            var user = new ApplicationUser { UserName = aspNetUsers.UserName, Email = aspNetUsers.Email, PhoneNumber = aspNetUsers.PhoneNumber };
            var result = await _userManager.CreateAsync(user, "P@ssw0rd");
            if (result.Succeeded)
            {
                AspNetUserRoles userRole = new AspNetUserRoles()
                {
                    UserId = user.Id,
                    RoleId = _context.AspNetRoles.Where(r => r.Name == "CinemaAdmin").FirstOrDefault().Id
                };

                _context.AspNetUserRoles.Add(userRole);
                await _context.SaveChangesAsync();

            }
            return RedirectToAction(nameof(Index));
        }

        // GET: ManageUsers/Edit/5
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aspNetUsers = await _context.AspNetUsers.SingleOrDefaultAsync(m => m.Id == id);
            if (aspNetUsers == null)
            {
                return NotFound();
            }
            return View(aspNetUsers);
        }

        // POST: ManageUsers/Edit/5
        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,AccessFailedCount,ConcurrencyStamp,Email,EmailConfirmed,LockoutEnabled,LockoutEnd,NormalizedEmail,NormalizedUserName,PasswordHash,PhoneNumber,PhoneNumberConfirmed,SecurityStamp,TwoFactorEnabled,UserName")] AspNetUsers aspNetUsers)
        {
            if (id != aspNetUsers.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aspNetUsers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AspNetUsersExists(aspNetUsers.Id))
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
            return View(aspNetUsers);
        }

        // GET: ManageUsers/Delete/5
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aspNetUsers = await _context.AspNetUsers
                .SingleOrDefaultAsync(m => m.Id == id);
            if (aspNetUsers == null)
            {
                return NotFound();
            }

            return View(aspNetUsers);
        }

        // POST: ManageUsers/Delete/5
        [Authorize(Roles = "SuperAdmin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var aspNetUsers = await _context.AspNetUsers.SingleOrDefaultAsync(m => m.Id == id);
            aspNetUsers.IsDeleted = true;
            _context.AspNetUsers.Update(aspNetUsers);
            _context.Entry(aspNetUsers).State = EntityState.Modified;
            var userDeleted = await _context.SaveChangesAsync() > 0;
            return RedirectToAction(nameof(Index));
        }

        private bool AspNetUsersExists(string id)
        {
            return _context.AspNetUsers.Any(e => e.Id == id);
        }
    }
}
