using Student.DataAccess.Repository.IRepository;
using Students.DataAccess.Data;
using Students.DataAccess.Repository;
using Students.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Student.DataAccess.Repository
{
    public class UnitOfWork: IUnitOfWork
    {
        private ApplicationDbContext db;
        public UnitOfWork(ApplicationDbContext _db)
        {
            db = _db;
            Student = new StudentRepository(db);
			Class = new ClassRepository(db);




		}

        public IStudentRepository Student { get; private set; }
        public IClassRepository Class { get; private set; }


		public void Save()
        {
            db.SaveChanges();
        }
    }
}
