/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Dolittle.Edge.TimeSeriesPrioritizer
{
    /// <summary>
    /// Defines a system that can prioritize timeseries based on their <see cref="TimeSeriesId"/>
    /// </summary>
    public interface IPrioritizer
    {
        /// <summary>
        /// Check if timeseries is prioritized or not
        /// </summary>
        /// <param name="timeSeriesId"><see cref="TimeSeriesId"/> to check</param>
        /// <returns>True if prioritized, false if not</returns>
        bool IsPrioritized(TimeSeriesId timeSeriesId);
    }
}