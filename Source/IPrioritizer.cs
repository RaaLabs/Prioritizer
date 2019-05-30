/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Dolittle.TimeSeries.Modules;

namespace Dolittle.TimeSeries.Prioritizer
{
    /// <summary>
    /// Defines a system that can prioritize timeseries based on their <see cref="TimeSeries"/>
    /// </summary>
    public interface IPrioritizer
    {
        /// <summary>
        /// Check if timeseries is prioritized or not
        /// </summary>
        /// <param name="timeSeries"><see cref="Dolittle.TimeSeries.Modules.TimeSeries"/> to check</param>
        /// <returns>True if prioritized, false if not</returns>
        bool IsPrioritized(Dolittle.TimeSeries.Modules.TimeSeries timeSeries);
    }
}