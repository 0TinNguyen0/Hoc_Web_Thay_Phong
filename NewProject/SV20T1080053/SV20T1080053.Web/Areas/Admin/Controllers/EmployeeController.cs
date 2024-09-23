using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SV20T1080053.BusinessLayers;
using SV20T1080053.DomainModels;
using SV20T1080053.Web.AppCodes;
using SV20T1080053.Web.Models;
using System.Drawing.Printing;
using System.Reflection;

namespace SV20T1080053.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    //[Authorize(Roles = $"{WebUserRoles.Administrator}")] //Quyền 
    [Area("Admin")]
    public class EmployeeController : Controller
    {
        private const int PAGE_SIZE = 6;
        private const string EMPLOYEE_SEARCH = "Employee_Search";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public IActionResult Index()
        {
            var input = ApplicationContext.GetSessionData<PaginationSearchInput>(EMPLOYEE_SEARCH);
            if (input == null)
            {
                input = new PaginationSearchInput()
                {
                    Page = 1,
                    PageSize = PAGE_SIZE,
                    SearchValue = ""
                };
            }
            return View(input);
        }
        public IActionResult Search(PaginationSearchInput input)
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfEmployees(
                                            out rowCount,
                                            input.Page,
                                            input.PageSize,
                                            input.SearchValue ?? ""
                                            );
            var model = new PaginationSearchEmployee()
            {
                Page = input.Page,
                PageSize = input.PageSize,
                SearchValue = input.SearchValue ?? "",
                RowCount = rowCount,
                Data = data
            };

            ApplicationContext.SetSessionData(EMPLOYEE_SEARCH, model);

            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            var model = new Employee()
            {
                EmployeeID = 0
            };
            ViewBag.Title = "Bổ sung nhân viên";
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Edit(int id = 0)
        {
            var model = CommonDataService.GetEmployee(id);
            if (model == null)
                return RedirectToAction("Index");

            ViewBag.Title = "Cập nhật nhân viên";
            return View("Create", model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Save(Employee data, string isWork, IFormFile photo)
        {
            ViewBag.Title = data.EmployeeID == 0 ? "Bổ sung nhân viên" : "Cập nhật nhân viên";

            if (string.IsNullOrWhiteSpace(data.FullName))
                ModelState.AddModelError(nameof(data.FullName), "Tên nhân viên không được rỗng(*)");
            
            if (string.IsNullOrWhiteSpace(data.Address))
                ModelState.AddModelError(nameof(data.Address), "Địa chỉ không được rỗng(*)"); //(thông tin báo lỗi nên để *)
            if (string.IsNullOrWhiteSpace(data.Phone))
                ModelState.AddModelError(nameof(data.Phone), "Số điện thoại không được rỗng(*)");
            if (string.IsNullOrWhiteSpace(data.Email))
                ModelState.AddModelError(nameof(data.Email), "Email không được rỗng(*)");
            //if (string.IsNullOrWhiteSpace(data.Photo))
            //    ModelState.AddModelError(nameof(data.Photo), "Ảnh không được rỗng(*)");

            //Xử lý ngày sinh
            //DateTime? dBirthDate = Converter.StringToDateTime(birthday);
            //if (dBirthDate == null)
            //{
            //    ModelState.AddModelError(nameof(data.BirthDate), "Ngày sinh không hợp lệ");
            //}
            //else
            //{
            //    data.BirthDate = dBirthDate.Value;
            //}

            if (!ModelState.IsValid)
            {
                return View("Create", data);
            }


            if (data.EmployeeID == 0)
            {
                if (photo != null)
                {
                    string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
                    string SavePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "Employees", ImageName);
                    using (var stream = new FileStream(SavePath, FileMode.Create))
                    {
                        photo.CopyTo(stream);
                    }
                    data.Photo = ImageName;
                }
                //add
                int employeeId = CommonDataService.AddEmployee(data);
                if (employeeId > 0)
                {
                    return RedirectToAction("Index");
                }
                ViewBag.ErrorMessage = "Không bổ sung được dữ liệu";
                return View("Create", data);
            }
            else
            {

                // upload ảnh
                if (photo != null)
                {
                    string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
                    string SavePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "Employees", ImageName);
                    using (var stream = new FileStream(SavePath, FileMode.Create))
                    {
                        photo.CopyTo(stream);
                    }
                    data.Photo = ImageName;
                }

                // birthday

                //DateTime? dBirthDay = Converter.StringToDateTime(birthDay);

                //update
                bool success = CommonDataService.UpdateEmployee(data);
                if (success)
                {
                    return RedirectToAction("Index");
                }
                ViewBag.ErrorMessage = "Không cập nhật được dữ liệu";
                return View("Create", data);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult ChangePass()
        {
            ViewBag.Tile = "Thay đổi mật khẩu nhân viên";
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Delete(int id = 0)
        {
            if (Request.Method == "POST")
            {
                bool success = CommonDataService.DeleteEmployee(id);
                if (!success)
                    TempData["ErrorMessage"] = "Không thể xóa nhân viên này";
                return RedirectToAction("Index");
            }
            var model = CommonDataService.GetEmployee(id);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }
    }
}
