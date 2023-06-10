// See https://aka.ms/new-console-template for more information

using ElevatorMovement.Controller;

Console.WriteLine("Enter the floor: ");
var floor = Math.Abs(Convert.ToInt32(Console.ReadLine()));

Console.WriteLine("Enter number of passenger: ");
var passengers = Math.Abs(Convert.ToInt32(Console.ReadLine()));

Console.WriteLine("Enter the total weight: ");
var maxWeight = Math.Abs(Convert.ToInt32(Console.ReadLine()));

new ElevatorController().Simulate(floor, passengers, maxWeight);