using Student.DataAccess.Repository.IRepository;
using Student.DataAccess.Repository;
using Students.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Students.DataAccess.Data;

namespace Students.DataAccess.Repository
{
    public class ClassRepository : Repository<Student.Models.Class>,  IClassRepository
	{
		private ApplicationDbContext _db;

		public ClassRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}
	}
}
