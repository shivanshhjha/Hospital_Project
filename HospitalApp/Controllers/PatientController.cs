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
    public class PatientController : Controller
    {
        private readonly IServiceRepository<Patient, int> empRepo;
        private readonly IServiceRepository<Doctor, int> docRepo;
        private readonly  IServiceRepository<Address, int> addRepo;
        public PatientController(IServiceRepository<Patient, int> empRepo, IServiceRepository<Doctor, int> docRepo, IServiceRepository<Address, int> addRepo)
        {
            this.empRepo = empRepo;
            this.docRepo = docRepo;
            this.addRepo = addRepo;
        }
        [HttpGet]
        public IActionResult Get()
        {
            ResponseStatus<Patient> response = new ResponseStatus<Patient>();
            response = empRepo.GetRecords();
            foreach (var record in response.Records)
            {
                var x = addRepo.GetRecord(record.Address_Id).Record;

                record.Address.Address_Id = x.Address_Id;
                record.Address.Area = x.Area;
                record.Address.City = x.City;
                record.Address.State = x.State;
                record.Address.Society = x.Society;
                record.Address.DOB = x.DOB;
                record.Address.House_No = x.House_No;
            }
            // addRepo.GetRecords();
            //return View(response.Records);
            return Ok(response.Records);
        }

        public IActionResult Create()
        {            
            List<SelectListItem> x = new List<SelectListItem>();
            // Get the Doctor Data
            foreach (var item in docRepo.GetRecords().Records)
            {
                // PAss the Data To Display and Data To Select to SelectLoistItem
                int f = 0;
                foreach (var y in x)
                {
                    if (y.Text == item.Name)
                    {
                        f = 1;
                    }
                }
                if (f == 0)
                    x.Add(new SelectListItem() { Text = item.Name, Value = item.Doctor_Id.ToString() });
            }
            // Pass this data to View
            ViewBag.Doc = x;
            return View(new Patient());
        }

        [HttpPost]
        public IActionResult Create(Patient emp)
        {
            var response = empRepo.CreateRecord(emp);
            // If the Add is successful then Redirect to the 'Idnex' Action Method

            //if (emp.IsAdmitted == 1)
            //{
            //    //TempData["pid"] = emp.Patient_Id;
            //    return RedirectToAction("Create", "IPD_Patient");
            //}
            // return RedirectToAction("Index");
            return Ok(response.Record);
        }

        public IActionResult Edit(int id)
        {
            var respose = empRepo.GetRecord(id);
            List<SelectListItem> x = new List<SelectListItem>();
            // Get the Doctor Data
            foreach (var item in docRepo.GetRecords().Records)
            {
                // PAss the Data To Display and Data To Select to SelectLoistItem
                int f = 0;
                foreach (var y in x)
                {
                    if (y.Text == item.Name)
                    {
                        f = 1;
                    }
                }
                if (f == 0)
                    x.Add(new SelectListItem() { Text = item.Name, Value = item.Doctor_Id.ToString() });
            }
            // Pass this data to View
            ViewBag.Doc = x;

            //return View(respose.Record);
            return Ok();
        }

        [HttpPut("{id}")]
        //[Route("api/[controller]/[action]/{id}")]
        public IActionResult Edit(int id, Patient emp)
        {
            var response = empRepo.UpdateRecord(id, emp);
            // If the Add is successful then Redirect to the 'Idnex' Action Method
            // return RedirectToAction("Index");
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var response = empRepo.DeleteRecord(id);
            // returna View with the data toi be edited
            //return RedirectToAction("Index");
            return Ok(response);
        }
    }
}