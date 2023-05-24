using Domain.Entities.Lab3;
using EntityFrameworkCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Repository
{
    public class StudentInformationRepository : RepositoryBase<StudentInformation>, IStudentInformationRepository
    {
        public StudentInformationRepository(DbContextApp db) : base(db)
        {
        }
    }
}
