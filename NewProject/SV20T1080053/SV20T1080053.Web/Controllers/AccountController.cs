using Azure.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using SV20T1080053.DataLayers;
using static SV20T1080053.BusinessLayers.UserAccountService;
using SV20T1080053.DomainModels;
using SV20T1080053.BusinessLayers;
using SV20T1080053.Web;

namespace SV20T1080053.Web.Controllers
{
    public class AccountController : Controller
    {
        /// <summary>
        /// Giao diện trang login
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(string userName = "", string password = "")
        {
            ViewBag.Username = userName;
            ViewBag.Password = password;

            var userAccount = UserAccountService.Authorize(userName, password, TypeOfAccount.Employee);

            // Kiểm tra thông tin đăng nhập đúng hay sai
            //TODO: kiểm tra userName và password từ CSDL

            if (userAccount != null)
            {
                // Đăng nhập thành công.
                //Tạo đối tượng lưu các thông tin của phiên đăng nhập người dùng
                WebUserData userData = new WebUserData()
                {
                    //UserId = userAccount.UserId,
                    //UserName = userAccount.UserName,
                    //DisplayName = userAccount.FullName,
                    //Email = userAccount.Email,
                    //Photo = userAccount.Photo,
                    //ClientIP = HttpContext.Connection.RemoteIpAddress?.ToString(),
                    //SessionId = HttpContext.Session.Id,
                    //AdditionalData = "",
                    //Roles = new List<string>() { WebUserRoles.Administrator }
                };
                //2.Thiết lập (ghi nhận) phiên đăng nhập
                await HttpContext.SignInAsync(userData.CreatePrincipal());
                //3. Quay lại trang chủ Admin
                return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
            }
            else
            {
                //Đăng nhập không thành công
                ModelState.AddModelError("Error", "Đăng nhập không thành công");
                return View();
            }

        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

    }
}