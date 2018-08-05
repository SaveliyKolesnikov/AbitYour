using System.Collections.Generic;

namespace AbitYour.Models
{
    public interface IResultList
    {
        IEnumerable<Student> Students { get; }
        Student CurrentStudent { get; }
        IReadOnlyCollection<int> NumOfStudentsWithPriority { get; }
    }
}