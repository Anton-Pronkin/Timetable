namespace Entities
{
    public class Room
    {
        public string Number { get; }
        public bool IsBi { get; }
        public bool IsLab { get; }

        public Room(string number, bool isLab, bool isBi)
        {
            Number = number;
            IsLab = isLab;
            IsBi = isBi;
        }

        public override string ToString()
        {
            return $"{Number} ({(IsBi ? "Бизнес-инкубатор" : "")}{(IsLab ? "Лабораторный корпус" : "")})";
        }
    }
}