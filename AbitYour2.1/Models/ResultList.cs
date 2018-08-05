using System.Collections.Generic;

namespace AbitYour.Models
{
    public class ResultList : IResultList
    {
        
        public IEnumerable<Student> Students => _firstPriority;
        public Student CurrentStudent { get; }
        public IReadOnlyCollection<int> NumOfStudentsWithPriority => _numOfPeopleWithPriority;

        private List<Student> _firstPriority;
        private readonly int[] _numOfPeopleWithPriority = new int[9];
        

        public ResultList(IEnumerable<Student> studentsList, Student currentStudent)
        {
            CurrentStudent = currentStudent;
            SetList(studentsList);
        }

        private void SetList(IEnumerable<Student> studentsList)
        {
            _firstPriority = new List<Student>();
            foreach (var student in studentsList)
            {
                if (student.Priority < 1 || student.Priority > 9) continue;
                _numOfPeopleWithPriority[student.Priority - 1]++;
                
                if (student.Priority == 1)
                    _firstPriority.Add(student);

                if (student.Equals(CurrentStudent)) break;
            }
        }
    }
}