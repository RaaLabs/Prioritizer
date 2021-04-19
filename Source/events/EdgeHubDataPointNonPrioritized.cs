// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using RaaLabs.Edge.Modules.EdgeHub;

namespace RaaLabs.Edge.Prioritizer.Events
{
    [OutputName("nonprioritized")]
    public class EdgeHubDataPointNonPrioritized : IEdgeHubOutgoingEvent
    {
        public EdgeHubDataPointNonPrioritized(string timeseries, dynamic value, long timestamp)
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
