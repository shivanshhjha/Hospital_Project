using Application.Entities;
using HospitalApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Xml.Serialization;

namespace HospitalApp.Controllers
{
    public class monthh
    {
        public int id { get; set; }
        public string monthname { get; set; }
    }
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportsController : Controller
    {
        

        private readonly IServiceRepository<Doctor, int> docRepo;
        private readonly IServiceRepository<Patient, int> PRepo;
        private readonly IServiceRepository<IPD_Patient, int> IPDPRepo;

        public ReportsController(IServiceRepository<Doctor, int> docRepo, IServiceRepository<Patient, int> iPDRepo, IServiceRepository<IPD_Patient, int> iPDPRepo)
        {
            this.docRepo = docRepo;
            PRepo = iPDRepo;
            IPDPRepo = iPDPRepo;
        }
        public IActionResult Index()
        {
            return View();
        }
        //List of Doctors based on Specialization
        [HttpPost]
        public IActionResult ListDoctorspecialization(Doctor spec)
        {
            var x = docRepo.GetRecords().Records;

            var Doc = (from doc in x
                                       where doc.Specialization.ToLower() == spec.Specialization.ToLower()
                                       select doc);


           // return View(Doc);
            return Ok(Doc);
        }
        
        public IActionResult Doctorspecialization()
        {
            // Pass a ViewBag with List of Deparetment Object
            List<SelectListItem> x = new List<SelectListItem>();
            // Get the Doctor Data
            foreach (var item in docRepo.GetRecords().Records)
            {
                // PAss the Data To Display and Data To Select to SelectLoistItem
                int f = 0;
                foreach(var y in x)
                {
                    if(y.Text==item.Specialization)
                    {
                        f = 1;
                    }
                }
                if(f==0)
                x.Add(new SelectListItem() { Text = item.Specialization, Value = item.Specialization });
            }
            // Pass this data to View
            ViewBag.Doc = x;
            return View(new Doctor());
        }
        //Employee Doctors
        //- Consultant / Visitng Doctors
        /*public IActionResult DoctorsType(string doctype)
        {
            var x = docRepo.GetRecords().Records;

            var Doc_names = from doc in x
                            where doc.Emp_Type == doctype
                            select doc.Name;
            return View(Doc_names);
        }*/


        //List Doctor based on Emp type
        [HttpPost]
        public IActionResult ListDoctorEmptype(Doctor spec)
        {
            var x = docRepo.GetRecords().Records;

            var Doc = (from doc in x
                       where doc.Emp_Type.ToLower() == spec.Emp_Type.ToLower()
                       select doc);


            //return View(Doc);
            return Ok(Doc);
        }

        public IActionResult DoctorEmptype()
        {
            // Pass a ViewBag with List of Deparetment Object
            List<SelectListItem> x = new List<SelectListItem>();
            // Get the Doctor Data
            foreach (var item in docRepo.GetRecords().Records)
            {
                // PAss the Data To Display and Data To Select to SelectLoistItem
                int f = 0;
                foreach (var y in x)
                {
                    if (y.Text == item.Emp_Type)
                    {
                        f = 1;
                    }
                }
                if (f == 0)
                    x.Add(new SelectListItem() { Text = item.Emp_Type, Value = item.Emp_Type });
            }
            // Pass this data to View
            ViewBag.Doc = x;
            return View(new Doctor());
        }



        //Patients Admitted in the hopsital per Doctor, Ward, etc.
        [HttpPost]
        public IActionResult ListPatientperDoctor(Doctor spec)
        {
            var x = PRepo.GetRecords().Records;

            var Doc = (from pat in x
                       where pat.Doctor_Id== spec.Doctor_Id && pat.IsAdmitted==1
                       select pat);


            //return View(Doc);
            return Ok(Doc);
        }

        public IActionResult PatientperDoctor()
        {
            // Pass a ViewBag with List of Deparetment Object
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
                    x.Add(new SelectListItem() { Text = item.Name, Value = item.Doctor_Id.ToString()});
            }
            // Pass this data to View
            ViewBag.Doc = x;
            return View(new Doctor());
        }


        //total collection per month

        public IActionResult Collectonpermonth()
        {
            List<SelectListItem> x = new List<SelectListItem>();
            for(int i=1;i<=12;i++)
            {
                x.Add(new SelectListItem() { Text = $"{i}", Value = $"{i}" });
            }
            ViewBag.arr = x;
            int y = 0;
            return View(new monthh());
        }

        [HttpPost]
        public IActionResult CCollectonpermonth(monthh mont)
        {

            //var allpatients = PRepo.GetRecords().Records;
            double sum = 0;
            /*foreach(var pat in allpatients)
            {
                if (pat.IsAdmitted == 0)
                    sum += pat.TotalFee;                
            }*/

            var alladmittedp = IPDPRepo.GetRecords().Records;
            foreach(var x in alladmittedp)
            {
                if(x.Admit_Date.Month==mont.id)
                sum += x.Total_Amount;
            }
            ViewBag.Sum = sum;
            //return View();
            return Ok(sum);
        }

    }
}
