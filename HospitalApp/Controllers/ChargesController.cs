using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Entities;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using HospitalApp.Models;
using HospitalApp.Repositories;

namespace HospitalApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize(Policy = "Charges")]
    public class ChargesController : Controller
    {
        private readonly IServiceRepository<Charges, int> empRepo;

        public ChargesController(IServiceRepository<Charges, int> empRepo)
        {
            this.empRepo = empRepo;
        }
        [HttpGet]
        public IActionResult Get()
        {
            ResponseStatus<Charges> response = new ResponseStatus<Charges>();
            response = empRepo.GetRecords();
            // return View(response.Records);
            return Ok(response.Records);

        }

        public IActionResult Create()
        {
            var emp = new Charges();
            return View(emp);
        }

        [HttpPost]
        public IActionResult Create(Charges emp)
        {
            var response = empRepo.CreateRecord(emp);
            // If the Add is successful then Redirect to the 'Idnex' Action Method
            return Ok(response.Records);
        }

        public IActionResult Edit(int id)
        {
            var respose = empRepo.GetRecord(id);
            // returna View with the data toi be edited

            return View(respose.Record);
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, Charges emp)
        {
            var response = empRepo.UpdateRecord(id, emp);
            // If the Add is successful then Redirect to the 'Idnex' Action Method
            return Ok(response);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var response = empRepo.DeleteRecord(id);
            // returna View with the data toi be edited
            //return RedirectToAction("Index");\
            return Ok(response.Records);
        }

       /* [Authorize(Roles = "Manager")]
        public IActionResult ManagerA(int id)
        {
            var emp = empRepo.GetRecord(id).Record;
            if (emp.IsHospitalApproved == 0)
                emp.IsHospitalApproved = 1;
            else
                emp.IsHospitalApproved = 0;
            var response = empRepo.UpdateRecord(id, emp);
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Administrator")]
        public IActionResult AdminA(int id)
        {
            var emp = empRepo.GetRecord(id).Record;
            if (emp.IsOrderDispatched == 0)
                emp.IsOrderDispatched = 1;
            else
                emp.IsOrderDispatched = 0;
            var response = empRepo.UpdateRecord(id, emp);
            return RedirectToAction("Index");
        }

        */
    }
}