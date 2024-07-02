using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAppMvc.Models;
using WebAppMvc.Models.Dtos;
using WebAppMvc.Services;

namespace WebAppMvc.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public CategoryController (ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public IActionResult Index()
        {  
            return View(_db.Categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategory input)
        {
            if(!ModelState.IsValid)
            {
                return View(input);
            }

            var category = _mapper.Map<Category>(input);

            await _db.Categories.AddAsync(category);
            await _db.SaveChangesAsync();

            TempData["InfoSuccess"] = "Category Created Successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var category = _db.Categories.Find(id);
            if(category == null)
            {
                TempData["InfoError"] = "Category Not Found!";
                return RedirectToAction("Index");
            }

            var input = _mapper.Map<CreateCategory>(category);

            ViewData["CategoryId"] = category.Id;
            
            return View(input);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CreateCategory input)
        {
            if(!ModelState.IsValid)
            {
                return View(input);
            }

            var category = _mapper.Map<Category>(input);

            _db.Categories.Update(category);

            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int CategoryId)
        {
            var category = _db.Categories.Find(CategoryId);

            if(category != null)
            {
                _db.Categories.Remove(category);
                await _db.SaveChangesAsync();
                TempData["InfoSuccess"] = "Category Deleted Successfully";
            }

            return RedirectToAction("Index");
        }
    }
}
