namespace Entities
{
    public class Teacher
    {
        public string Surname { get; }
        public string Initials { get; }
        public TeacherRank Rank { get; }

        public Teacher(string surname, string initials, TeacherRank rank)
        {
            Surname = surname;
            Initials = initials;
            Rank = rank;
        }

        public override string ToString()
        {
            return $"{Surname} {Initials} ({Rank})";
        }
    }
}