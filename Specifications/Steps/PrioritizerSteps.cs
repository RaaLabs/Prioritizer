// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using TechTalk.SpecFlow;
using System.Collections.Generic;
using FluentAssertions;
using System;

namespace RaaLabs.Edge.Prioritizer.Specs.StepDefinitions
{
    [Binding]
    public class PrioritizerSteps
    {
        private readonly Prioritizer _prioritizer;
        private HashSet<Guid> _nonPrioritizedTimeseries;
        private HashSet<Guid> _prioritizedTimeseries;

        public PrioritizerSteps(Prioritizer prioritizer)
        {
            _prioritizer = prioritizer;
            _prioritizedTimeseries = new HashSet<Guid>();
            _nonPrioritizedTimeseries = new HashSet<Guid>();
        }

        [When(@"the following timeseries are requested")]
        public void TheFollowingTimeseriesAreRequested(Table table)
        {
            foreach (var row in table.Rows)
            {
                var timeseries = Guid.Parse(row["timeseries"]);

                if (_prioritizer.IsPrioritized(timeseries))
                {
                    _prioritizedTimeseries.Add(timeseries);
                }
                else
                {
                    _nonPrioritizedTimeseries.Add(timeseries);
                }
            }
        }
        [Then(@"the following timeseries will be prioritized")]
        public void ThenTheFollowingTimeseriesWillBePrioritized(Table timeseries)
        {
            foreach (var row in timeseries.Rows)
            {
                _prioritizedTimeseries.Should().Contain(Guid.Parse(row["timeseries"]));
            }
        }
        
        [Then(@"the following timeseries will be non prioritized")]
        public void ThenTheFollowingTimeseriesWillBeNonPrioritized(Table timeseries)
        {
            foreach (var row in timeseries.Rows)
            {
                _nonPrioritizedTimeseries.Should().Contain(Guid.Parse(row["timeseries"]));
            }
        }
    }
}