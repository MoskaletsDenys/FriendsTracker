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
        private readonly ILogger<ViewController> _logger;
        private readonly DbOperations _dbOperations;
        public ViewController(FriendsContext context, ILogger<ViewController> logger)
        {
            _dbOperations=new DbOperations(context);
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                return View(await _dbOperations.GetAllFriends());
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
                await _dbOperations.AddFriend(friend);
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
                await _dbOperations.AddListOfFriends(friendsList);
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
                    var friend = await _dbOperations.GetFriendById(id);
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
                    var friend = await _dbOperations.GetFriendById(id);
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
                await _dbOperations.UpdateFriend(friend);
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
                    var friend = await _dbOperations.GetFriendById(id);
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
                    var friend = await _dbOperations.GetFriendById(id);
                    if (friend != null)
                    {
                        await _dbOperations.DeleteFriend(friend);
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
                int usersCount = _dbOperations.GetFriendsCount();
                await _dbOperations.DeleteAllFriends();
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
