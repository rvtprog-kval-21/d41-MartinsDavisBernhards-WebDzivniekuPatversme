﻿using System;
using System.Diagnostics;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebDzivniekuPatversme.Models;
using WebDzivniekuPatversme.Services.Other;
using WebDzivniekuPatversme.Services.Interfaces;
using WebDzivniekuPatversme.Models.ViewModels.Shelters;

namespace WebDzivniekuPatversme.Controllers
{
    public class SheltersController : Controller
    {
        private readonly IShelterService _sheltersServices;
        private readonly IMapper _mapper;

        public SheltersController(
            IShelterService shelterServices,
            IMapper mapper)
        {
            _sheltersServices = shelterServices;
            _mapper = mapper;
        }

        [AllowAnonymous]
        public IActionResult Index(
            string sortOrder,
            string name,
            int? pageNumber,
            int pageSize = 3)
        { 
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["CountSortParm"] = sortOrder == "count" ? "count_desc" : "count";
            ViewData["DateAddedSortParm"] = sortOrder == "dateAdded" ? "dateAdded_desc" : "dateAdded";

            ViewData["Name"] = name;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["PageSize"] = pageSize;

            var allShelters = _sheltersServices.GetAllShelters();
            var mappedShelters = _mapper.Map<List<ShelterViewModel>>(allShelters);
            mappedShelters = _sheltersServices.FilterAndSortShelters(mappedShelters, sortOrder, name);

            ViewData["PageAmount"] = decimal
                .ToInt32(Math
                .Ceiling(mappedShelters.Count / (decimal)pageSize)) + 1;

            return View(PaginatedList<ShelterViewModel>.Create(mappedShelters, pageNumber ?? 1, pageSize));
        }

        [Authorize(Roles = "administrator,worker")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "administrator,worker")]
        public IActionResult Create(ShelterCreateViewModel shelter)
        {
            if (ModelState.IsValid)
            {
                var mappedShelter = _mapper.Map<Shelter>(shelter);
                _sheltersServices.AddNewShelter(mappedShelter);

                return RedirectToAction("Index");
            }
            return View(shelter);
        }

        [Authorize(Roles = "administrator,worker")]
        public IActionResult Edit(string Id)
        {
            var shelter = _sheltersServices.GetShelterById(Id);
            var mappedShelter = _mapper.Map<ShelterEditViewModel>(shelter);

            return View(mappedShelter);
        }

        [HttpPost]
        [Authorize(Roles = "administrator,worker")]
        public IActionResult Edit(ShelterEditViewModel shelter)
        {
            if (ModelState.IsValid)
            {
                var mappedShelter = _mapper.Map<Shelter>(shelter);
                _sheltersServices.EditShelter(mappedShelter);

                return RedirectToAction("Index");
            }
            return View(shelter);
        }

        [HttpPost]
        [Authorize(Roles = "administrator,worker")]
        public IActionResult Delete(ShelterDetailsViewModel shelter)
        {
            var mappedShelter = _mapper.Map<Shelter>(shelter);
            _sheltersServices.DeleteShelter(mappedShelter);

            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public IActionResult Details(string Id)
        {
            var shelter = _sheltersServices.GetShelterById(Id);
            var mappedShelter = _mapper.Map<ShelterDetailsViewModel>(shelter);

            return View(mappedShelter);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}