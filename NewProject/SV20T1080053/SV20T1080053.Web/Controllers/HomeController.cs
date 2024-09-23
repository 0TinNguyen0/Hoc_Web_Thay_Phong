using Microsoft.AspNetCore.Mvc;
using SV20T1080053.Web.Models;
using System.Diagnostics;

namespace SV20T1080053.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //var dal = new PersonDAL();
            //var data = dal.List();

            //ViewData["TitleMessage"] = "List of Person";
            //ViewData["ListOfPersons"] = data;
            //ViewBag.TitleMessage = "List of Person";
            //ViewBag.ListOfPersons = data;

            var data = new HomeIndexModel()
            {
                TitleMessage = "List of Persons and Students",
                ListOfPersons = new PersonDAL().List(),
                ListOfStudents = new StudentDAL().List()
            };
            var listOfPersons = new PersonDAL().List();
            var listOfStudents = new PersonDAL().List();

            return View(data);
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

        public IActionResult InputEmployee()
        {
            return View();
        }
        public IActionResult GetEmployee(InputEmployee data)
        {
            return Content($"name: {data.Name}, age: {data.Age}, address: {data.Address}");
        }
    }
}