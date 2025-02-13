using Microsoft.AspNetCore.Mvc;


using System.Diagnostics;

namespace MyAPI.Controllers
{
    public class HomeController : Controller
    {
        
        
        public IActionResult Index()
        {
            return View();
        }


        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
      

        public IActionResult Privacy()
        {
            return View();
        }

      //  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
