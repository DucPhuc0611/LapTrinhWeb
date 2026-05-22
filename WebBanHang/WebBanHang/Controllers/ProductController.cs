using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebBanHang.Models;
using WebBanHang.Repositories;


public class ProductController : Controller
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;

    public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }

    public IActionResult Add()
    {
        var categories = _categoryRepository.GetAllCategories();
        ViewBag.Categories = new SelectList(categories, "Id", "Name");
        return View();
    }

    [HttpPost]
    public IActionResult Add(Product product)
    {
        if (ModelState.IsValid)
        {
            _productRepository.Add(product);
            return RedirectToAction("Index"); // Chuyển hướng tới trang danh sách sản phẩm
        }
        return View(product);
    }

    public IActionResult Index(string searchString, string sortOrder)
    {
        var products = _productRepository.GetAll();

        // 1. Chức năng Tìm kiếm (đã làm ở bước trước)
        if (!string.IsNullOrEmpty(searchString))
        {
            products = products.Where(p => p.Name.ToLower().Contains(searchString.ToLower())).ToList();
        }

        // 2. Chức năng Sắp xếp
        switch (sortOrder)
        {
            case "price_asc":
                products = products.OrderBy(p => p.Price).ToList(); // Giá tăng dần
                break;
            case "price_desc":
                products = products.OrderByDescending(p => p.Price).ToList(); // Giá giảm dần
                break;
            default:
                products = products.OrderBy(p => p.Name).ToList(); // Mặc định sắp xếp theo tên A-Z
                break;
        }

        return View(products);
    }

    // Display a single product
    public IActionResult Display(int id)
    {
        var product = _productRepository.GetById(id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }

    // Show the product update form
    public IActionResult Update(int id)
    {
        var product = _productRepository.GetById(id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }

    // Process the product update
    [HttpPost]
    public IActionResult Update(Product product)
    {
        if (ModelState.IsValid)
        {
            _productRepository.Update(product);
            return RedirectToAction("Index");
        }
        return View(product);
    }

    // Show the product delete confirmation
    public IActionResult Delete(int id)
    {
        var product = _productRepository.GetById(id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }

    // Process the product deletion
    [HttpPost, ActionName("DeleteConfirmed")]
    public IActionResult DeleteConfirmed(int id)
    {
        _productRepository.Delete(id);
        return RedirectToAction("Index");
    }
}