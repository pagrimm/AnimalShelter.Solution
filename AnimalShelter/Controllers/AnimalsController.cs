using Microsoft.AspNetCore.Mvc;
using AnimalShelter.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AnimalShelter.Controllers
{
  public class AnimalsController : Controller
  {
    private readonly AnimalShelterContext _db;

    public AnimalsController(AnimalShelterContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Animal> model = _db.Animals.Include(animals => animals.Type).ToList();
      return View(model);
    }

/*     [HttpPost]
    public ActionResult Index(string orderBy)
    {
      List<Animal> model = _db.Animals.Include(animals => animals.Type).ToList().OrderBy(animal => animal.orderBy);
      return View(model);
    } */

    public ActionResult Create()
    {
      ViewBag.TypeId = new SelectList(_db.Types, "TypeId", "Name");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Animal animal)
    {
      _db.Animals.Add(animal);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Animal thisAnimal = _db.Animals.FirstOrDefault(animal => animal.AnimalId == id);
      return View(thisAnimal);
    }
  }
}