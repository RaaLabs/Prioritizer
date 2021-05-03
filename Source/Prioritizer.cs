// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Linq;

namespace RaaLabs.Edge.Prioritizer
{
    /// <summary>
    /// Defines a system that can prioritize timeseries
    /// </summary>
    public class Prioritizer
    {
        readonly PrioritizerConfiguration _configuration;

        /// <summary>
        /// Defines a system that can prioritize timeseries 
        /// </summary>
        /// <param name="configuration"><see cref="PrioritizerConfiguration">Configuration</see> to use</param>
        public Prioritizer(PrioritizerConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Check if timeseries is prioritized or not
        /// </summary>
        /// <param name="timeSeries">unique identifier</param>
        /// <returns>True if prioritized, false if not</returns>
        public bool IsPrioritized(string timeSeries)
        {
            return _configuration.Prioritized.Contains(timeSeries);
        }
    }
}