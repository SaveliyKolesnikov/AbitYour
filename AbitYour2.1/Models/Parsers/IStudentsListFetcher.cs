using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbitYour.Models.Parsers
{
    interface IStudentsListFetcher
    {
        Task<List<Student>> GetStudentsAsync(string userName, double userScore);
    }
}
