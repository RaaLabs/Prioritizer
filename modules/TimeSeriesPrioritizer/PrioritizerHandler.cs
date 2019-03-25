/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Text;
using System.Threading.Tasks;
using Dolittle.Edge.Modules;
using Dolittle.Logging;
using Dolittle.Serialization.Json;
using Microsoft.Azure.Devices.Client;

namespace Dolittle.Edge.TimeSeriesPrioritizer
{

    /// <summary>
    /// Represents a <see cref="ICanHandleDataPoint{T}">message handler</see> for prioritizing timeseries
    /// </summary>
    public class PrioritizerHandler : ICanHandleDataPoint<object>
    {
        readonly ISerializer _serializer;
        readonly ILogger _logger;
        readonly IPrioritizer _prioritizer;
        private readonly ICommunicationClient _client;

        /// <summary>
        /// Initializes a new instance of <see cref="PrioritizerHandler"/>
        /// </summary>
        /// <param name="serializer"><see cref="ISerializer">JSON serializer</see></param>
        /// <param name="logger"><see cref="ILogger"/> used for logging</param>
        /// <param name="prioritizer"><see cref="IPrioritizer"/> for dealing with prioritization</param>
        /// <param name="client"><see cref="ICommunicationClient"/> for dealing with messaging</param>
        public PrioritizerHandler(
            ISerializer serializer,
            ILogger logger,
            IPrioritizer prioritizer,
            ICommunicationClient client)
        {
            _serializer = serializer;
            _logger = logger;
            _prioritizer = prioritizer;
            _client = client;
        }

        /// <inheritdoc/>
        public Input Input => "events";

        /// <inheritdoc/>
        public async Task Handle(DataPoint<dynamic> dataPoint)
        {
            if (_prioritizer.IsPrioritized(dataPoint.TimeSeries))
            {
                _logger.Information("Datapoint prioritized");
                await _client.SendAsJson("prioritized", dataPoint);
            }
            else
            {
                _logger.Information("Datapoint not prioritized");
                await _client.SendAsJson("nonprioritized", dataPoint);
            }

            await Task.CompletedTask;
        }
    }
}