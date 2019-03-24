/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Dolittle.Edge.TimeSeriesPrioritizer
{
    /// <summary>
    /// Represents an input message
    /// </summary>
    public class TimeSeriesDataPoint
    {
        /// <summary>
        /// Gets or sets the <see cref="TimeSeriesId"/> this value belong to
        /// </summary>
        public TimeSeriesId TimeSeriesId { get; set; }

        /// <summary>
        /// Gets or sets the value
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Gets or sets the timestamp in the form of EPOCH milliseconds granularity
        /// </summary>
        public long Timestamp { get; set; }
    }
}