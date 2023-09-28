using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)  {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }
        public IActionResult Create() {
            
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name","The Display Order cannot exactly match the Name");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Created Successfully!";
                return RedirectToAction("Index");
            }
            return View();
            }
        public IActionResult Edit(int? id)
        {
            if(id == 0 || id == null) {
                return NotFound();
            }
            Category categoryfromDb = _db.Categories.Find(id);
            if(categoryfromDb == null)
            {
                return NotFound();
            }

            return View(categoryfromDb);
        }
        [HttpPost]
        public IActionResult Edit (Category obj)
        {
   
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Changed Successfully!";
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet, ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            //if (id == 0 || id == null)
            //{
            //    return NotFound();
            //}
            //Category categoryfromDb = _db.Categories.Find(id);
            //if (categoryfromDb == null)
            //{
            //    return NotFound();
            //}

            //return View(categoryfromDb);

            Category obj = _db.Categories.Find(id); if (obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);
             _db.SaveChanges();
            TempData["success"] = "Category Removed Successfully!";
            return RedirectToAction("Index");

            return View();
        }
        
        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeletePOST()
        //{
        //    int id = Convert.ToInt32(Request.Form["Id"]);
        //    Category obj = _db.Categories.Find(id); if (obj == null)
        //    {
        //        return NotFound();
        //    }
        //    _db.Categories.Remove(obj);
        //    _db.SaveChanges();
        //    return RedirectToAction("Index");
         
        //    return View();
        //}



    }
}
