/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Dolittle.Configuration;
using RaaLabs.TimeSeries.Modules;

namespace RaaLabs.TimeSeries.Prioritizer
{
    /// <summary>
    /// Represents the configuration for the prioritizer
    /// </summary>
    [Name("configuration")]
    public class PrioritizerConfiguration : IConfigurationObject, ITriggerAppRestartOnChange
    {
        /// <summary>
        /// Initializes a new instance of <see cref="PrioritizerConfiguration"/>
        /// </summary>
        /// <param name="prioritized"></param>
        public PrioritizerConfiguration(IEnumerable<RaaLabs.TimeSeries.TimeSeries> prioritized)
        {
            Prioritized = prioritized;
        }

        /// <summary>
        /// Gets the prioritized timeseries
        /// </summary>
        public IEnumerable<RaaLabs.TimeSeries.TimeSeries> Prioritized { get; }
    }
}