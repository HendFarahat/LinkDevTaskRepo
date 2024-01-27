using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Student.DataAccess.Repository;
using Student.DataAccess.Repository.IRepository;
using Student.Utility;



namespace Students.Web.Controllers
{
    public class StudentController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IWebHostEnvironment _IWebHostEnvironment;
        IConfiguration configuration;


        public StudentController(IUnitOfWork _unitOfWork, IWebHostEnvironment IWebHostEnvironment, IConfiguration _config)
        {
            _UnitOfWork = _unitOfWork;
            _IWebHostEnvironment = IWebHostEnvironment; configuration = _config;
        }


        public IActionResult Index()
        {
            try
            {
                ViewBag.ClassList = _UnitOfWork.Class.GetAll();

            }
            catch (Exception e)
            {

              
            }
            return View();
        }

        public IActionResult List(int _PageNo, int? _ClassId)
        {
            var StudentList = new List<Student.Models.Student>();

            try
            {
                ViewBag.PageIndex = _PageNo;

                //Pagination
                if (_PageNo != 0)
                {
                    _PageNo = _PageNo - 1;

                }
                ViewBag.UserPageNo = _PageNo;
                int RowsPerPage = configuration.GetSection("appSettings")["RowsPerPage"] != null ? int.Parse(configuration.GetSection("appSettings")["RowsPerPage"]) : 10;
                decimal PagesCount = 0;
                var AllStudentsList = _UnitOfWork.Student.GetAll(_ClassId != null ? a => a.ClassID == _ClassId : null, IncludeProperities: "Class").ToList();

                StudentList = AllStudentsList.OrderBy(a => a.Name).Skip(_PageNo).Take(RowsPerPage).ToList();

                //get Pages Count
                decimal pages = (decimal)AllStudentsList.Count / RowsPerPage;
                if (pages > 0)
                {
                    if ((int)pages < pages)
                    {
                        pages = (int)pages + 1;
                    }
                    PagesCount = pages;
                }
                else
                {
                    PagesCount = 1;
                }

                ViewBag.PagesCount = PagesCount;


            }
            catch (Exception ex)
            {

            }
            return View(StudentList);

        }

        // [Authorize(Roles = StaticDetails.Role_Admin)]
        public IActionResult CreateOrUpdate(int? id)
        {
            Student.Models.Student model = new Student.Models.Student();
            try
            {
                ViewBag.ClassList = _UnitOfWork.Class.GetAll();

                if (id != null)
                {
                    model = _UnitOfWork.Student.GetFirstOrDefault(a => a.ID == id);
                }

            }
            catch (Exception)
            {

                
            }

            return View(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateOrUpdate(Student.Models.Student model, IFormFile Image)
        {
            try
            {

                if (Image != null)
                {
                    try
                    {


                        string wwwPath = _IWebHostEnvironment.WebRootPath;
                        string contentPath = _IWebHostEnvironment.ContentRootPath;
                        string path = Path.Combine(_IWebHostEnvironment.WebRootPath, "Photos/Students");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }


                        string fileName = Guid.NewGuid().ToString();
                        var extension = Path.GetExtension(Image.FileName);

                        if (model.Photo != null)
                        {
                            var oldImagePath = Path.Combine(wwwPath, model.Photo.TrimStart('\\'));
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                        }



                        using (var fileStream = new FileStream(Path.Combine(path, fileName + extension), FileMode.Create))
                        {
                            Image.CopyTo(fileStream);
                        }
                        model.Photo = @"\Photos\Students\" + fileName + extension;
                    }
                    catch (Exception ex)
                    {

                
                    }
                }
                if (model.ID == null || model.ID == 0)
                {
                    _UnitOfWork.Student.Add(model);
                    _UnitOfWork.Save();
                    TempData["success"] = "Student added Successfully";
                }
                else
                {
                    _UnitOfWork.Student.Update(model);
                    _UnitOfWork.Save();
                    TempData["success"] = "Student Updated Successfully";
                }

                return RedirectToAction("Index");


            }
            catch (Exception)
            {

                
            }
            return View(model);
        }


        [Authorize(Roles = StaticDetails.Role_Admin)]
        public IActionResult Delete(int? Id)
        {
            try
            {
                var Student = _UnitOfWork.Student.GetFirstOrDefault(a => a.ID == Id);
                if (Student == null)
                {
                    return Json(new { result = false, message = "Delete Fail" });
                }
                if (Student.Photo != null)
                {
                    var oldImagePath = Path.Combine(_IWebHostEnvironment.WebRootPath, Student.Photo.Trim('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                _UnitOfWork.Student.Remove(Student);
                _UnitOfWork.Save();
                return Json(new { result = true, message = "Delete Successfully" });
            }
            catch (Exception ex)
            {

             
            }
            return Json(new { result = false, message = "Delete Fail" });


        }
    }
}
