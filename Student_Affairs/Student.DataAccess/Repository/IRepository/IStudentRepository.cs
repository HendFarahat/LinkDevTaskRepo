using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student.DataAccess.Repository.IRepository
{
    public interface IStudentRepository: IRepository<Student.Models.Student>
    {
        void Update(Student.Models.Student  obj);
    }
}
