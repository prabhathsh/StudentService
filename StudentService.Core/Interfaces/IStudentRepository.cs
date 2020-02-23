using StudentService.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentService.Core.Interfaces
{
   public  interface  IStudentRepository : IGenericRepository<Student>
    {
    }
}
