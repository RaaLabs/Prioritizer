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

        // TODO add system integration test. Need to extend NullIotModuleClient to include outputname
        [Then(@"the following prioritized events are produced")]
        public void ThenTheFollowingPrioritizedEventsAreProduced(Table table)
        {
            NullIotModuleClient client = (NullIotModuleClient)_appContext.ScopeHolder.Scope.Resolve<IIotModuleClient>();            
            foreach (var sentMessage in client.MessagesSent)
            {
                
            }
            /*foreach (var (sentMessage, expectedMessage) in client.MessagesSent.Zip(table.Rows))
            {
                var sent = JsonConvert.DeserializeObject<EdgeHubDataPointRemapped>(sentMessage);
                ((double)sent.Value).Should().BeApproximately(float.Parse(expectedMessage["Value"]), 0.01);

            }*/

        }

    }

}

