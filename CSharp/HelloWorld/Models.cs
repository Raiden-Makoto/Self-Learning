using System.Text.Json;

namespace HelloWorld
{
    public class Vehicle
    {
        public string? Actual { get; set; }
        public string? Delay { get; set; }
        public string? Destination { get; set; }
        public string? Direction { get; set; }
        public string? Route { get; set; }
        public string? Scheduled { get; set; }
        public string? VehicleNumber { get; set; }
    }

    public class Route
    {
        public string? Branch { get; set; }
        public string? Destination { get; set; }
        public string? Name { get; set; }
    }
}

