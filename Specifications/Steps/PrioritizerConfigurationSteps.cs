// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using TechTalk.SpecFlow;
using System.Linq;
using BoDi;
using System.Collections.Generic;
using System;

namespace RaaLabs.Edge.Prioritizer.Specs.StepDefinitions
{
    [Binding]
    public class PrioritizerConfigurationSteps
    {
        private readonly IObjectContainer _container;
        public PrioritizerConfigurationSteps(IObjectContainer container)
        {
            _container = container;
        }
        
        [Given(@"the prioritized timeseries")]
        public void GivenThePrioritizedTimeseries(Table table)
        {
            IEnumerable<Guid> prioritized = table.Rows.Select(row => Guid.Parse(row["prioritized"]));
            var prioritizer = new PrioritizerConfiguration(prioritized);
            _container.RegisterInstanceAs<PrioritizerConfiguration>(prioritizer); 
        }

    }
}