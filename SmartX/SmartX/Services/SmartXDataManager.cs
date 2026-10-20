using SmartX.Models;
using System.Collections.Concurrent;

namespace SmartX.Services
{
    public class SmartXDataManager
    {
        // 1. STANDARD QUEUE (FIFO): For normal telemetry processing
        public Queue<string> StandardTelemetryQueue { get; } = new();

        // 2. PRIORITY QUEUE: For critical alerts (simulated using a sorted list for immediate processing)
        public List<string> CriticalAlertQueue { get; } = new();

        // 3. STACK (LIFO): For Undo functionality on manual commands
        public Stack<DeviceCommand> CommandHistory { get; } = new();

        // 4. DICTIONARY: O(1) lookup for active devices by MAC address
        public Dictionary<string, string> ActiveDevices { get; } = new();

        // 5. SORTED DICTIONARY: For historical logs ordered by timestamp
        public SortedDictionary<DateTime, string> HistoricalLogs { get; } = new();

        // 6. HASHSET: For preventing duplicate active error states
        public HashSet<string> ActiveErrors { get; } = new();

        // 7. PREDICTIVE ENGINE: Tracking user search history
        public List<string> RecentSearches { get; } = new();

        // --- Methods for Part 2 ---

        public void ProcessIncomingTelemetry(string mac, bool isCritical)
        {
            if (isCritical)
            {
                // Priority bypasses standard queue
                CriticalAlertQueue.Add($"CRITICAL from {mac}");
            }
            else
            {
                StandardTelemetryQueue.Enqueue($"Normal from {mac}");
            }
        }

        public void IssueCommand(DeviceCommand command)
        {
            CommandHistory.Push(command);
            HistoricalLogs.Add(DateTime.UtcNow, $"Command {command.CommandType} issued to {command.DeviceMac}");
        }

        public DeviceCommand? UndoLastCommand()
        {
            if (CommandHistory.Count > 0)
            {
                return CommandHistory.Pop();
            }
            return null;
        }

        public void LogSearch(string searchTerm)
        {
            RecentSearches.Add(searchTerm);
        }
    }
}