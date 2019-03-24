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
    /// Represents a <see cref="ICanHandleMessages">message handler</see> for prioritizing timeseries
    /// </summary>
    public class PrioritizerMessageHandler : ICanHandleMessages
    {
        readonly ISerializer _serializer;
        readonly ILogger _logger;
        readonly IPrioritizer _prioritizer;
        private readonly IClient _client;

        /// <summary>
        /// Initializes a new instance of <see cref="PrioritizerMessageHandler"/>
        /// </summary>
        /// <param name="serializer"><see cref="ISerializer">JSON serializer</see></param>
        /// <param name="logger"><see cref="ILogger"/> used for logging</param>
        /// <param name="prioritizer"><see cref="IPrioritizer"/> for dealing with prioritization</param>
        /// <param name="client"><see cref="IClient"/> for dealing with messaging</param>
        public PrioritizerMessageHandler(
            ISerializer serializer,
            ILogger logger,
            IPrioritizer prioritizer,
            IClient client)
        {
            _serializer = serializer;
            _logger = logger;
            _prioritizer = prioritizer;
            _client = client;
        }

        /// <inheritdoc/>
        public Input Input => "events";

        /// <inheritdoc/>
        public async Task<MessageResponse> Handle(Message message)
        {
            try
            {

                _logger.Information($"Handle incoming message");
                var messageBytes = message.GetBytes();
                var messageString = Encoding.UTF8.GetString(messageBytes);
                _logger.Information($"Event received '{messageString}'");
                var dataPoint = _serializer.FromJson<TimeSeriesDataPoint>(messageString);
                if (_prioritizer.IsPrioritized(dataPoint.TimeSeriesId))
                {
                    _logger.Information("Datapoint prioritized");
                    await _client.SendEventAsJson("prioritized", dataPoint);
                }
                else
                {
                    _logger.Information("Datapoint not prioritized");
                    await _client.SendEventAsJson("nonprioritized", dataPoint);
                }

                return MessageResponse.Completed;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Issues writing datapoint : '{ex.Message}'");
                return MessageResponse.Abandoned;
            }

        }
    }
}