using DAL.Interface;
using DAL.Model.DBTable;
using DAL.Model.VM;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagmentSystem.Controllers
{
   
    [ApiController]
    [Route("api/[controller]")]
  //[Authorize]
    public class StudentController : Controller
    {
        private IEmployeeRepository _EmployeeRepo;
        UserManager<IdentityUser> _userManager;
        public StudentController(IEmployeeRepository EmployeeRepo, UserManager<IdentityUser> userManager)
        {
            _EmployeeRepo = EmployeeRepo;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Get()
        {
            
            List<Employee> ls = _EmployeeRepo.GetAll().ToList();
            
            var rJson = JsonConvert.SerializeObject(ls, Formatting.None,
                 new JsonSerializerSettings()
                 {
                     ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                 });
            return Ok(rJson);

            #region return
           
            #endregion
        }
    }
}
