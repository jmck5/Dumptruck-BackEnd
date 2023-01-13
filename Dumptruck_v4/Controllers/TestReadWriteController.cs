using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dumptruck_v4.Controllers {// This is an MVC controlelr that returns a view, but I don;t think I want to do it this way, I want to use an API controller not an MVC one... I think
    public class TestReadWriteController : Controller { 
        // GET: TestReadWriteController
        public ActionResult Index() {
            return View();
        }

        // GET: TestReadWriteController/Details/5
        public ActionResult Details(int id) {
            return View();
        }

        // GET: TestReadWriteController/Create
        public ActionResult Create() {
            return View();
        }

        // POST: TestReadWriteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection) {
            try {
                return RedirectToAction(nameof(Index));
            } catch {
                return View();
            }
        }

        // GET: TestReadWriteController/Edit/5
        public ActionResult Edit(int id) {
            return View();
        }

        // POST: TestReadWriteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection) {
            try {
                return RedirectToAction(nameof(Index));
            } catch {
                return View();
            }
        }

        // GET: TestReadWriteController/Delete/5
        public ActionResult Delete(int id) {
            return View();
        }

        // POST: TestReadWriteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection) {
            try {
                return RedirectToAction(nameof(Index));
            } catch {
                return View();
            }
        }
    }
}
