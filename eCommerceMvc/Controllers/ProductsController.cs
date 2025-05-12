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
        private readonly IFileService _fileService;

        public ProductsController(IProductInterface productService, ApplicationDbContext context, IFileService fileService)
        {
            _productService = productService;
            _context = context;
            _fileService = fileService;
        }

        public async Task<IActionResult> Index(string searchString, int? categoryId)
        {


            var products = await _productService.GetAllProductsAsync();

            if (!string.IsNullOrEmpty(searchString))
                products = products.Where(p => p.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();


            if (categoryId.HasValue)
                products = products.Where(p => p.CategoryId == categoryId.Value).ToList();

            ViewData["Categories"] = new SelectList(_context.Categories.ToList(), "CategoryId", "Name");

            return View(products);
        }


        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories.ToList(), "CategoryId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, IFormFile imageFile)
        {
            ModelState.Remove("Category");

            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    var imageUrl = await _fileService.UploadFileAsync(imageFile);
                    product.ImageUrl = imageUrl;

                }
                else
                {
                    ModelState.AddModelError("ImageUrl", "please upload an image file ");
                    ViewData["CategoryId"] = new SelectList(_context.Categories.ToList(), "CategoryId", "Name", product.CategoryId);
                    return View(product);

                }

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
        public async Task<IActionResult> Edit(int id, Product product, IFormFile imageFile)
        {
            if (id != product.Id)
                return NotFound();

            ModelState.Remove("Category");

            if (ModelState.IsValid)
            {
                var existingProduct = await _productService.GetProductByIdAsync(id);
                if (existingProduct == null)
                    return NotFound();

                if (imageFile != null && imageFile.Length > 0)
                {
                    var imageUrl = await _fileService.UploadFileAsync(imageFile);
                    existingProduct.ImageUrl = imageUrl;
                }

                existingProduct.Name = product.Name;
                existingProduct.Description = product.Description;
                existingProduct.Price = product.Price;
                existingProduct.CategoryId = product.CategoryId;

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
