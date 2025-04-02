using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using shopquanao.Models;
using BCrypt.Net;

namespace shopquanao.Controllers
{
    public class AuthController : Controller
    {
        private readonly ShopQuanAoEntities _context = new ShopQuanAoEntities();

        // GET: Auth/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Auth/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "Vui lòng nhập đầy đủ tên tài khoản và mật khẩu.";
                return View();
            }

            var user = _context.Users.SingleOrDefault(u => u.username == username);
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.password))
            {
                Session["Username"] = user.username; // Adjusted to store "Username" like the first snippet
                TempData["Success"] = "Đăng nhập thành công!"; // Consistent success message
                return RedirectToAction("Index", "Home"); // Redirect to Home, not self-referencing
            }

            ViewBag.Error = "Tên truy cập hoặc mật khẩu không đúng."; // Consistent error message
            return View();
        }

        // GET: Auth/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Auth/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User user, string newPassword, string confirmPassword)
        {
            if (user == null || string.IsNullOrWhiteSpace(user.username) || string.IsNullOrWhiteSpace(newPassword) ||
                string.IsNullOrWhiteSpace(user.email))
            {
                ViewBag.Error = "Vui lòng nhập đầy đủ thông tin (tên tài khoản, email, mật khẩu).";
                return View();
            }

            if (newPassword != confirmPassword)
            {
                ViewBag.Error = "Mật khẩu và xác nhận mật khẩu không khớp.";
                return View();
            }

            if (_context.Users.Any(u => u.username == user.username))
            {
                ViewBag.Error = "Tài khoản đã tồn tại.";
                return View();
            }

            if (_context.Users.Any(u => u.email == user.email))
            {
                ViewBag.Error = "Email đã được sử dụng.";
                return View();
            }

            if (!string.IsNullOrWhiteSpace(user.phone) && _context.Users.Any(u => u.phone == user.phone))
            {
                ViewBag.Error = "Số điện thoại đã được sử dụng.";
                return View();
            }

            user.password = BCrypt.Net.BCrypt.HashPassword(newPassword);
            user.created_at = DateTime.Now;
            _context.Users.Add(user);
            _context.SaveChanges();

            TempData["Success"] = "Đăng ký thành công. Vui lòng đăng nhập.";
            return RedirectToAction("Login");
        }

        // GET: Auth/Logout
        public ActionResult Logout()
        {
            Session.Clear(); // Clear session as in both snippets
            return RedirectToAction("Login", "Auth"); // Explicitly redirect to Login in Auth controller
        }

        // GET: Auth/ResetPassword
        public ActionResult ResetPassword()
        {
            return View();
        }

        // POST: Auth/ResetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(string username, string emailOrPhone, string newPassword, string confirmPassword)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(emailOrPhone) ||
                string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                ViewBag.Error = "Vui lòng nhập đầy đủ thông tin.";
                return View();
            }

            if (newPassword != confirmPassword)
            {
                ViewBag.Error = "Mật khẩu mới và xác nhận mật khẩu không khớp.";
                return View();
            }

            var user = _context.Users.SingleOrDefault(u => u.username == username &&
                                                          (u.email == emailOrPhone || u.phone == emailOrPhone));
            if (user == null)
            {
                ViewBag.Error = "Tài khoản không tồn tại hoặc email/số điện thoại không khớp.";
                return View();
            }

            user.password = BCrypt.Net.BCrypt.HashPassword(newPassword);
            _context.SaveChanges();

            TempData["Success"] = "Đặt lại mật khẩu thành công. Vui lòng đăng nhập.";
            return RedirectToAction("Login");
        }

        // Dispose the context properly
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}