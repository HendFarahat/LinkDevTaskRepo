using Student.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Students.DataAccess.Repository.IRepository
{
	public interface IClassRepository: IRepository<Student.Models.Class>
	{
	}
}
