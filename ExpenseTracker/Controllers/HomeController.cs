using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ExpenseTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ExpensesDbContext _context;

        public HomeController(ILogger<HomeController> logger, ExpensesDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Expenses()
        {
            var allExpenses = _context._expenses.ToList();

            var totalExpenses = _context._expenses.Sum(x => x.Value);

            ViewBag.Expenses = totalExpenses;

            return View(allExpenses);
        }

        public IActionResult CreateEditExpense(int? id)
        {
            if(id != null)
            {
                 var espenseInDb = _context._expenses.SingleOrDefault(expense => expense.Id == id);
                 return View(espenseInDb);
            }

            return View();
        }

        public IActionResult DeleteExpense(int id)
        {
            var espenseInDb = _context._expenses.SingleOrDefault(expense => expense.Id == id);
            _context._expenses.Remove(espenseInDb);
            _context.SaveChanges();

           return RedirectToAction("Expenses");
        }

        public IActionResult CreateEditExpenseForm(Expense model)

        {
            if(model.Id == 0)
            {
                _context._expenses.Add(model);
            }else
            {
                _context._expenses.Update(model);
            }

            _context.SaveChanges();

            return RedirectToAction("Expenses");
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
}
