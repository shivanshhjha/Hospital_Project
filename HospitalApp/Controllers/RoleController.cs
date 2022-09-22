
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using HospitalApp.Models;
//using HospitalApp.Repositories;

//namespace HospitalApp.Controllers
//{
//    /// <summary>
//    /// Controller will be used for Creating Roles and Adding Users in Roles
//    /// </summary>

//    [Authorize(Policy = "Role")]
//    public class RoleController : Controller
//    {
//        private RoleManager<IdentityRole> roleManager;
//        private UserManager<IdentityUser> userManager;
//        private IServiceRepository<Customer, int> cusRepo;
//        /// <summary>
//        /// Injection of RoleManager<IdentityRole>
//        /// </summary>
//        /// <param name="roleManager"></param>
//        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, IServiceRepository<Customer, int> custo)
//        {
//            this.roleManager = roleManager;
//            this.userManager = userManager;
//            this.cusRepo = custo;
//        }

//        /// <summary>
//        /// Get All Roles
//        /// </summary>
//        /// <returns></returns>
//        public IActionResult Index()
//        {
//            var roles = roleManager.Roles.ToList();
//            return View(roles);
//        }

//        public IActionResult Create()
//        {
//            return View(new IdentityRole());
//        }

//        [HttpPost]
//        public async Task<IActionResult> Create(IdentityRole role)
//        {
//            var result = await roleManager.CreateAsync(role);
//            return RedirectToAction("Index");
//        }

//        public IActionResult AssignRoleToUser()
//        {
//            var users = userManager.Users.ToList();

//            List<SelectListItem> usersList = new List<SelectListItem>();
//            foreach (var user in users)
//            {
//                usersList.Add(new SelectListItem() { Text = user.UserName, Value = user.Id });
//            }

//            ViewBag.Users = usersList;

//            var roles = roleManager.Roles.ToList();
//            List<SelectListItem> rolesList = new List<SelectListItem>();
//            foreach (var role in roles)
//            {
//                rolesList.Add(new SelectListItem() { Text = role.Name, Value = role.Id });
//            }

//            ViewBag.Roles = rolesList;

//            return View(new UserInRole());
//        }
//        [HttpPost]
//        public async Task<IActionResult> AssignRoleToUser(UserInRole userInRole)
//        {
//            var users = userManager.Users.ToList();

//            List<SelectListItem> usersList = new List<SelectListItem>();
//            foreach (var user in users)
//            {
//                usersList.Add(new SelectListItem() { Text = user.UserName, Value = user.Id });
//            }

//            ViewBag.Users = usersList;

//            var roles = roleManager.Roles.ToList();
//            List<SelectListItem> rolesList = new List<SelectListItem>();
//            foreach (var role in roles)
//            {
//                rolesList.Add(new SelectListItem() { Text = role.Name, Value = role.Id });
//            }

//            ViewBag.Roles = rolesList;


//            // Logic for Assigning Role to User

//            // 1. get the User NAme Based on Id

//            IdentityUser userFind = await userManager.FindByIdAsync(userInRole.UserId);

//            if (userFind == null)
//            {
//                // Return to Error View
//                return View("Error", new ErrorViewModel()
//                {
//                    ErrorMessage = "Sorry ! User is not Found"
//                });
//            }

//            // 2. Get Role Based on Id

//            IdentityRole roleFind = await roleManager.FindByIdAsync(userInRole.RoleId);
//            if (roleFind == null)
//            {
//                // Return to Error View
//                return View("Error", new ErrorViewModel()
//                {
//                    ErrorMessage = "Sorry ! Role is not Found"
//                });
//            }
//            if (roleFind.Name == "Customer")
//            {
//                var x = new Customer
//                {
//                    Name = userFind.UserName,
//                    Email = userFind.Email,
//                    MobNo = 91,
//                    Address = "Dharur"
//                };
//                cusRepo.CreateRecord(x);
//            }
//            // 3. Add User to Role

//            await userManager.AddToRoleAsync(userFind, roleFind.Name);


//            return RedirectToAction("Index");
//        }
//    }
//}