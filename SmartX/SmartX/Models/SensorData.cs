namespace SmartX.Models
{
    public class PowerMeter
    {
        public double Wattage { get; set; }

        // Operator Overloading: Allows Meter3 = Meter1 + Meter2
        public static PowerMeter operator +(PowerMeter a, PowerMeter b)
        {
            return new PowerMeter { Wattage = a.Wattage + b.Wattage };
        }
    }

    public class TelemetryBatch
    {
        // Advanced Jagged Array to hold raw historical data
        public double[][] RawHistoricalData { get; set; }

        public TelemetryBatch(int sensors, int readingsPerSensor)
        {
            RawHistoricalData = new double[sensors][];
            for (int i = 0; i < sensors; i++)
            {
                RawHistoricalData[i] = new double[readingsPerSensor];
            }
        }
    }
}