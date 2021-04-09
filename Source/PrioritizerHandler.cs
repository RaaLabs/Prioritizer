/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Text;
using System.Threading.Tasks;
using RaaLabs.Edge.Modules.EventHandling;
using RaaLabs.Edge.Prioritizer.events;
using Serilog;

namespace RaaLabs.Edge.Prioritizer
{
    public class PrioritizerHandler : IConsumeEvent<EdgeHubDataPointReceived>, IProduceEvent<EdgeHubDataPointPrioritized>, IProduceEvent<EdgeHubDataPointNonPrioritized>
    {
        private readonly ILogger _logger;
        private readonly Prioritizer _prioritizer;
        public event EventEmitter<EdgeHubDataPointPrioritized> SendPrioritizedEvent;
        public event EventEmitter<EdgeHubDataPointNonPrioritized> SendNonPrioritizedEvent;

        /// <summary>
        /// Initializes a new instance of <see cref="PrioritizerHandler"/>
        /// </summary>
        /// <param name="serializer"><see cref="ISerializer">JSON serializer</see></param>
        /// <param name="logger"><see cref="ILogger"/> used for logging</param>
        /// <param name="prioritizer"><see cref="IPrioritizer"/> for dealing with prioritization</param>
        /// <param name="client"><see cref="ICommunicationClient"/> for dealing with messaging</param>
        public PrioritizerHandler(
            Prioritizer prioritizer,
            ILogger logger)
        {
            _prioritizer = prioritizer;
            _logger = logger;
        }
        
         public void Handle(EdgeHubDataPointReceived @event)
        {
            if (_prioritizer.IsPrioritized(@event.Timeseries))
            {
                _logger.Information("Datapoint prioritized");
                var prioritizedDataPoint = new EdgeHubDataPointPrioritized(@event.Timeseries, @event.Value, @event.Timestamp);
                SendPrioritizedEvent(prioritizedDataPoint);                
            }
            else
            {
                _logger.Information("Datapoint not prioritized");
                var nonPrioritizedDataPoint = new EdgeHubDataPointNonPrioritized(@event.Timeseries, @event.Value, @event.Timestamp);
                SendNonPrioritizedEvent(nonPrioritizedDataPoint);
            }
        }
    }
}