using System.Collections.Concurrent;
using SmartX.Models;

namespace SmartX.Services
{
    public class CommandEngineService
    {
        // 1. Message Queue (FIFO) - Standard telemetry processing
        public ConcurrentQueue<TelemetryPacket<object>> StandardQueue { get; } = new();

        // 2. Priority Queue - Critical alerts (bypasses standard queue)
        public PriorityQueue<TelemetryPacket<object>, int> CriticalAlertQueue { get; } = new();

        // 3. Stack (LIFO) - Command History for "Undo"
        public Stack<string> CommandHistory { get; } = new();

        // 4. Dictionary (O(1) Lookup) - Live Device Registry by MAC
        public Dictionary<string, string> DeviceRegistry { get; } = new();

        // 5. HashSet - Unique Active Errors (prevents duplicate alerts)
        public HashSet<string> ActiveErrorStates { get; } = new();

        // 6. SortedDictionary - Historical Logs by Timestamp
        public SortedDictionary<DateTime, string> HistoricalLogs { get; } = new();
    }
}