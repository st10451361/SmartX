using Microsoft.AspNetCore.Mvc;
using SmartX.Models;
using SmartX.Services;

namespace SmartX.Controllers
{
    [ApiController]
    [IgnoreAntiforgeryToken]
    [Route("api/[controller]")]
    public class CommandStreamController : ControllerBase
    {
        private readonly SmartXDataManager _dataManager;

        public CommandStreamController(SmartXDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        [HttpGet("devices")]
        public IActionResult GetDevices()
        {
            // Simulate some devices for the UI
            if (_dataManager.ActiveDevices.Count == 0)
            {
                _dataManager.ActiveDevices.Add("00:1B:44:11:3A:01", "Hydroponics Pump");
                _dataManager.ActiveDevices.Add("00:1B:44:11:3A:02", "Smart Relay");
                _dataManager.ActiveDevices.Add("00:1B:44:11:3A:03", "Moisture Sensor");
            }
            return Ok(_dataManager.ActiveDevices);
        }

        [HttpPost("issue-command")]
        public IActionResult IssueCommand([FromBody] DeviceCommand command)
        {
            _dataManager.IssueCommand(command);
            return Ok(new { message = $"Command {command.CommandType} sent to {command.DeviceMac}" });
        }

        [HttpPost("undo")]
        public IActionResult UndoCommand()
        {
            var undoneCommand = _dataManager.UndoLastCommand();
            if (undoneCommand == null) return BadRequest("No commands to undo.");

            return Ok(new { message = $"Undid command: {undoneCommand.CommandType} on {undoneCommand.DeviceMac}" });
        }

        [HttpGet("search")]
        public IActionResult SearchDevices([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query)) return Ok(_dataManager.ActiveDevices);

            _dataManager.LogSearch(query); // Save search for the predictive engine

            var results = _dataManager.ActiveDevices
                .Where(d => d.Value.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                            d.Key.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToDictionary(d => d.Key, d => d.Value);

            return Ok(results);
        }

        [HttpGet("insights")]
        public IActionResult GetInsights()
        {
            // PREDICTIVE ENGINE LOGIC
            var insights = new List<string>();

            // If the user searched for "Moisture" recently, suggest a pump action
            if (_dataManager.RecentSearches.Any(s => s.Contains("Moisture", StringComparison.OrdinalIgnoreCase)))
            {
                insights.Add("Automated Insight: Frequent moisture searches detected. Suggest checking 'Hydroponics Pump' status.");
            }

            if (_dataManager.CriticalAlertQueue.Count > 0)
            {
                insights.Add("Automated Insight: Critical alerts pending. Suggest issuing a 'Shut Down All Valves' command.");
            }

            if (insights.Count == 0)
            {
                insights.Add("System stable. No predictive actions required at this time.");
            }

            return Ok(insights);
        }
    }
}