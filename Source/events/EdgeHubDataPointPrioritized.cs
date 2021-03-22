using System.Diagnostics.CodeAnalysis;
using RaaLabs.Edge.Modules.EdgeHub;

namespace RaaLabs.Edge.Prioritizer.events
{
    [ExcludeFromCodeCoverage]
    [OutputName("prioritized")]
    public class EdgeHubDataPointPrioritized : IEdgeHubOutgoingEvent
    {
        public EdgeHubDataPointPrioritized(string timeseries, dynamic value, long timestamp)
        {
            Timeseries = timeseries;
            Value = value;
            Timestamp = timestamp;
        }
        public string Timeseries { get; set; }

        public dynamic Value { get; set; }

        public long Timestamp { get; set; }
    }
}
