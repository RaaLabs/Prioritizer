using System.Diagnostics.CodeAnalysis;
using RaaLabs.Edge.Modules.EdgeHub;

namespace RaaLabs.Edge.Prioritizer.events
{
    [ExcludeFromCodeCoverage]
    [InputName("events")]
    public class EdgeHubDataPointReceived : IEdgeHubIncomingEvent
    {
        public string Timeseries { get; set; }

        public dynamic Value { get; set; }

        public long Timestamp { get; set; }
    }
}
