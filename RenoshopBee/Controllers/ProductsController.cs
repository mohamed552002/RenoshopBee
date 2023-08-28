using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using RenoshopBee.Data;
using RenoshopBee.Interfaces.ProductInterfaces;
using RenoshopBee.Interfaces.UserInterfaces;
using RenoshopBee.Models;
using RenoshopBee.ViewModels;

namespace RenoshopBee.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IUserServices _userContext;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IProductSizes _productSizes;
        private readonly IProductDate _productDate ;
        private readonly IProductImage _productImage ;
        private readonly IProductReview _productReview;
        private readonly IProductContext _productContext;

        public ProductsController(ApplicationDbContext context,
            IWebHostEnvironment webHostEnvironment,
            UserManager<ApplicationUser> userManager,
            IProductSizes productSizes,
            IProductDate productDate,
            IProductImage productImage,
            IProductReview productReview,
            IProductContext productContext,
            IUserServices userContext)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
            _productSizes = productSizes;
            _productDate = productDate;
            _productImage = productImage;
            _productReview = productReview;
            _productContext = productContext;
            _userContext = userContext;

        }
        [Authorize(Roles = "Admin")]

        // GET: Products
        public async Task<IActionResult> Index()
        {
              return View(await _context.Products.ToListAsync());
        }
        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }
            ProductDetailsVM productDetalis = new ProductDetailsVM(await _productContext.GetProductByIdAsync(id),await _productReview.ViewProductReviewsAsync(id),await _productContext.GetProductsAsync());
            if (productDetalis.product == null)
            {
                return NotFound();
            }
            return View(productDetalis);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddReview(ProductReview review)
        {
            
            review.UserId =  _userManager.GetUserAsync(User).Result.Id;
             _context.Add(review);
            await _context.SaveChangesAsync();
            return Redirect($"Details/{review.ProductId}");
        }
        [Authorize]
        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product,IFormFile? imgFile,List<string>sizes)
        {
            if (ModelState.IsValid)
            {
                _productImage.ProductImageSet(product, imgFile);
                _productDate.SetProductDatesToNow(product);
                _productSizes.SetProductSizes(product, sizes);
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        [Authorize(Roles = "Admin")]
        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Product product,IFormFile? formFile)
        {
            if (ModelState.IsValid)
            {
                _productImage.ProductImageEdit(product, formFile);
                    _productDate.SetProductUpdatedAtNow(product);
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ID == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        [Authorize(Roles = "Admin")]
        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _productImage.ProductImageDelete(product);
                _context.Products.Remove(product);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool ProductExists(int id)
        {
          return _context.Products.Any(e => e.ID == id);
        }


    }
}
