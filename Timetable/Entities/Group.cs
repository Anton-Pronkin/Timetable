using System;
using System.Data.SqlClient;

namespace Entities
{
    public class Group
    {
        public int Year { get; }
        public int Faculty { get; }
        public int Specialty { get; }
        public bool IsMaster { get; }
        public string Number => throw new NotImplementedException();

        private Group(int year, int faculty, int specialty, bool isMaster = false)
        {
            Year = year;
            Faculty = faculty;
            Specialty = specialty;
            IsMaster = isMaster;
        }

        public static Group Create(int year, int faculty, int specialty, bool isMaster = false)
        {
            if (year < 0 || year > 9)
            {
                throw new ArgumentOutOfRangeException(nameof(year));
            }

            if (faculty < 0 || faculty > 9)
            {
                throw new ArgumentOutOfRangeException(nameof(faculty));
            }

            if (specialty < 0 || specialty > 99)
            {
                throw new ArgumentOutOfRangeException(nameof(specialty));
            }

            return new Group(year, faculty, specialty, isMaster);
        }

        public static Group Parse(string group)
        {
            if (group is null)
            {
                throw new ArgumentNullException(nameof(group));
            }

            switch (group.Length)
            {
                case 3:
                    return ParseGroupWithThreeComponent(group);
                case 4:
                    return ParseGroupWithFourComponent(group);
                case 5:
                    return ParseGroupWithFifeComponent(group);
                default:
                    throw new ArgumentOutOfRangeException(nameof(group), "Неверное количество символов в названии группы.");
            }
        }

        private static Group ParseGroupWithThreeComponent(string group)
        {
            throw new NotImplementedException();
        }

        private static Group ParseGroupWithFourComponent(string group)
        {
            throw new NotImplementedException();
        }

        private static Group ParseGroupWithFifeComponent(string group)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            if (obj is Group group)
            {
                return Equals(group);
            }

            return false;
        }

        protected bool Equals(Group group)
        {
            return Year == group.Year && 
                   Faculty == group.Faculty && 
                   Specialty == group.Specialty && 
                   IsMaster == group.IsMaster;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Year;
                hashCode = (hashCode * 397) ^ Faculty;
                hashCode = (hashCode * 397) ^ Specialty;
                hashCode = (hashCode * 397) ^ IsMaster.GetHashCode();

                return hashCode;
            }
        }
    }
}