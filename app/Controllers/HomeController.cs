using app.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace app.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PogojiZasebnosti()
        {
            return View();
        }

        public IActionResult UporabaPiskotkov()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PotrdiPrivolitev(bool Privolitev)
        {
            if (Privolitev)
            {
                ViewBag.PrivacyAccepted = true;
            }
            else
            {
                ViewBag.PrivacyAccepted = false;
            }

            return View("Index");
        }
    }
}