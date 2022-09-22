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
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize(Policy = "Order")]
    public class DoctorController : Controller
    {
        private readonly IServiceRepository<Doctor, int> empRepo;

        public DoctorController(IServiceRepository<Doctor, int> empRepo)
        {
            this.empRepo = empRepo;
        }
        [HttpGet]
        public IActionResult Get()
        {
            ResponseStatus<Doctor> response = new ResponseStatus<Doctor>();
            response = empRepo.GetRecords();
            // return View(response.Records);
            return Ok(response.Records);
        }

        public IActionResult Create()
        {
            var emp = new Doctor();
            return View(emp);
        }

        [HttpPost]
        public IActionResult Create(Doctor emp)
        {
            var response = empRepo.CreateRecord(emp);
            // If the Add is successful then Redirect to the 'Idnex' Action Method
            //return RedirectToAction("Index");
            return Ok(response.Record);
        }

        public IActionResult Edit(int id)
        {
            var response = empRepo.GetRecord(id);
            // returna View with the data toi be edited

            return View(response.Record);
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, Doctor emp)
        {
            var response = empRepo.UpdateRecord(id, emp);
            // If the Add is successful then Redirect to the 'Idnex' Action Method
            // return RedirectToAction("Index");
            return Ok(response.Records);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var response = empRepo.DeleteRecord(id);
            // returna View with the data toi be edited
            // return RedirectToAction("Index");
            return Ok(response.Records);
        }
    }
}