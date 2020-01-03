/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.IO;
using System.Linq;
using RaaLabs.TimeSeries.Modules;
using Dolittle.IO;
using Dolittle.Serialization.Json;

namespace RaaLabs.TimeSeries.Prioritizer
{
    /// <summary>
    /// Represents an implementation of <see cref="IPrioritizer"/>
    /// </summary>
    public class Prioritizer : IPrioritizer
    {
        readonly PrioritizerConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of <see cref="Prioritizer"/>
        /// </summary>
        /// <param name="configuration"><see cref="PrioritizerConfiguration">Configuration</see> to use</param>
        public Prioritizer(PrioritizerConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <inheritdoc/>
        public bool IsPrioritized(RaaLabs.TimeSeries.TimeSeries timeSeries)
        {
            return _configuration.Prioritized.Contains(timeSeries);
        }
    }
}