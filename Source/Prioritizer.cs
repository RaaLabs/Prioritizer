/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.IO;
using System.Linq;


namespace RaaLabs.Edge.Prioritizer
{
    /// <summary>
    /// 
    /// </summary>
    public class Prioritizer
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
        public bool IsPrioritized(string timeSeries)
        {
            return _configuration.Prioritized.Contains(timeSeries);
        }
    }
}