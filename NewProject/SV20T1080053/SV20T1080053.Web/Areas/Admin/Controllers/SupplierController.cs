using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SV20T1080053.BusinessLayers;
using SV20T1080053.DomainModels;
using SV20T1080053.Web.AppCodes;
using SV20T1080053.Web.Models;
using System.Drawing.Printing;

namespace SV20T1080053.Web.Areas.Admin.Controllers
{
    ///<sumary>
    ///
    ///</sumary>
    ///
    [Authorize(Roles = $"{WebUserRoles.Administrator}")] //Quyền 
    [Area("Admin")]
    public class SupplierController : Controller
    {
        private const int PAGE_SIZE = 10;
        private const string SUPPLIER_SEARCH = "Supplier_Search";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public IActionResult Index()
        {
            var input = ApplicationContext.GetSessionData<PaginationSearchInput>(SUPPLIER_SEARCH);
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
            var data = CommonDataService.ListOfSuppliers(
                                            out rowCount,
                                            input.Page,
                                            input.PageSize,
                                            input.SearchValue ?? ""
                                            );
            var model = new PaginationSearchSupplier()
            {
                Page = input.Page,
                PageSize = input.PageSize,
                SearchValue = input.SearchValue ?? "",
                RowCount = rowCount,
                Data = data
            };

            ApplicationContext.SetSessionData(SUPPLIER_SEARCH, model);

            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            var model = new Supplier
            {
                SupplierID = 0
            };
            ViewBag.Title = "Bổ sung nhà cung cấp";
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Edit(int id = 0)
        {
            var model = CommonDataService.GetSupplier(id);
            if (model == null)
                return RedirectToAction("Index");

            ViewBag.Title = "Cập nhật nhà cung cấp";
            return View("Create", model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public IActionResult Save(Supplier data)
        {
            ViewBag.Title = data.SupplierID == 0 ? "Bổ sung nhà cung cấp" : "Cập nhật nhà cung cấp";

            if (string.IsNullOrWhiteSpace(data.SupplierName))
                ModelState.AddModelError(nameof(data.SupplierName), "Tên nhà cung cấp không được rỗng(*)");
            if (string.IsNullOrWhiteSpace(data.ContactName))
                ModelState.AddModelError(nameof(data.ContactName), "Tên nhà cung cấp không được rỗng(*)"); //(thông tin báo lỗi nên để *)
            if (string.IsNullOrWhiteSpace(data.Province))
                ModelState.AddModelError(nameof(data.Province), "Vui lòng chọn tỉnh/thành(*)");
            if (string.IsNullOrWhiteSpace(data.Address))
                ModelState.AddModelError(nameof(data.Address), "Địa chỉ không được rỗng(*)");
            if (string.IsNullOrWhiteSpace(data.Phone))
                ModelState.AddModelError(nameof(data.Phone), "Số điện thoại không được rỗng(*)");
            if (string.IsNullOrWhiteSpace(data.Email))
                ModelState.AddModelError(nameof(data.Email), "Địa chỉ Email không được rỗng(*)");

            if (!ModelState.IsValid)
            {
                return View("Create", data);
            }

            if (data.SupplierID == 0)
            {
                int customerId = CommonDataService.AddSupplier(data);
                if (customerId > 0)
                {
                    return RedirectToAction("Index");
                }
                ViewBag.ErrorMessage = "Không bổ sung được dữ liệu";
                return View("Create", data);
            }
            else
            {
                bool success = CommonDataService.UpdateSupplier(data);
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
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Delete(int id = 0)
        {
            if (Request.Method == "POST")
            {
                bool success = CommonDataService.DeleteSupplier(id);
                if (!success)
                    TempData["ErrorMessage"] = "Không thể xóa nhà cung cấp này";
                return RedirectToAction("Index");
            }
            var model = CommonDataService.GetSupplier(id);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }

    }
}
