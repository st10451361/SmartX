using SmartX.Models;

namespace SmartX.Services
{
    public class DeploymentNode
    {
        public string Name { get; set; } = string.Empty;
        public string MacAddress { get; set; } = string.Empty;
        public int ZoneId { get; set; }
        public List<DeploymentNode> Children { get; set; } = new();
    }

    public class ValidationService
    {
        // Recursive validation algorithm
        public bool ValidateNodeConfiguration(DeploymentNode node)
        {
            // Validate leaf node (Sensor)
            if (node.Children == null || node.Children.Count == 0)
            {
                return !string.IsNullOrEmpty(node.MacAddress) && node.ZoneId > 0;
            }

            //Validate all children
            foreach (var child in node.Children)
            {
                if (!ValidateNodeConfiguration(child)) return false;
            }
            return true;
        }
    }
}