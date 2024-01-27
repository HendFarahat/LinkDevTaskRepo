using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Student.DataAccess.Repository;
using Student.DataAccess.Repository.IRepository;
using StudentsClassAPI.Models;
using StudentsClassAPI.Repository;

namespace StudentsClassAPI.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsClassController : Controller
    {
        private readonly IJWTManagerRepository _jWTManager;
        private readonly IUnitOfWork _UnitOfWork;
        public StudentsClassController(IJWTManagerRepository jWTManager, IUnitOfWork _unitOfWork)
        {
            this._jWTManager = jWTManager; _UnitOfWork = _unitOfWork;
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(Users usersdata)
        {
            var token = _jWTManager.Authenticate(usersdata);

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }



        [HttpGet]
        public IActionResult Get()
        {

            var AllStudentsClassList = _UnitOfWork.Student.GetAll( IncludeProperities: "Class").ToList();
            return Ok(AllStudentsClassList);
           
        }

    }
}
