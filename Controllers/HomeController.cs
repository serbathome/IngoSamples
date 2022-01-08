using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using sampleapp.Models;
using Microsoft.EntityFrameworkCore;

namespace sampleapp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private TodoContext _db;

    public HomeController(ILogger<HomeController> logger, IConfiguration config)
    {
        _logger = logger;
        _db = new Models.TodoContext(config["Connection"]);
    }

    public IActionResult Index()
    {
        if (_db.Todos is not null)
            return View(_db.Todos.AsAsyncEnumerable());
        else
            return View();
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddTodo(TodoForm form)
    {
        if (ModelState.IsValid)
        {
            if (_db.Todos is not null)
            {
                _db.Todos.Add(new Todo
                {
                    Title = form.Title,
                    Details = form.Details
                });
                _db.SaveChangesAsync();
            }
        }
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int TodoId)
    {
        if (_db.Todos is not null)
        {
            var t = await _db.Todos.FindAsync(TodoId);
            if (t is not null)
            {
                _db.Remove(t);
                await _db.SaveChangesAsync();
            }
        }
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? TodoId)
    {
        if (TodoId is not null)
        {
            if (_db.Todos is not null)
            {
                var todo = await _db.Todos.FindAsync(TodoId);
                if (todo is not null)
                {
                    return View(todo);
                }
            }
        }
        return View();
    }

    [HttpPost]
    public IActionResult Edit(Todo todo)
    {
        if (ModelState.IsValid)
        {
            if (todo is not null)
            {
                if (_db.Todos is not null)
                {
                    if (todo.isCompleted)
                    {
                        todo.CompletionDate = DateTime.Now;
                        todo.isStarted = false;
                    }
                    else
                    {
                        todo.CompletionDate = DateTime.MaxValue;
                    }
                    _db.Entry(todo).State = EntityState.Modified;
                    _db.SaveChangesAsync();
                }
            }
        }
        return RedirectToAction("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
