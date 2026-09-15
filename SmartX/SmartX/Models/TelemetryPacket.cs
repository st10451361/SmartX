namespace SmartX.Models
{
    // Generic class to handle disparate data types without boxing/unboxing
    public class TelemetryPacket<T>
    {
        public string MacAddress { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public T Payload { get; set; }

        public TelemetryPacket(string mac, T payload)
        {
            MacAddress = mac;
            Payload = payload;
        }
    }
}