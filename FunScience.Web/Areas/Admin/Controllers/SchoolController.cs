﻿namespace FunScience.Web.Areas.Admin.Controllers
{
    using AutoMapper;
    using FunScience.Service;
    using FunScience.Service.Admin.Models.School;
    using FunScience.Web.Areas.Admin.Models;
    using FunScience.Web.Infrastructure;
    using Microsoft.AspNetCore.Mvc;

    public class SchoolController : AdminBaseController
    {
        private readonly ISchoolService schoolService;
        private readonly IMapper mapper;

        public SchoolController(
            ISchoolService schoolService,
            IMapper mapper)
        {
            this.schoolService = schoolService;
            this.mapper = mapper;
        }

        public IActionResult AddSchool()
        {
            return View(new SchoolViewModel { });
        }

        [HttpPost]
        public IActionResult Create(SchoolViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(AddSchool), model);
            }

            var result = this.schoolService.AddSchool(model.Name, model.Director, model.Lat, model.Lng);

            if (result)
            {
                return Redirect(nameof(Schools));
            }

            model.StatusMessage = MessageConstants.SchoolWithSameNameExist;

            return View(nameof(AddSchool), model);
        }

        public IActionResult Edit(int id)
        {
            var school = this.schoolService.SchoolInfo(id);

            if (school == null)
            {
                return BadRequest();
            }

            var schoolView = mapper.Map<SchoolViewModel>(school);

            schoolView.StatusMessage = StatusMessage;

            return View(schoolView);
        }

        [HttpPost]
        public IActionResult Edit(SchoolViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model.Id);
            }

            var result = this.schoolService.Edit(
                                    model.Id,
                                    model.Name,
                                    model.Director,
                                    model.Lat,
                                    model.Lng);
            if (result)
            {
                StatusMessage = MessageConstants.SchoolWasChanged;

                return RedirectToAction(nameof(Edit), new { id = model.Id });
            }

            return View(model);
        }
        
        public IActionResult Schools()
        {
            var schools = this.schoolService.ListOfSchools();

            return View(schools);
        }

        public IActionResult SchoolInfo(int id)
        {
            var school = this.schoolService.SchoolInfo(id);

            return View(school);
        }

        public IActionResult Delete(int id)
        {
            var model = this.schoolService.DeleteInfo(id);

            return View(model);
        }

        [HttpPost]
        public IActionResult Destroy(SchoolListingModel model)
        {
            this.schoolService.Delete(model.Id);

            return RedirectToAction(nameof(Schools));
        }
    }
}
