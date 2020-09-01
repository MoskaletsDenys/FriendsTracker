using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FriendsTracker.Models;
//???
using Microsoft.EntityFrameworkCore;

namespace FriendsTracker.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationContext db;
        public HomeController(ApplicationContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await db.Friends.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Friend friend)
        {
            db.Friends.Add(friend);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                Friend friend = await db.Friends.FirstOrDefaultAsync(p => p.Id == id);
                if (friend != null)
                    return View(friend);
            }
            return NotFound();
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Friend friend = await db.Friends.FirstOrDefaultAsync(p => p.Id == id);
                if (friend != null)
                    return View(friend);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Friend friend)
        {
            db.Friends.Update(friend);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Friend friend = await db.Friends.FirstOrDefaultAsync(p => p.Id == id);
                if (friend != null)
                    return View(friend);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Friend friend = await db.Friends.FirstOrDefaultAsync(p => p.Id == id);
                if (friend != null)
                {
                    db.Friends.Remove(friend);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }
    }
}
