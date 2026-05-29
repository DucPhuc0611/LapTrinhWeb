using System.ComponentModel.DataAnnotations;

namespace WebBanHang.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tài khoản")]
        [StringLength(50)]
        public string Username { get; set; } // Dùng số điện thoại hoặc email làm tài khoản

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [StringLength(255)]
        public string Password { get; set; }
    }
}