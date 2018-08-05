namespace AbitYour.Models
{
    public sealed class Student
    {
        private int _number;
        private string _name;
        private int _priority;
        private double _score;

        public Student(int number, string name, int priority, double score)
        {
            Number = number;
            Name = name;
            Priority = priority;
            Score = score;
        }

        public int Number
        {
            get => _number;
            private set => _number = value > 0 ? value : 1;
        }
        
        public string Name
        {
            get => _name;
            private set
            {
                value = value.Replace("і", "i").Replace("І", "I");
                _name = value;
            }
        }
        
        public int Priority
        {
            get => _priority;
            private set => _priority = value >= 0 && value < 10 ? value : 0;
        }
        
        public double Score
        {
            get => _score;

            private set
            {
                if (value < 0 || value > 200)
                    throw new InvalidScoreException("Ошибка! Балл должен находиться в пределах 0 - 200.");
                
                _score = value;
            }
        }

        public static bool operator ==(Student student1, Student student2)
        {
            if (student1 is null && student2 is null)
                return true;

            if (student1 is null || student2 is null)
                return false;

            return student1.Equals(student2);
        }

        public static bool operator !=(Student student1, Student student2)
        {
            return !(student1 == student2);
        }

        public override bool Equals(object obj)
        {
            if (obj is Student student)
            {
                return Equals(student);
            }

            return false;
        }

        private bool Equals(Student other)
        {
            return string.Equals(_name, other._name) && _priority == other._priority && _score.Equals(other._score);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = _name is null ? 0 : _name.GetHashCode();
                hashCode = (hashCode * 397) ^ _priority;
                hashCode = (hashCode * 397) ^ _score.GetHashCode();
                return hashCode;
            }
        }
    }
}