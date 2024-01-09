using app.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace app.Controllers
{
    public class VlogaController : Controller
    {
        private readonly UserManager<Vlagatelj> userManager;

        public VlogaController(UserManager<Vlagatelj> user)
        {
            userManager = user;
        }

        [HttpGet]
        public async Task<IActionResult> NovaVloga()
        {
            Vloga v = new();
            Vlagatelj user = await userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            using (var db = new KontekstBaze())
            {
                v.Delavci = db.Delavec.Where(x => x.ImeZavoda == user.ImeZavoda).ToList();
            }
            return View(v);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NovaVloga(Vloga v, int[] SelectedDelavci)
        {
            Vlagatelj user = await userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            v.Identifikator = Guid.NewGuid();
            if (ModelState.IsValid && SelectedDelavci.Length > 2)
            {
                using (var db = new KontekstBaze())
                {
                    v.Vlagatelj = db.Users.FirstOrDefault(x => x == user);
                    v.Delavci = db.Delavec.Where(d => SelectedDelavci.Contains(d.Id)).ToList();
                    db.Vloga.Add(v);
                    db.SaveChanges();
                }
                return RedirectToAction("Vloge");
            }
            else
            {
                return View(v);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Vloge()
        {
            List<Vloga> vloge = new();
            Vlagatelj user = await userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            using (var db = new KontekstBaze())
            {
                var vlogeQuery = db.Vloga.Include(x => x.Vlagatelj).Include(x => x.Delavci)
                    .Where(x => x.Vlagatelj.Email == user.Email);
                if (vlogeQuery.Any())
                {
                    vloge = vlogeQuery.ToList();
                    vloge.Reverse();
                }
            }
            return View(vloge);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Vloga> vloge = new();
            using (var db = new KontekstBaze())
            {
                var vlogeQuery = db.Vloga.Include(x => x.Vlagatelj).Include(x => x.Delavci);
                if (vlogeQuery.Any())
                {
                    vloge = vlogeQuery.ToList();
                    vloge.Reverse();
                }
            }
            return View(vloge);
        }

        [HttpGet]
        public async Task<IActionResult> PravilnaVloga(int id)
        {
            using (var db = new KontekstBaze())
            {
                Vloga vloga = db.Vloga.Find(id);
                vloga.KoncanaVloga = true;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> NepravilnaVloga(int id)
        {
            using (var db = new KontekstBaze())
            {
                Vloga vloga = db.Vloga.Find(id);
                vloga.KoncanaVloga = false;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
