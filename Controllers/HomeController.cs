using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FriendsTracker.Models;
using Microsoft.EntityFrameworkCore;
using System;
//using FriendsTracker.Logic;

namespace FriendsTracker.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationContext db;
        private readonly ILogger<HomeController> _logger;
        public HomeController(ApplicationContext context, ILogger<HomeController> logger)
        {
            db = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            // TODO Handle pissible error
            try
            {
                return View(await db.Friends.ToListAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        // TODO: remove
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Friend friend)
        {
            // TODO Handle pissible error
            try
            {
                db.Friends.Add(friend);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<IActionResult> AddFriensFromXML()
        {
            //Write Logic.* or using Logic?
            try
            {
                var readFromXml = new Logic.ReadFriendsFromXML();
                db.Friends.AddRange(readFromXml.ReadFriends());
                await db.SaveChangesAsync();
                _logger.LogInformation("Added list of friends from XML");
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id != null)
                {
                    // TODO Handle pissible error
                    var friend = await db.Friends.FirstOrDefaultAsync(p => p.Id == id);
                    if (friend != null)
                        return View(friend);
                }
                return NotFound();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id != null)
                {
                    // TODO Handle pissible error
                    var friend = await db.Friends.FirstOrDefaultAsync(p => p.Id == id);
                    if (friend != null)
                        return View(friend);
                }
                return NotFound();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Friend friend)
        {
            // TODO Handle pissible error
            try
            {
                db.Friends.Update(friend);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            // TODO Handle pissible error
            try
            {
                if (id != null)
                {
                    var friend = await db.Friends.FirstOrDefaultAsync(p => p.Id == id);
                    if (friend != null)
                        return View(friend);
                }
                return NotFound();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }

        // TODO change to HTTP DELETE
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            // TODO Handle pissible error
            try
            {
                if (id != null)
                {
                    var friend = await db.Friends.FirstOrDefaultAsync(p => p.Id == id);
                    if (friend != null)
                    {
                        db.Friends.Remove(friend);
                        await db.SaveChangesAsync();
                        _logger.LogInformation("One friend deleted");
                        return RedirectToAction("Index");
                    }
                }
                return NotFound();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }
        public async Task<IActionResult> DeleteAllFriends()
        {
            try
            {
                //Good way to delete?
                db.Friends.RemoveRange(db.Friends);
                await db.SaveChangesAsync();
                _logger.LogInformation("All friends deleted");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
