using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using sampleapp.Models;

namespace sampleapp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private TodoContext _db;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
        _db = new Models.TodoContext();
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
