using ElevatorMovement.Models;

namespace ElevatorMovement.Controller
{
    public class ElevatorController
    {
        public int _numberOfElevators { get; set; } = 4;
        public int _capacity { get; set; } = 10;

        public void Simulate(int requestFloor, int requestCapacity, int maxWeight)
        {
            // Create a list of elevators
            List<Elevator> elevators = new List<Elevator>();
            for (int i = 0; i < _numberOfElevators; i++)
            {
                elevators.Add(new Elevator());
            }

            // Initialize the status of the elevators
            for (int i = 0; i < elevators.Count; i++)
            {
                elevators[i].CurrentFloor = i;
                elevators[i].IsMoving = false;
                elevators[i].Capacity = _capacity;
                elevators[i].PeopleWaiting = 0;
                elevators[i].Index = i;
            }

            var closestElevator = GetClosestElevator(requestFloor, elevators);
            
            SelectElevator(requestFloor, requestCapacity, elevators, closestElevator);

            Checks(requestCapacity, maxWeight, elevators, closestElevator);

            Console.WriteLine("closest elevator: " + elevators[closestElevator.Index].Index);
            // Start the simulation
            do
            {
                // Update the status of the elevators
                for (int i = 0; i < elevators.Count; i++)
                {
                    if (elevators[i].IsMoving)
                    {
                        MoveElevatorUpOrDown(elevators, i);
                        EmptyElevatorOnSuccess(elevators, i);
                    }
                }

                // Print the status of the elevators
                Console.WriteLine("Elevator moved a single floor:");
                for (int i = 0; i < elevators.Count; i++)
                {
                    Console.WriteLine("Elevator {0}: Current floor = {1}, Is moving = {2}, Capacity = {3}, People waiting = {4}", elevators[i].Index, elevators[i].CurrentFloor, elevators[i].IsMoving, elevators[i].Capacity, elevators[i].PeopleWaiting);
                }

                // Wait for a second before the next iteration
                Thread.Sleep(1000);
            }
            while (elevators[closestElevator.Index].CurrentFloor != requestFloor);
        }

        private void SelectElevator(int requestFloor, int requestCapacity, List<Elevator> elevators, Elevator closestElevator)
        {
            elevators[closestElevator.Index].IsMoving = true;
            elevators[closestElevator.Index].PeopleWaiting = requestCapacity;
            elevators[closestElevator.Index].DestinationFloor = requestFloor;
        }

        private void EmptyElevatorOnSuccess(List<Elevator> elevators, int i)
        {
            if (elevators[i].CurrentFloor == elevators[i].DestinationFloor)
            {
                elevators[i].IsMoving = false;
                elevators[i].PeopleWaiting = 0;
                Console.WriteLine("Elevator Arrived.");
            }
        }

        private void MoveElevatorUpOrDown(List<Elevator> elevators, int i)
        {
            if (elevators[i].CurrentFloor == elevators[i].DestinationFloor)
            {
                return;
            }

            if (elevators[i].CurrentFloor < elevators[i].DestinationFloor)
            {
                elevators[i].CurrentFloor++;
            }
            else
            {
                elevators[i].CurrentFloor--;
            }
        }

        private void Checks(int requestCapacity, int maxWeight, List<Elevator> elevators, Elevator movingElevator)
        {
            if (elevators[movingElevator.Index].Capacity < requestCapacity)
            {
                throw new Exception("This many people cannot fit, max capacity: " + elevators[movingElevator.Index].Capacity);
            }
            if (elevators[movingElevator.Index].WeightLimitKg < maxWeight)
            {
                throw new Exception("This much weight is greater than max weight of " + elevators[movingElevator.Index].WeightLimitKg.ToString());
            }
        }

        private Elevator GetClosestElevator(int floor, List<Elevator> elevators)
        {
            int closestElevatorIndex = 0;
            int closestDistance = Math.Abs(elevators[0].CurrentFloor - floor);
            for (int i = 0; i < elevators.Count; i++)
            {
                if (!elevators[i].IsMoving && Math.Abs(elevators[i].CurrentFloor - floor) < closestDistance)
                {
                    closestElevatorIndex = i;
                }           
            }

            return elevators[closestElevatorIndex];
        }
    }
}
