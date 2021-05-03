// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using RaaLabs.Edge.Modules.Configuration;

namespace RaaLabs.Edge.Prioritizer
{
    /// <summary>
    /// Represents the configuration for the prioritizer
    /// </summary>
    [Name("configuration.json")]
    [RestartOnChange]
    public class PrioritizerConfiguration : IConfiguration
    {
        /// <summary>
        /// Initializes a new instance of <see cref="PrioritizerConfiguration"/>
        /// </summary>
        /// <param name="prioritized"></param>
        public PrioritizerConfiguration(IEnumerable<string> prioritized)
        {
            Prioritized = prioritized;
        }

        /// <summary>
        /// Gets the prioritized timeseries
        /// </summary>
        public IEnumerable<string> Prioritized { get; }
    }
}