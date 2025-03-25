using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyMvcApp.Controllers;

public class UserController : Controller
{
    public static List<User> Users { get; set; } = new List<User>();

    // GET: User
    public IActionResult Index()
    {
        return View(Users);
    }

    // GET: User/Details/5
    public IActionResult Details(int id)
    {
        var user = Users.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    // GET: User/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: User/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(User user)
    {
        if (ModelState.IsValid)
        {
            Users.Add(user);
            return RedirectToAction(nameof(Index));
        }
        return View(user);
    }

    // GET: User/Edit/5
    public IActionResult Edit(int id)
    {
        var user = Users.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    // POST: User/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, User user)
    {
        if (id != user.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var existingUser = Users.FirstOrDefault(u => u.Id == id);
            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(user);
    }

    // GET: User/Delete/5
    public IActionResult Delete(int id)
    {
        var user = Users.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    // POST: User/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var user = Users.FirstOrDefault(u => u.Id == id);
        if (user != null)
        {
            Users.Remove(user);
        }
        return RedirectToAction(nameof(Index));
    }

    // GET: User/Search
    public IActionResult Search(string query)
    {
        if (string.IsNullOrEmpty(query))
        {
            // Retorna una vista con una lista vacía si el query es nulo o vacío
            return View(new List<User>());
        }

        var results = Users.Where(u => u.Name.Contains(query) || u.Email.Contains(query)).ToList();
        return View(results);
    }
}
