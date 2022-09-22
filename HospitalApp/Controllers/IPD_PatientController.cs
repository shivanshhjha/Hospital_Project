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
using static System.Net.Mime.MediaTypeNames;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace HospitalApp.Controllers
{
    //[Authorize(Policy = "Order")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IPD_PatientController : Controller
    {
        private readonly IServiceRepository<IPD_Patient, int> empRepo;
        private readonly IServiceRepository<Patient, int> PRepo;
        private readonly IServiceRepository<Charges, int> CRepo;
        private readonly IServiceRepository<Doctor, int> DRepo;

        public IPD_PatientController(IServiceRepository<IPD_Patient, int> empRepo, IServiceRepository<Patient, int> pRepo,IServiceRepository<Charges, int> crepo, IServiceRepository<Doctor, int> drepo)
        {
            this.empRepo = empRepo;
            PRepo = pRepo;
            CRepo= crepo;
            DRepo= drepo;
        }
        [HttpGet]
        public IActionResult Get()
        {
            ResponseStatus<IPD_Patient> response = new ResponseStatus<IPD_Patient>();
            response = empRepo.GetRecords();
            // return View(response.Records);
            return Ok(response.Records);
        }

        
        public IActionResult Create()
        {
            var emp = new IPD_Patient();
            List<SelectListItem> x = new List<SelectListItem>();
            // Get the Doctor Data
            foreach (var item in CRepo.GetRecords().Records)
            {
                // PAss the Data To Display and Data To Select to SelectLoistItem
                //x.Add(new SelectListItem() { Text = $"{item.First_Name} {item.Middle_Name} {item.Last_Name}", Value = item.Patient_Id.ToString() });
                x.Add(new SelectListItem() { Text = $"{item.Charges_Id}", Value = item.Charges_Id.ToString() });
            }
            // Pass this data to View
            ViewBag.Charges = x;            
            return View(emp);
        }
       // [Route("IPD_Patient/Create")]
        [HttpPost]
        public IActionResult Create(IPD_Patient emp)
        {
            emp.Patient_Id = PRepo.GetRecords().Records.Last().Patient_Id;

            // get doctor charge of the admitted patient

            var patient = PRepo.GetRecord(emp.Patient_Id).Record;
            var doc_of_patient = DRepo.GetRecord(patient.Doctor_Id).Record;
            
            //cal charge of 
            var charges = CRepo.GetRecord(emp.Charges_Id).Record;
            int sum = 0;                        
            
            if(emp.Nurse==1)            
            sum += charges.Nurse_Charges;

            if(emp.Canteen_Access==1)
            sum += charges.Canteen_Charges;

            sum += charges.Room_Charges;                

            if(emp.Medical_Store_Access==1)
            sum += charges.Medicine_Charges;                
            
            if(emp.Room.ToLower()=="special")
            {
                sum += 500;
            }
            else if(emp.Room.ToLower() == "shared")
            {
                sum += 250;
            }
            else
            {
                sum += 100;
            }
            emp.No_of_Days = (emp.Discharge_Date.Date - emp.Admit_Date.Date).Days;
            var totalamount = sum * emp.No_of_Days+ (doc_of_patient.Fees) * emp.No_of_Visits;
            emp.Total_Amount = totalamount;
            var response = empRepo.CreateRecord(emp);
            // If the Add is successful then Redirect to the 'Idnex' Action Method
            // return RedirectToAction("Index");
            return Ok(response);
        }

        public IActionResult Edit(int id)
        {
            var respose = empRepo.GetRecord(id);
            // returna View with the data toi be edited

            return View(respose.Record);
        }

        [HttpPost]
        public IActionResult Edit(int id, IPD_Patient emp)
        {
            var response = empRepo.UpdateRecord(id, emp);
            // If the Add is successful then Redirect to the 'Idnex' Action Method
            return RedirectToAction("Index");
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var respose = empRepo.DeleteRecord(id);
            // returna View with the data toi be edited
            return RedirectToAction("Index");
        }
        
    }
}