// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using RaaLabs.Edge.Modules.EdgeHub;

namespace RaaLabs.Edge.Prioritizer.Events
{
    /// <summary>
    /// Non-prioritized output event
    /// </summary>
    [OutputName("nonprioritized")]
    public class EdgeHubDataPointNonPrioritized : IEdgeHubOutgoingEvent
    {
        /// <inheritdoc/>
        public EdgeHubDataPointNonPrioritized(Guid timeseries, dynamic value, long timestamp)
        {
            TimeSeries = timeseries;
            Timestamp = timestamp;
            Value = value;
        }

        /// <summary>
        /// The time series id
        /// </summary>
        public Guid TimeSeries { get; set; }

        /// <summary>
        /// The value of the data point
        /// </summary>
        public dynamic Value { get; set; }

        /// <summary>
        /// The timestamp, represented as epoc time with milliseconds
        /// </summary>
        public long Timestamp { get; set; }
    }
}
