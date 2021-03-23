/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using TechTalk.SpecFlow;
using System.Linq;
using FluentAssertions;
using RaaLabs.Edge.Prioritizer.Specs.Drivers;
using Newtonsoft.Json;
using RaaLabs.Edge.Prioritizer.events;
using RaaLabs.Edge.Modules.EdgeHub;
using Autofac;
using System.Collections.Generic;
using System;

namespace RaaLabs.Edge.Prioritizer.Specs.StepDefinitions
{
    [Binding]
    public class SystemIntegrationSteps
    {
        private readonly ApplicationContext _appContext;

        public SystemIntegrationSteps(ApplicationContext appContext)
        {
            _appContext = appContext;
        }


        [Given(@"a config file with the following mapping")]
        public void GivenAConfigFileWithThefollowingMapping(Table table)
        {
            IEnumerable<string> prioritized = table.Rows.Select(row => row["prioritized"]);
            var prioritizer = new PrioritizerConfiguration(prioritized);
            var file = JsonConvert.SerializeObject(prioritizer);
            _appContext.AddConfigurationFile("data/configuration.json", file);
        }

        [Given(@"the application is running")]
        public void TheApplicationIsRunning()
        {
            _appContext.Run();
        }

        [When(@"the following events are received")]
        public void WhenTheFollonwingEventsAreReceived(Table table)
        {
            NullIotModuleClient client = (NullIotModuleClient)_appContext.ScopeHolder.Scope.Resolve<IIotModuleClient>();
            foreach (var row in table.Rows)
            {
                var incomingEvent = new EdgeHubDataPointReceived
                {
                    Timeseries = row["Timeseries"],
                    Value = float.Parse(row["Value"]),
                    Timestamp = long.Parse(row["Timestamp"])
                };
                client.SimulateIncomingEvent("events", JsonConvert.SerializeObject(incomingEvent));
            }

        }

        [Then(@"the following events are produced")]
        public void ThenTheFollowingEventsAreProduced(Table table)
        {
            NullIotModuleClient client = (NullIotModuleClient)_appContext.ScopeHolder.Scope.Resolve<IIotModuleClient>();            
       
            var sentMessages = client.MessagesSent.ToDictionary(entry => JsonConvert.DeserializeObject(entry.Item2), entry => entry.Item1);
            foreach (var (sentMessage, expectedMessage) in sentMessages.Zip(table.Rows))
            {
                var prioritizedMessage = sentMessage.Value;
                prioritizedMessage.Should().Be(expectedMessage["OutputName"]);
            }
        }
    }

}

