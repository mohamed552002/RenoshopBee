using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RenoshopBee.Data;
using RenoshopBee.Models;
using RenoshopBee.ViewModels;

namespace RenoshopBee.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProductsController(ApplicationDbContext context,
            IWebHostEnvironment webHostEnvironment,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
        }
        [Authorize(Roles = "Admin")]

        // GET: Products
        public async Task<IActionResult> Index()
        {
              return View(await _context.Products.ToListAsync());
        }
        [Authorize]


        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }
            ViewBag.AllProducts = _context.Products.ToList() ;
            ProductDetailsVM productDetalis = new ProductDetailsVM();
            productDetalis.product = await _context.Products
                .FirstOrDefaultAsync(m => m.ID == id);
            var pr = await _context.ProductReviews.Where(p => p.ProductId == id).ToListAsync();
            if (pr.Count > 0)
            {
                List<ApplicationUser> user = await _context.Users.ToListAsync();
                productDetalis.usersReviews = user
                    .Join(pr, user => user.Id
                    , product => product.UserId
                    , (user, productReview) => new UsersReviews
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Img_Url = user.Img_Url,
                        ReviewBody = productReview.ReviewBody
                    });
            }
            else
            {
                productDetalis.usersReviews = null;
            }
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
        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Price,Description,Seller_Name,Av_in_stock,Category,Created_at,Last_updated_at,Img_url,Is_active,NumOfSales")]
        Product product,IFormFile? imgFile,List<string>sizes)
        {
            if (ModelState.IsValid)
            {
                if (imgFile == null)
                {
                    product.Img_url = "\\images\\No_Image.png";
                }
                else
                {
                    string imgExtension = Path.GetExtension(imgFile.FileName);
                    Guid imgGuid = Guid.NewGuid();
                    string imgName = imgGuid + imgExtension;
                    string imgUrl = "\\images\\" + imgName;
                    product.Img_url = imgUrl;

                    string imgPath = _webHostEnvironment.WebRootPath + imgUrl;
                    FileStream imgStream = new FileStream(imgPath, FileMode.Create);
                    imgFile.CopyTo(imgStream);
                    imgStream.Dispose();
                }
                product.Created_at = DateTime.Now;
                product.Last_updated_at = DateTime.Now;
                product.availableSizes = new List<ProductSizes>();
                foreach (var size in sizes)
                {
                    product.availableSizes.Add(new ProductSizes { ProductId = product.ID, Size = size });
                }
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
        [Authorize(Roles = "Admin")]
        // GET: Products/Edit/5
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
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Price,Description,Seller_Name,Av_in_stock,Category,Created_at,Last_updated_at,Img_url,Is_active,NumOfSales")] 
        Product product,IFormFile? formFile)
        {
            if (id != product.ID)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    if (formFile != null)
                    {
                        //var imgOldName = _context.Products.Find(id).Img_url;
                        if (product.Img_url != @"\images\No_Image.png")
                        {
                            var imgOldPath = $"{_webHostEnvironment.WebRootPath + product.Img_url}";
                            if (System.IO.File.Exists(imgOldPath))
                            {
                                System.IO.File.Delete(imgOldPath);
                            }
                        }
                        
                        var imgExtension = Path.GetExtension(formFile.FileName);
                        var imgGuid = Guid.NewGuid();
                        var imgName = imgGuid + imgExtension;
                        var imgUrl = @"\images\" + imgName;
                        product.Img_url = imgUrl;
                        var imgPath = _webHostEnvironment.WebRootPath + imgUrl;
                        FileStream fileStream = new FileStream(imgPath, FileMode.Create);
                        await formFile.CopyToAsync(fileStream);
                        await fileStream.DisposeAsync();
                    }
                    product.Last_updated_at = DateTime.Now;
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
        [Authorize(Roles = "Admin")]
        // GET: Products/Delete/5
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
                if (product.Img_url != @"\images\No_Image.png")
                {
                    string imgPath = _webHostEnvironment.WebRootPath + product.Img_url;
                    System.IO.File.Delete(imgPath);
                }
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Sections(string section,string? sort,int pageNumber=1)
        {
            IEnumerable<Product> sectionProduct;

            if (_context.Products == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Products'  is null.");
            }
            try
            {
                switch (sort)
                {
                    case("Price"):
                        sectionProduct = _context.Products.Where(p => p.Category == section).OrderBy(p=>p.Price);
                        break;
                    case("Modified"):
                        sectionProduct = _context.Products.Where(p => p.Category == section).OrderBy(p => p.Created_at);
                        break;

                    default:
                        sectionProduct = _context.Products.Where(p => p.Category == section);
                        break;
                        

                }
                var NumOfAllProducts = sectionProduct.Count();
                ViewBag.allProducts = NumOfAllProducts;
                sectionProduct = sectionProduct.Skip((pageNumber - 1) * 10).Take(10);
                ViewBag.PForm = ((pageNumber - 1) * 10)+1;
                ViewBag.PTo = NumOfAllProducts>10? ((pageNumber - 1) * 10)+10 : ((pageNumber - 1) * 10) + NumOfAllProducts;
                ViewBag.pageNumber = pageNumber;
                ViewBag.sort = sort;
                ViewBag.SectionName = section;
            }
            catch(Exception)
            {

                return NotFound();
            }
            return View(sectionProduct);
            
        }

        private bool ProductExists(int id)
        {
          return _context.Products.Any(e => e.ID == id);
        }
    }
}
