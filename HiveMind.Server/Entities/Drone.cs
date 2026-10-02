namespace HiveMind.Server.Entities;
    public class Drone
    {
        public int DroneId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string HostName { get; set; } = string.Empty;
        public int StationSlots { get; set; } = 1;
        public List<Station> Stations { get; set; } = new List<Station>();
    }
