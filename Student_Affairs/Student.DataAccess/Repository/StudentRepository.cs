using Student.DataAccess.Repository.IRepository;
using Students.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student.DataAccess.Repository
{
    public class StudentRepository : Repository<Student.Models.Student>, IStudentRepository
    {
        private ApplicationDbContext _db;

        public StudentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Student.Models.Student obj)
        {
            _db.Students.Update(obj);



        }
    }
}
