/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using RaaLabs.TimeSeries.Modules;

namespace RaaLabs.TimeSeries.Prioritizer
{
    /// <summary>
    /// Defines a system that can prioritize timeseries based on their <see cref="TimeSeries"/>
    /// </summary>
    public interface IPrioritizer
    {
        /// <summary>
        /// Check if timeseries is prioritized or not
        /// </summary>
        /// <param name="timeSeries"><see cref="RaaLabs.TimeSeries.TimeSeries"/> to check</param>
        /// <returns>True if prioritized, false if not</returns>
        bool IsPrioritized(RaaLabs.TimeSeries.TimeSeries timeSeries);
    }
}