using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FriendsTracker.Models;
using Microsoft.EntityFrameworkCore;
using System;
using FriendsTracker.Logic;
using System.Linq;

namespace FriendsTracker.Controllers
{
    //TODO SEPARATE Vies controller and API
    public class ViewController : Controller
    {
        private readonly FriendsContext _friendsContext;
        private readonly ILogger<ViewController> _logger;
        public ViewController(FriendsContext context, ILogger<ViewController> logger)
        {
            _friendsContext = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                return View(await _friendsContext.Friends.ToListAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Friend friend)
        {
            try
            {
                _friendsContext.Friends.Add(friend);
                await _friendsContext.SaveChangesAsync();
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
            try
            {
                var friendsReader = new FriendsFromFileReader();
                var friendsList= friendsReader.ReadXML();
                _friendsContext.Friends.AddRange(friendsList);
                await _friendsContext.SaveChangesAsync();
                _logger.LogInformation($"Added list of {friendsList.Count} friend(s) from XML");
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
                    var friend = await _friendsContext.Friends.FirstOrDefaultAsync(p => p.Id == id);
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
                    var friend = await _friendsContext.Friends.FirstOrDefaultAsync(p => p.Id == id);
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
            try
            {
                _friendsContext.Friends.Update(friend);
                await _friendsContext.SaveChangesAsync();
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
            try
            {
                if (id != null)
                {
                    var friend = await _friendsContext.Friends.FirstOrDefaultAsync(p => p.Id == id);
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
            try
            {
                if (id != null)
                {
                    var friend = await _friendsContext.Friends.FirstOrDefaultAsync(p => p.Id == id);
                    if (friend != null)
                    {
                        _friendsContext.Friends.Remove(friend);
                        await _friendsContext.SaveChangesAsync();
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
                int usersCount = _friendsContext.Friends.Count();
                _friendsContext.Friends.RemoveRange(_friendsContext.Friends);
                await _friendsContext.SaveChangesAsync();
                _logger.LogInformation($"Deleted all {usersCount} friend(s)");
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
