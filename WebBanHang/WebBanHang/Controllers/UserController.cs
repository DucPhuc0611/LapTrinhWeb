using Microsoft.AspNetCore.Mvc;
using WebBanHang.Models;
using System.Linq;

namespace WebBanHang.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // HÀM XỬ LÝ LƯU DỮ LIỆU ĐĂNG KÝ
        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem tài khoản (Username) này đã ai đăng ký chưa
                var existingUser = _context.Users.FirstOrDefault(u => u.Username == user.Username);
                if (existingUser != null)
                {
                    // Nếu có rồi thì báo lỗi
                    ModelState.AddModelError("Username", "Số điện thoại/Email này đã được sử dụng!");
                    return View(user);
                }

                // Nếu chưa có thì lưu vào CSDL
                _context.Users.Add(user);
                _context.SaveChanges();

                // Đăng ký thành công thì chuyển hướng về trang Đăng nhập
                return RedirectToAction("Login");
            }
            return View(user);
        }
        // XỬ LÝ ĐĂNG NHẬP
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // Tìm user trong Database có trùng khớp tài khoản và mật khẩu không
            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                // Đăng nhập thành công: Lưu tên người dùng vào Session
                HttpContext.Session.SetString("Username", user.FullName);

                // Chuyển hướng về trang chủ
                return RedirectToAction("Index", "Home");
            }

            // Đăng nhập thất bại: Báo lỗi
            ViewBag.Error = "Tài khoản hoặc mật khẩu không chính xác!";
            return View();
        }

        // XỬ LÝ ĐĂNG XUẤT
        public IActionResult Logout()
        {
            // Xóa thông tin đăng nhập khỏi Session
            HttpContext.Session.Remove("Username");
            return RedirectToAction("Index", "Home");
        }
    }
}