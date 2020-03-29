﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Uplift.DataAccess.Data.Repository.IRepository;
using Uplift.Models;
using Uplift.Models.ViewModels;

namespace Uplift.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class ServiceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        [BindProperty]
        public ServiceVM  serviceVM { get; set; }

        public ServiceController(IUnitOfWork unitOfWork ,IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            serviceVM = new ServiceVM()
            {
                Service = new Service(),
                FrequencyList = _unitOfWork.Frequency.GetFrequencyListForDropDown(),
                CategoryList = _unitOfWork.Category.GetCategoryListForDropDown(),
            };

            if (id != null)
                serviceVM.Service = _unitOfWork.Service.Get(id.GetValueOrDefault());

            return View(serviceVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if(serviceVM.Service.Id == 0)
                {
                    //New Service
                    var fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\services");
                    var extension = Path.GetExtension(files[0].FileName);

                    using (var fileStreams = new FileStream(Path.Combine(uploads , fileName + extension),FileMode.Create))
                    {
                        files[0].CopyTo(fileStreams);
                    }
                    serviceVM.Service.ImageUrl = @"\images\services\" + fileName + extension;
                    _unitOfWork.Service.Add(serviceVM.Service);
                }
                else
                {
                    var serviceFromDb = _unitOfWork.Service.Get(serviceVM.Service.Id);
                    if(files.Count > 0)
                    {
                        string fileName = Guid.NewGuid().ToString();
                        var uploads = Path.Combine(webRootPath, @"images\services");
                        var extension_new = Path.GetExtension(files[0].FileName);

                        var imagePath = Path.Combine(webRootPath, serviceFromDb.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                        using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension_new), FileMode.Create))
                        {
                            files[0].CopyTo(fileStreams);
                        }
                        serviceVM.Service.ImageUrl = @"\images\services\" + fileName + extension_new;
                    }
                    else
                    {
                        serviceVM.Service.ImageUrl = serviceFromDb.ImageUrl;
                    }

                    _unitOfWork.Service.Update(serviceVM.Service);
                }
                _unitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                serviceVM.CategoryList = _unitOfWork.Category.GetCategoryListForDropDown();
                serviceVM.FrequencyList = _unitOfWork.Frequency.GetFrequencyListForDropDown();
                return View(serviceVM);
            }
        }
        #region API CALLS
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.Service.GetAll(includeProperties: "Category,Frequency") });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Service.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting..." });
            }

            _unitOfWork.Service.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Deleting successful..." });
        }
        #endregion
    }
}