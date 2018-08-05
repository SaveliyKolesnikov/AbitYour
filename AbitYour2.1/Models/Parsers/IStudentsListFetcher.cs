using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbitYour.Models.Parsers
{
    interface IStudentsListFetcher
    {
        List<Student> GetStudents(string userName, double userScore, ref Student currentStudent);
    }
}
