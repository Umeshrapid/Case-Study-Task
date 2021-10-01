using DAL.Interface;
using DAL.Model.DBTable;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {

        private ITaskSetRepository _TaskSetRepository;

        public TaskController(ITaskSetRepository TaskSetRepository)
        {
            _TaskSetRepository = TaskSetRepository;
        }
        #region All Task List
       // [HttpGet]
        //public IActionResult TaskList()
        //{
        //    var claims = User.Claims.ToList();
        //    var ID = User.Claims.First().Value;
        //    List<TaskSet> ls = _TaskSetRepository.FindBy(x => x.CreatedUserId == ID && x.IsActive == true).ToList();  //List<Employee> ls = _EmployeeRepo.GetAll().ToList();


        //    var rJson = JsonConvert.SerializeObject(ls, Formatting.None,
        //         new JsonSerializerSettings()
        //         {
        //             ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        //         });
        //    return Ok(rJson);

        //    #region return
        //    //return Json(ls, new JsonSerializerSettings
        //    //{
        //    //    Formatting = Formatting.Indented,
        //    //});
        //    #endregion
        //}
        #endregion
    }
}
