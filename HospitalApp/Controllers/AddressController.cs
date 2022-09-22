using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Entities;
using HospitalApp.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using HospitalApp.Models;


namespace HospitalApp.Controllers
{
    //[Authorize(Policy = "Address")]
    public class AddressController : Controller
    {
        private readonly IServiceRepository<Address, int> empRepo;

        public AddressController(IServiceRepository<Address, int> empRepo)
        {
            this.empRepo = empRepo;
        }

        public IActionResult Index()
        {
            ResponseStatus<Address> response = new ResponseStatus<Address>();
            response = empRepo.GetRecords();
            return View(response.Records);
        }

        public IActionResult Create()
        {
            var emp = new Address();
            return View(emp);
        }

        [HttpPost]
        public IActionResult Create(Address emp)
        {
            var response = empRepo.CreateRecord(emp);
            // If the Add is successful then Redirect to the 'Idnex' Action Method
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var respose = empRepo.GetRecord(id);
            // returna View with the data toi be edited

            return View(respose.Record);
        }

        [HttpPost]
        public IActionResult Edit(int id, Address emp)
        {
            var response = empRepo.UpdateRecord(id, emp);
            // If the Add is successful then Redirect to the 'Idnex' Action Method
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var respose = empRepo.DeleteRecord(id);
            // returna View with the data toi be edited
            return RedirectToAction("Index");
        }
    }
}