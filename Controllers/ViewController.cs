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
        private DbOperations _dbOperations;
        public ViewController(FriendsContext context, ILogger<ViewController> logger)
        {
            _dbOperations=new DbOperations(context);
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                return View(_dbOperations.GetFriends().Result);
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
        public IActionResult Create(Friend friend)
        {
            try
            {
                _dbOperations.AddFriend(friend);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public IActionResult AddFriensFromXML()
        {
            try
            {
                var friendsReader = new FriendsFromFileReader();
                var friendsList= friendsReader.ReadXML();
                _dbOperations.AddListOfFriends(friendsList);
                _logger.LogInformation($"Added list of {friendsList.Count} friend(s) from XML");
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public IActionResult Details(int? id)
        {
            try
            {
                if (id != null)
                {
                    var friend = _dbOperations.GetFriendById(id);
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

        public IActionResult Edit(int? id)
        {
            try
            {
                if (id != null)
                {
                    var friend = _dbOperations.GetFriendById(id);
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
        public IActionResult Edit(Friend friend)
        {
            try
            {
                _dbOperations.UpdateFriend(friend);
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
        public IActionResult ConfirmDelete(int? id)
        {
            try
            {
                if (id != null)
                {
                    var friend = _dbOperations.GetFriendById(id);
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
        public IActionResult Delete(int? id)
        {
            try
            {
                if (id != null)
                {
                    var friend = _dbOperations.GetFriendById(id).Result;
                    if (friend != null)
                    {
                        _dbOperations.DeleteFriend(friend);
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
        public IActionResult DeleteAllFriends()
        {
            try
            {
                int usersCount = _dbOperations.GetFriendsCount();
                //_friendsContext.Friends.RemoveRange(_friendsContext.Friends);
                //await _friendsContext.SaveChangesAsync();
                _dbOperations.DeleteAllFriends();
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
