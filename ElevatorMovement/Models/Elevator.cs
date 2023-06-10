namespace ElevatorMovement.Models
{
    public class Elevator
    {
        public int Index { get; set; }
        public int CurrentFloor { get; set; } = 0;
        public bool IsMoving { get; set; } = false;
        public int Capacity { get; set; } = 10;
        public int PeopleWaiting { get; set; } = 0;
        public int DestinationFloor { get; set; } = 0;
        public int WeightLimitKg { get; set; } = 100;
    }
}
