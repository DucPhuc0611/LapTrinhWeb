namespace WebBanHang.Repositories;
using System.Collections.Generic;
using System.Linq;
using WebBanHang.Models;

// Kế thừa interface IProductRepository
public class MockProductRepository : IProductRepository
{
    private readonly List<Product> _products;

    public MockProductRepository()
    {
        // BẠN TẠO CÁC MOCK PRODUCT Ở NGAY ĐÂY
        _products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop Dell XPS", Price = 1500m, Description = "A high-end laptop", CategoryId = 1,ImageUrl = "https://images.unsplash.com/photo-1593642632823-8f785ba67e45?w=200"},
            new Product { Id = 2, Name = "Bàn phím cơ Logitech", Price = 120m, Description = "Bàn phím cơ gõ êm ái", CategoryId = 2,ImageUrl = "https://images.unsplash.com/photo-1595225476474-87563907a212?w=200" },
            new Product { Id = 3, Name = "Chuột không dây", Price = 45m, Description = "Chuột gaming độ trễ thấp", CategoryId = 2, ImageUrl= "https://cdn2.cellphones.com.vn/insecure/rs:fill:358:358/q:90/plain/https://cellphones.com.vn/media/catalog/product/c/h/chuot-gaming-khong-day-logitech-g304-lightspeed_1_1.png"}
        };
    }

    // Các hàm bắt buộc phải có khi kế thừa IProductRepository
    public IEnumerable<Product> GetAll()
    {
        return _products;
    }

    public Product GetById(int id)
    {
        return _products.FirstOrDefault(p => p.Id == id);
    }

    public void Add(Product product)
    {
        product.Id = _products.Max(p => p.Id) + 1;
        _products.Add(product);
    }

    public void Update(Product product)
    {
        var index = _products.FindIndex(p => p.Id == product.Id);
        if (index != -1)
        {
            _products[index] = product;
        }
    }

    public void Delete(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product != null)
        {
            _products.Remove(product);
        }
    }
}
