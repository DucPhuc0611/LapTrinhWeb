using Microsoft.AspNetCore.Mvc;
using WebBanHang.Models;
using WebBanHang.Extensions;
using System.Collections.Generic;
using System.Linq;
using WebBanHang.Repositories;

namespace WebBanHang.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductRepository _productRepository;

        public CartController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // Xem giỏ hàng
        public IActionResult Index()
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("Cart") ?? new List<CartItem>();
            return View(cart);
        }

        // Thêm sản phẩm vào giỏ
        public async Task<IActionResult> AddToCart(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return NotFound();

            var cart = HttpContext.Session.Get<List<CartItem>>("Cart") ?? new List<CartItem>();

            var cartItem = cart.FirstOrDefault(c => c.Product.Id == id);
            if (cartItem != null)
            {
                cartItem.Quantity++; // Nếu có rồi thì tăng số lượng
            }
            else
            {
                cart.Add(new CartItem { Product = product, Quantity = 1 }); // Chưa có thì thêm mới
            }

            HttpContext.Session.Set("Cart", cart); // Lưu lại Session
            return RedirectToAction("Index"); // Chuyển sang trang xem giỏ hàng
        }

        // Xóa sản phẩm khỏi giỏ
        public IActionResult RemoveFromCart(int id)
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("Cart");
            if (cart != null)
            {
                var item = cart.FirstOrDefault(c => c.Product.Id == id);
                if (item != null)
                {
                    cart.Remove(item);
                    HttpContext.Session.Set("Cart", cart);
                }
            }
            return RedirectToAction("Index");
        }
    }
}