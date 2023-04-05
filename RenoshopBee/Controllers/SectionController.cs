using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RenoshopBee.Data;
using RenoshopBee.Interfaces.ProductInterfaces;
using RenoshopBee.Models;

namespace RenoshopBee.Controllers
{
    public class SectionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductSection _productSection;
        public SectionController(ApplicationDbContext context, IProductSection productSection)
        {
            _context = context;
            _productSection = productSection;
        }

        public async Task<IActionResult> Sections(string section, string? sort, int pageNumber = 1)
        {
            IEnumerable<Product> sectionProduct;

            var sectionProducts = await _productSection.GetSectionProductsAsync(section, sort, pageNumber);
            var NumOfAllProducts = sectionProducts.Count();
            ViewBag.PForm = ((pageNumber - 1) * 10) + 1;
            ViewBag.PTo = NumOfAllProducts > 10 ? ((pageNumber - 1) * 10) + 10 : ((pageNumber - 1) * 10) + NumOfAllProducts;
            ViewBag.pageNumber = pageNumber;
            ViewBag.sort = sort;
            ViewBag.SectionName = section;
            return View(sectionProducts);
        }


    }
}
