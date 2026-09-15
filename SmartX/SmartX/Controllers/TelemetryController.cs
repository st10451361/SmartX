using Microsoft.AspNetCore.Mvc;
using SmartX.Models;

namespace SmartX.Controllers


{
    [ApiController]
    [IgnoreAntiforgeryToken]
    [Route("api/[controller]")]
    public class TelemetryController : ControllerBase
    {
        private static List<object> _telemetryStore = new();

        [HttpPost("ingest")]
        public IActionResult IngestData([FromBody] TelemetryPacket<object> packet)
        {
            _telemetryStore.Add(packet);
            return Ok(new { status = "Ingested", timestamp = packet.Timestamp });
        }

        [HttpPost("upload-log")]
        public async Task<IActionResult> UploadLog([FromBody] FileUploadRequest request) 
        {
            try
            {
                if (string.IsNullOrEmpty(request.Base64Data))
                    return BadRequest("Error: No file data received.");

                byte[] fileBytes = Convert.FromBase64String(request.Base64Data);

                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string folderPath = Path.Combine(desktopPath, "SmartX_Uploads");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string safeMac = request.MacAddress.Replace(":", "_");
                var filePath = Path.Combine(folderPath, $"{safeMac}_{request.FileName}");

                await System.IO.File.WriteAllBytesAsync(filePath, fileBytes);

                return Ok(new { message = $"Success! Saved to: {filePath}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Backend Error: {ex.Message}");
            }
        }
    }
    public class FileUploadRequest
    {
        public string MacAddress { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string Base64Data { get; set; } = string.Empty;
    }
}