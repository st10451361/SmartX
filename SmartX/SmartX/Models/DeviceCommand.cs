namespace SmartX.Models
{
    public class DeviceCommand
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string DeviceMac { get; set; } = string.Empty;
        public string CommandType { get; set; } = string.Empty; // e.g., "Toggle Relay", "Shut Down"
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}