using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FriendsTracker.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml;
using System;

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
            // TODO Handle pissible error
            try
            {
                return View(await db.Friends.ToListAsync());
            }
            catch (Exception)
            {
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
            db.Friends.Add(friend);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AddFriensFromXML()
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("C:/Users/Denys/Documents/GitHub/FriendsTracker/FriendsXMLFile.xml");
            // получим корневой элемент
            XmlElement xRoot = xDoc.DocumentElement;
            // обход всех узлов в корневом элементе
            foreach (XmlNode xnode in xRoot)
            {
                int age = 0;
                string name = "";
                foreach (XmlNode childnode in xnode.ChildNodes) if (xnode.Name == "user")
                {
                        // если узел - name
                    if (childnode.Name == "name")
                    {
                        name = childnode.InnerText;
                    }
                        // если узел age
                    if (childnode.Name == "age")
                    {
                        age = Convert.ToInt32(childnode.InnerText);
                    }
                }
                db.Friends.Add(new Friend() { Name = name, Age = age });
            }
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                // TODO Handle pissible error
                // TODO use var
                var friend = await db.Friends.FirstOrDefaultAsync(p => p.Id == id);
                if (friend != null)
                    return View(friend);
            }
            return NotFound();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                // TODO Handle pissible error
                // TODO use var
                var friend = await db.Friends.FirstOrDefaultAsync(p => p.Id == id);
                if (friend != null)
                    return View(friend);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Friend friend)
        {
            // TODO Handle pissible error
            db.Friends.Update(friend);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            // TODO Handle pissible error
            // TODO use var
            if (id != null)
            {
                var friend = await db.Friends.FirstOrDefaultAsync(p => p.Id == id);
                if (friend != null)
                    return View(friend);
            }
            return NotFound();
        }

        // TODO change to HTTP DELETE
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            // TODO Handle pissible error
            // TODO use var
            if (id != null)
            {
                var friend = await db.Friends.FirstOrDefaultAsync(p => p.Id == id);
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
