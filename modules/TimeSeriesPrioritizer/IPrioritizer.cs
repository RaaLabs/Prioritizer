/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Dolittle.Edge.Modules;

namespace Dolittle.Edge.TimeSeriesPrioritizer
{
    /// <summary>
    /// Defines a system that can prioritize timeseries based on their <see cref="TimeSeries"/>
    /// </summary>
    public interface IPrioritizer
    {
        /// <summary>
        /// Check if timeseries is prioritized or not
        /// </summary>
        /// <param name="timeSeries"><see cref="TimeSeries"/> to check</param>
        /// <returns>True if prioritized, false if not</returns>
        bool IsPrioritized(TimeSeries timeSeries);
    }
}