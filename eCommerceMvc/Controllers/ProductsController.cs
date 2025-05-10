using eCommerceMvc.Data;
using eCommerceMvc.Models;
using eCommerceMvc.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eCommerceManagement.Controllers
{
    public class ProductsController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IProductInterface _productService;

        public ProductsController(IProductInterface productService, ApplicationDbContext context)
        {
            _productService = productService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProductsAsync();
            return View(products);
        }


        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories.ToList(), "CategoryId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            ModelState.Remove("Category");

            if (ModelState.IsValid)
            {
                await _productService.CreateProductAsync(product);
                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoryId"] = new SelectList(_context.Categories.ToList(), "CategoryId", "Name", product.CategoryId);
            return View(product);
        }


        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
                return NotFound();

            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();

            ViewData["CategoryId"] = new SelectList(_context.Categories.ToList(), "CategoryId", "Name", product.CategoryId);
            return View(product);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.Id)
                return NotFound();

            ModelState.Remove("Category");

            if (ModelState.IsValid)
            {
                await _productService.UpdateProductAsync(product);
                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoryId"] = new SelectList(_context.Categories.ToList(), "CategoryId", "Name", product.CategoryId);
            return View(product);
        }


        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
                return NotFound();

            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productService.DeleteProductAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
                return NotFound();

            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
                return NotFound();
            return View(product);

        }

    }
}
